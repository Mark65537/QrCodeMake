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
            MinimumSize = Size;
            cB_error.SelectedIndex = 1;
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
                b_next.Enabled = true;
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
            string pass = Settings.Default.appPass;

            Dictionary<string,string> confDic = HtmlClass.ReadCfg(htmlPath, confPath);

            if (!confDic.ContainsKey("{$img_qrcode}"))
            {
                string bmpName = lpersons[persons_index].ToString() + ".png";
                if (!File.Exists(bmpName))
                {
                    lpersons[persons_index].QrCode.Save(bmpName);                    
                }
                HtmlClass.WriteCfg(ref confDic, confPath, "{$img_qrcode}", bmpName);
            }

            WordClass.ConvertDocToHtml(wordPath, htmlPath);
            string result = EmailClass.sendEmail(emailFrom, emailTo, pass, body: File.ReadAllText(htmlPath), confDic, subject);
            MessageBox.Show(result, "Информация");
        }


    }
}
