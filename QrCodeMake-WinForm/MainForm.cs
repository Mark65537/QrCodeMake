using GeneralClassLibrary;
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
using System.Security.Cryptography;


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

        //Paths
         string progPath = Environment.CurrentDirectory;//папка где будут находиться файл конфигурации и файлы шаблонов
         string wordPath;
         string htmlPath;
         string confPath;
         string qrCodeFolder;
        //Paths end

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Paths   перемещены сюда для того что бы работал progPath 
             wordPath = progPath + "\\template.docx";
             htmlPath = progPath + "\\template.html";
             confPath = progPath + "\\conf.cfg";
             qrCodeFolder = "qrCodeImg\\";
            //Paths end
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
        }

        private void b_open_Click(object sender, EventArgs e)
        {            
            if (oFD_file.ShowDialog() == DialogResult.OK) {
                try
                {
                    tB_fileName.Text = oFD_file.FileName;
                    if (cB_error.Text.Equals(string.Empty))
                        lpersons = ExcelClass.ExcelToPersons(oFD_file.FileName);
                    else
                        lpersons = ExcelClass.ExcelToPersons(oFD_file.FileName, cB_error.SelectedIndex);

                    pB_QrCode.Image = lpersons[0].QrCode;
                    //активация кнопок
                    b_next.Enabled = lpersons.Count > 1 ? true : false;
                    b_copyQr.Enabled = true;
                    b_sendEmail.Enabled = true;
                    //активация кнопок end
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
            string subject = Settings.Default.subject;
            string emailFrom = Settings.Default.mailFrom;
            string emailTo = lpersons[persons_index].Email;
            string provider = Settings.Default.provider;
            SecureString pass = Settings.Default.appPass.Aggregate(new SecureString(), (s, c) => { s.AppendChar(c); return s; }, (s) => { s.MakeReadOnly(); return s; });

            Dictionary<string, string> confDic = HtmlClass.ReadCfg(htmlPath, confPath);
            Dictionary<string, string> reportDic = new Dictionary<string, string>();
            string bmpName = string.Empty;
            string result = string.Empty;

            if (File.Exists(wordPath)) 
                WordClass.ConvertDocToHtml(wordPath, htmlPath);

            Directory.CreateDirectory(qrCodeFolder).Attributes |= FileAttributes.Hidden;//создание скрытой папки для хранения картинок QR-кодов 

            if (chB_sendAll.Checked)
            {
                foreach (Person p in lpersons)
                {
                    if (p.Events.Count > 0)
                    {
                        bmpName = qrCodeFolder + p.ToString() + ".png";

                        if (!File.Exists(bmpName))//создаем картинку qr-кода, если ее не существует            
                            p.QrCode.Save(bmpName);

                        confDic["{$img_qrcode}"] = bmpName;
                        result = MailClass.sendEmail(emailFrom,
                                                      emailTo,
                                                      pass,
                                                      body: File.Exists(htmlPath) ? File.ReadAllText(htmlPath) : "{$img_qrcode}",//если файла шаблона не существует, то отправляется просто картинка qr-кода
                                                      confDic,
                                                      subject,
                                                      provider);
                    }
                    reportDic.Add(p.Fio, result);
                }
            }
            else
            {
                if (lpersons[persons_index].Events.Count > 0)
                {
                    bmpName = qrCodeFolder + lpersons[persons_index].ToString() + ".png";

                    if (!File.Exists(bmpName))//создаем картинку qr-кода, если ее не существует            
                        lpersons[persons_index].QrCode.Save(bmpName);

                    confDic["{$img_qrcode}"] = bmpName;
                    result = MailClass.sendEmail(emailFrom,
                                                 emailTo,
                                                 pass,
                                                 body: File.Exists(htmlPath) ? File.ReadAllText(htmlPath) : "{$img_qrcode}",//если файла шаблона не существует, то отправляется просто картинка qr-кода
                                                 confDic,
                                                 subject,
                                                 provider);
                }
                reportDic.Add(lpersons[persons_index].Fio, result);
            }

            //удаление ненужных папок и файлов 
             File.Delete(htmlPath);
             if(Directory.Exists(htmlPath.Replace(".html", ".files")))
                 Directory.Delete(htmlPath.Replace(".html", ".files"), true);


            reportDic.Clear();

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
