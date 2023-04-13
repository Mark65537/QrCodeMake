using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using OpenXmlPowerTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;

namespace GeneralClassLibrary
{
    public class WordClass
    {
        public static void WordToHtml(string DocxFilePath, string HTMLFilePath) {
            byte[] byteArray = File.ReadAllBytes(DocxFilePath);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(byteArray, 0, byteArray.Length);
                using (WordprocessingDocument doc = WordprocessingDocument.Open(memoryStream, true))
                {
                    HtmlConverterSettings settings = new HtmlConverterSettings()
                    {
                        PageTitle = "QRcode template"
                    };
                    XElement html = HtmlConverter.ConvertToHtml(doc, settings);

                    File.WriteAllText(HTMLFilePath, html.ToStringNewLineOnAttributes());
                }
            }
        }

        

        public static string ConvertDocToHtml(object Sourcepath, object TargetPath)
        {
            Word.Application newApp=null;
            Word.Documents d=null;
            Word.Document doc=null;
            string err = string.Empty;
            try
            {
                newApp = new Word.Application();


                object Unknown = Type.Missing;
                d = newApp.Documents;
                doc = d.Open(ref Sourcepath);

                object format = Word.WdSaveFormat.wdFormatHTML;

                doc.WebOptions.Encoding = Microsoft.Office.Core.MsoEncoding.msoEncodingUTF8;

                newApp.ActiveDocument.SaveAs(ref TargetPath, ref format);

                newApp.Quit();

            }
            catch (Exception ex)
            {
                err=ex.ToString();
            }
            finally
            {
                //освобождаем память, занятую объектами
                Marshal.ReleaseComObject(doc);
                Marshal.ReleaseComObject(d);
                Marshal.ReleaseComObject(newApp);                
            }
            return err;
        }

        public static void ReplaceWordsInDocByDic(string wordPath, Dictionary<string, string> confDic)
        {
            #region Переменные
            //заменить в файле word слова из словаря congfDic, где ключ это слово которое надо заменить, а значение это слово на которое надо заменить
            Word.Application wordApp = new Word.Application();
            Word.Document doc = new Word.Document();
            //переместить весь текст из файла Word в string
            string text = string.Empty; 
            #endregion
            
            try
            {
                //открыть файл Word
                doc = wordApp.Documents.Open(wordPath);

                //получить весь текст из документа
                text = doc.Content.Text;

                //заменить слова в тексте
                foreach (KeyValuePair<string, string> entry in confDic)
                {
                    text = text.Replace(entry.Key, entry.Value);
                }

                //очистить документ и вставить новый текст
                doc.Content.Text = text;

                //сохранить изменения и закрыть документ
                doc.Save();
                doc.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                //освободить ресурсы
                Marshal.ReleaseComObject(doc);
                Marshal.ReleaseComObject(wordApp);
            }
        }
    }
}
