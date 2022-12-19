﻿using GeneralClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using QrCodeMake_WinForm.Properties;
using System.Security;

namespace QrCodeMake_WinForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        List<Person> lpersons= new List<Person>();
        int persons_index = 0;

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text += Assembly.GetExecutingAssembly().GetName().Version;
            MinimumSize = Size;//для того что бы нельзя было уменьшить форму
            cB_error.SelectedIndex = Settings.Default.corErr;

            if (!tB_fileName.Text.Equals(string.Empty) || !(tB_fileName.Text = Settings.Default.excelFile).Equals(string.Empty))
            {
                if (cB_error.Text.Equals(string.Empty))
                    lpersons = ExcelClass.ExcelToPersons(tB_fileName.Text);
                else
                    lpersons = ExcelClass.ExcelToPersons(tB_fileName.Text, cB_error.SelectedIndex);

                pB_QrCode.Image = lpersons[0].QrCode;
                b_next.Enabled = lpersons.Count > 1 ? true : false;
            }
            //else if (!Settings.Default.excelFile.Equals(string.Empty))
            //{               
                
            //    tB_fileName.Text = Settings.Default.excelFile;

            //    if (cB_error.Text.Equals(string.Empty))
            //        lpersons = ExcelClass.ExcelToPersons(tB_fileName.Text);
            //    else
            //        lpersons = ExcelClass.ExcelToPersons(tB_fileName.Text, cB_error.SelectedIndex);

            //    pB_QrCode.Image = lpersons[0].QrCode;
            //    b_next.Enabled = lpersons.Count > 0 ? true : false;                
            //}
        }

        private void b_open_Click(object sender, EventArgs e)
        {
            if (oFD_file.ShowDialog() == DialogResult.OK) {
                tB_fileName.Text = oFD_file.FileName;
                if(cB_error.Text.Equals(string.Empty))
                    lpersons = ExcelClass.ExcelToPersons(oFD_file.FileName);
                else
                    lpersons = ExcelClass.ExcelToPersons(oFD_file.FileName, cB_error.SelectedIndex);
                
                pB_QrCode.Image = lpersons[0].QrCode;
                b_next.Enabled=lpersons.Count > 1 ? true : false;
            }
        }

        private void pB_QrCode_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cMS_forPicBox.Show();
            }
        }

        private void cMS_forPicBox_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == TSMI_normal)
            {
                pB_QrCode.SizeMode = PictureBoxSizeMode.Normal;
            }
            else if (e.ClickedItem == TSMI_stretchImage)
            {
                pB_QrCode.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else if (e.ClickedItem == TSMI_centerImage)
            {
                pB_QrCode.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            else if (e.ClickedItem == TSMI_zoom)
            {
                pB_QrCode.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void b_copyQr_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pB_QrCode.Image);
        }

        private void b_prev_Click(object sender, EventArgs e)
        {
            if (--persons_index == 0)
            {
                b_prev.Enabled = false;
                b_next.Enabled = true;
            }
            else
            {
                b_next.Enabled = true;
                b_prev.Enabled = true;
            }

            pB_QrCode.Image = lpersons[persons_index].QrCode;
        }

        private void b_next_Click(object sender, EventArgs e)
        {
            if ((++persons_index) == lpersons.Count - 1)
            {
                b_next.Enabled = false;
            }
            else
            {
                b_next.Enabled = true;
            }

            b_prev.Enabled = true;
            pB_QrCode.Image = lpersons[persons_index].QrCode;
        }

        private void b_sendEmail_Click(object sender, EventArgs e)
        {
            //Paths
             string wordPath= "C:\\Users\\User\\Desktop\\пример QR кода.docx";
             string htmlPath= "C:\\Users\\User\\Desktop\\пример QR кода.html";
             string confPath = "C:\\Users\\User\\Desktop\\conf.cfg";
            //Paths end

            string subject = "Сообщение сгенерированно с помощью программы QrCodeMake";
            string emailFrom = Settings.Default.emailFrom;
            string emailTo = lpersons[persons_index].Email;
            SecureString pass = Settings.Default.appPass.Aggregate(new SecureString(), (s, c) =>{s.AppendChar(c);return s;}, (s) =>{s.MakeReadOnly();return s;}); 

            Dictionary<string,string> confDic = HtmlClass.ReadCfg(htmlPath, confPath);
            string bmpName = lpersons[persons_index].ToString() + ".png";

            if (!File.Exists(bmpName))//создаем картинку qr-кода, если ее не существует            
                lpersons[persons_index].QrCode.Save(bmpName);

            //if (!confDic.ContainsKey("{$img_qrcode}"))//если нету ключа, то мы его создаем
            //{
            //    HtmlClass.WriteCfg(ref confDic, confPath, "{$img_qrcode}", bmpName);
            //}
            //else if (!confDic["{$img_qrcode}"].Equals(bmpName))//если есть ключ, но не то значение то мы его перезаписываем
            //{
            //    HtmlClass.WriteCfg(ref confDic, confPath, "{$img_qrcode}", bmpName);//перезаписать значение в файле
            //}

            WordClass.ConvertDocToHtml(wordPath, htmlPath);
            string result = EmailClass.sendEmail(emailFrom, emailTo, pass, body: File.ReadAllText(htmlPath), confDic, subject);
            MessageBox.Show(result, "Информация");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.corErr = cB_error.SelectedIndex;
            Settings.Default.excelFile = tB_fileName.Text;
            Settings.Default.Save();
        }
    }
}
