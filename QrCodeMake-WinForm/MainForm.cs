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
        //заполнить массив Options данными
        List<Options> unChgableVars = new List<Options>();        
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

            initializeUnChgableList();

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

                        #region Заполнение TextBox
                        tB_fileName.Text = oFD_file.FileName;
                        tB_Fio.Text = lpersons[persons_index].Fio; 
                        #endregion                        
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
        
        private void initializeUnChgableList(int evtCount=1)
        {
            unChgableVars = new List<Options>
            {
                new Options { name = "Str_FIO", val = "", desc = "ФИО участника" },
                new Options { name = "Img_QRcode", val = "", desc = "Картинка с QR-кодом" },                
            };
            for(int i=1; i<=evtCount; i++)
                unChgableVars.Add(new Options { name = $"Str_Event{i}", val = "", desc = "Название мероприятия" });
    //        unChgableVars = new List<Options>
    //{
    //    new Options { name = "Str_FIO", val = "", desc = "ФИО участника" },
    //    new Options { name = "Img_QRcode", val = "", desc = "Картинка с QR-кодом" }
    //};
    //        unChgableVars.AddRange(Enumerable.Range(1, evtCount).Select(i => new Options { name = $"Str_Event{i}", val = "", desc = "Название мероприятия" }));
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
            //придумать название статическом класса и метода в нем, реализующий данный код
            //Название статического класса: EmailSender

            //Название метода: SendEmailsWithQrCodes

            //Описание метода: Метод отправляет электронные письма с QR-кодами и отчетом о доставке. Принимает список объектов Person, информацию о письме(тема, отправитель, провайдер, пароль), а также словарь с настройками.Если файл шаблона не существует, отправляется только картинка QR-кода.Если выбрана опция "отправить всем", то письма отправляются каждому объекту Person в списке, иначе отправляется только текущему объекту Person.Возвращает отчет о доставке в виде словаря, где ключ -ФИО получателя, значение - результат отправки.
            #region Переменные
            List<Options> chgableVars = HtmlClass.ReadJSONcfg(confPath);
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

            try
            {
                if (File.Exists(wordPath))
                {                    
                    //File.Copy(wordPath, $"{wordPath}Temp", true);
                    //WordClass.ReplaceWordsInDocByDic($"{wordPath}Temp", confDic);
                    WordClass.ConvertDocToHtml(wordPath, htmlPath);
                    //HtmlClass.ReplaceWordsInHtmlByDic(htmlPath, confDic);
                }


                Directory.CreateDirectory(qrCodeFolder).Attributes |= FileAttributes.Hidden;//создание скрытой папки для хранения картинок QR-кодов 

                string body = File.Exists(htmlPath) ? File.ReadAllText(htmlPath) : unChgableVars[1].name;//если файла шаблона не существует, то отправляется просто картинка qr-кода
                if (chB_sendAll.Checked)//отправить всем
                {
                    foreach (Person p in lpersons)
                    {
                        mailMessageInfo.emailTo = p.Email;
                        mailMessageInfo.body = body;
                        result = SendEmailWithQrCode(p, mailMessageInfo, chgableVars);
                        reportDic.Add(p.Fio, result);
                    }
                }
                else//отправить одному
                {
                    mailMessageInfo.emailTo = lpersons[persons_index].Email;
                    mailMessageInfo.body = body;
                    result = SendEmailWithQrCode(lpersons[persons_index], mailMessageInfo, chgableVars);
                    reportDic.Add(lpersons[persons_index].Fio, result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                #region Удаление ненужных папок и файлов
                File.Delete($"{wordPath}Temp");
                File.Delete(htmlPath);
                //удалить папку со всеми файлами в ней
                if (Directory.Exists(htmlPath.Replace(".html", ".files")))
                    Directory.Delete(htmlPath.Replace(".html", ".files"), true);
                #endregion

                ReportForm RF = new ReportForm(reportDic);//отправка данных в отчетную форму
                reportDic.Clear();
                RF.Show();
            }
            //MessageBox.Show(result, "Информация");
        }
        //можно ли упростить
        private string SendEmailWithQrCode(Person p, MailMessageInfo mailMessageInfo, List<Options> confDic)
        {
            #region Переменные
            string bmpName = string.Empty;
            string result = string.Empty;
            #endregion            

            initializeUnChgableList(p.Events.Count);

            if (p.Events.Count == 0)
            {
                return "Нет событий для отправки сообщения";
            }

            bmpName = $"{qrCodeFolder}{p}.png";

            if (!File.Exists(bmpName))//создаем картинку qr-кода, если ее не существует            
                p.QrCode.Save(bmpName);
            //var unChgableVars = new Options[p.Events.Count + 2];
            unChgableVars[0].val = p.Fio;
            unChgableVars[1].val = bmpName;                
            for (int i = 0; i < p.Events.Count; i++)
            {
                unChgableVars[i+2].val = p.Events[i];
            }

            return MailClass.sendEmail(mailMessageInfo, unChgableVars.Concat(confDic).ToList());
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
            OptionForm OF = new OptionForm(confPath, unChgableVars);//отправка данных в отчетную форму
            OF.Show();
        }
    }
}
