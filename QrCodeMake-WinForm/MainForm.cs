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
using QrCodeMakelib;

namespace QrCodeMake_WinForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region Глобальные переменные
        List<Person> lpersons = new List<Person>();
        int persons_index = 0; 
        #endregion

        #region Пути
        string progPath = Environment.CurrentDirectory;//папка где будут находиться файл конфигурации и файлы шаблонов
        string wordPath;
        string htmlPath;
        string confPath;
        string qrCodeFolder; 
        #endregion


        private void MainForm_Load(object sender, EventArgs e)
        {
            #region Инициализация Путей(перемещены сюда для того что бы работал progPath) 
            wordPath = progPath + "\\template.docx";
            htmlPath = progPath + "\\template.html";
            confPath = progPath + "\\conf.json";
            qrCodeFolder = "qrCodeImg\\";
            #endregion
           
            Text += Assembly.GetExecutingAssembly().GetName().Version;
            MinimumSize = Size;//для того что бы нельзя было уменьшить форму
            cB_error.SelectedIndex = Settings.Default.corErr;

            if (!string.IsNullOrEmpty(tB_fileName.Text) || !(tB_fileName.Text = Settings.Default.excelFile).Equals(string.Empty))
            {
                if (cB_error.Text.Equals(string.Empty))
                    lpersons = ExcelClass.ExcelToPersons(tB_fileName.Text);
                else
                    lpersons = ExcelClass.ExcelToPersons(tB_fileName.Text, cB_error.SelectedIndex);

                if (lpersons.Count > 1)
                {
                    pB_QrCode.Image = lpersons[0].QrCode;
                    b_next.Enabled = true;
                }
                else
                {
                    b_next.Enabled = false;
                }                
            }
        }

        private void b_open_Click(object sender, EventArgs e)
        {            
            if (oFD_file.ShowDialog() == DialogResult.OK) {
                try
                {
                    lpersons.Clear();
                    tB_fileName.Text = oFD_file.FileName;
                    if (cB_error.Text.Equals(string.Empty))
                        lpersons = ExcelClass.ExcelToPersons(oFD_file.FileName);
                    else
                        lpersons = ExcelClass.ExcelToPersons(oFD_file.FileName, cB_error.SelectedIndex);

                    if (lpersons.Count > 0) {
                        pB_QrCode.Image = lpersons[0].QrCode;


                        #region Активация кнопок
                        b_next.Enabled = lpersons.Count > 1 ? true : false;
                        b_copyQr.Enabled = true;
                        b_sendEmail.Enabled = true;
                        #endregion
                        tB_Fio.Text = lpersons[persons_index].Fio;
                    }
                    else
                        MessageBox.Show("Нет участников с очной формой посещения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            persons_index--;
            b_prev.Enabled = persons_index != 0;
            b_next.Enabled = true;
            pB_QrCode.Image = lpersons[persons_index].QrCode;
            tB_Fio.Text = lpersons[persons_index].Fio;
        }
        //есть ли событие которое происходит когда меняется изображение в PictureBox?
        private void b_next_Click(object sender, EventArgs e)
        {
            persons_index++;
            b_next.Enabled = persons_index < lpersons.Count - 1;
            b_prev.Enabled = true;
            pB_QrCode.Image = lpersons[persons_index].QrCode;
            tB_Fio.Text = lpersons[persons_index].Fio;
        }

        private void b_sendEmail_Click(object sender, EventArgs e)
        {
            #region Переменные
            Dictionary<string, string> confDic = HtmlClass.ReadJSONcfg(confPath);
            Dictionary<string, string> reportDic = new Dictionary<string, string>();
            string result = string.Empty;
            MailMessageInfo mailMessageInfo = new MailMessageInfo()
            {
                subject = Settings.Default.subject,
                emailFrom = Settings.Default.mailFrom,                
                provider = Settings.Default.provider,
                pass = Settings.Default.appPass.Aggregate(new SecureString(), (s, c) => { s.AppendChar(c); return s; }, (s) => { s.MakeReadOnly(); return s; })
            };
            #endregion

            if (File.Exists(wordPath)) 
                WordClass.ConvertDocToHtml(wordPath, htmlPath);

            Directory.CreateDirectory(qrCodeFolder).Attributes |= FileAttributes.Hidden;//создание скрытой папки для хранения картинок QR-кодов 

            string body = File.Exists(htmlPath) ? File.ReadAllText(htmlPath) : confDic["1"];//если файла шаблона не существует, то отправляется просто картинка qr-кода
            if (chB_sendAll.Checked)//отправить всем
            {
                foreach (Person p in lpersons)
                {
                    mailMessageInfo.emailTo= p.Email;
                    mailMessageInfo.body = body;
                    result = SendEmailWithQrCode(p, mailMessageInfo, confDic);
                    reportDic.Add(p.Fio, result);
                }
            }
            else//отправить одному
            {
                mailMessageInfo.emailTo = lpersons[persons_index].Email;
                mailMessageInfo.body = body;
                result = SendEmailWithQrCode(lpersons[persons_index], mailMessageInfo, confDic);
                reportDic.Add(lpersons[persons_index].Fio, result);
            }


            #region Удаление ненужных папок и файлов
            File.Delete(htmlPath);
            //удалить папку со всеми файлами в ней
            if (Directory.Exists(htmlPath.Replace(".html", ".files")))
                Directory.Delete(htmlPath.Replace(".html", ".files"), true); 
            #endregion

            ReportForm RF = new ReportForm(reportDic);//отправка данных в отчетную форму
            reportDic.Clear();
            RF.Show();
            //MessageBox.Show(result, "Информация");
        }

        private string SendEmailWithQrCode(Person p, MailMessageInfo mailMessageInfo, Dictionary<string, string> confDic)
        {
            #region Переменные
            string bmpName = string.Empty;
            string result = string.Empty; 
            #endregion

            if (p.Events.Count > 0)
            {
                bmpName = qrCodeFolder + p.ToString() + ".png";

                if (!File.Exists(bmpName))//создаем картинку qr-кода, если ее не существует            
                    p.QrCode.Save(bmpName);

                confDic[confDic["0"]] = p.Fio;
                confDic[confDic["1"]] = bmpName;
                for (int i = 1; i <= p.Events.Count; i++)
                {
                    confDic[$"{confDic["2"]}{i}"] = p.Events[i - 1];
                }

                return MailClass.sendEmail(mailMessageInfo, confDic);
            }
            return "Нет событий для отправки сообщения";
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) 
        {
            Settings.Default.corErr = cB_error.SelectedIndex;
            Settings.Default.excelFile = tB_fileName.Text;
            Settings.Default.Save();
        }

        private void tB_fileName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (File.Exists(tB_fileName.Text))
                {

                }
            }
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (File.Exists(tB_fileName.Text))
                {

                }
            }
        }

        private void b_settings_Click(object sender, EventArgs e)
        {
            //List<Options> options = new List<Options>();
            OptionForm OF = new OptionForm(confPath);//отправка данных в отчетную форму
            OF.Show();
        }
    }
}
