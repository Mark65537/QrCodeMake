using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            Word.Application newApp = new Word.Application();
            Word.Documents d = newApp.Documents;
            object Unknown = Type.Missing;
            Word.Document doc = d.Open(ref Sourcepath);

            object format = Word.WdSaveFormat.wdFormatHTML;

            doc.WebOptions.Encoding = Microsoft.Office.Core.MsoEncoding.msoEncodingUTF8;

            newApp.ActiveDocument.SaveAs(ref TargetPath, ref format);
            
            newApp.Documents.Close(Word.WdSaveOptions.wdDoNotSaveChanges);

        }
    }
}
