using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using Microsoft.Office.Interop.Excel;
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

        

        public static void ConvertDocToHtml(object Sourcepath, object TargetPath)
        {
            Word.Application newApp=null;
            Word.Documents d=null;
            Word.Document doc=null;

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
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                //освобождаем память, занятую объектами
                Marshal.ReleaseComObject(newApp);
                Marshal.ReleaseComObject(d);
                Marshal.ReleaseComObject(doc);
            }
        }
    }
}
