using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Office.Interop.Excel;
using Net.Codecrete.QrCodeGenerator;
using Excel = Microsoft.Office.Interop.Excel;

namespace GeneralClassLibrary
{
    public class ExcelClass
    {
        private static readonly QrCode.Ecc[] _eCorLev = { QrCode.Ecc.Low, QrCode.Ecc.Medium, QrCode.Ecc.Quartile, QrCode.Ecc.High };

        public static List<Person> ExcelToPersons(string pathToExcel, int err = 1)
        {
            //переменные для Excel
            Excel.Application application = null;//не удалять
            Workbooks workbooks = null;
            Workbook workbook = null;
            Worksheet sheet = null;
            //переменные для Excel end
            List<Person> lpersons = new List<Person>();
            try
            {
                application = new Excel.Application(); //запуск программы excel
                workbooks = application.Workbooks;
                workbook = workbooks.Open(pathToExcel);//получаем доступ к первому листу                                               
                sheet = workbook.ActiveSheet;

                int y = 2;
                int x = 1;
                string[] keyWords = { "мероприятие", "форма участия", "фио", "почт" };
                Dictionary<string, List<int>> headers = FindHeaders(ref sheet, keyWords);

                foreach (string k in keyWords)
                    if (!headers.ContainsKey(k))
                    {
                        throw new Exception($"добавьте столбец содержащий имя {k}");
                    }                

                while (sheet.Cells[y, x].text != "")//проверка на пустую строку в vba
                {

                    Person person = new Person();
                    for (int f = 0; f < headers["форма участия"].Count; f++)
                    {
                        if (sheet.Cells[y, headers["форма участия"][f]].text.Equals("Очная"))
                        {
                            person.Events.Add(sheet.Cells[y, headers["мероприятие"][f]].text);
                        }
                        if ((f == headers["форма участия"].Count - 1) && person.Events.Count > 0)// если у нас есть очная 'форма участия' и есть события
                        {
                            person.Fio = sheet.Cells[y, headers["фио"][0]].text;
                            person.Email = sheet.Cells[y, headers["почт"][0]].text;
                            QrCode qr = QrCode.EncodeText(person.ToString(), _eCorLev[err]);
                            person.QrCode = qr.ToBitmap();

                            lpersons.Add(person);
                        }
                    }

                    y++;
                }
                application.Quit();//для выхода из приложения excel                
            }
            finally 
            { 

                //освобождаем память, занятую объектами
                Marshal.ReleaseComObject(application);
                Marshal.ReleaseComObject(workbooks);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(sheet);
            }

            
            return lpersons;
        }

        private static Dictionary<string, List<int>> FindHeaders(ref Worksheet sheet, string[] keys)
        {
            Dictionary<string, List<int>> dic= new Dictionary<string, List<int>>();

            int x = 1;

            while (sheet.Cells[1,x].text!="")
            {
                string text = sheet.Cells[1, x].text;
                text=text.ToLower();
                foreach (string k in keys)
                    if (text.Contains(k))
                    {                        
                        if (!dic.ContainsKey(k))// создаем список если ключ не найден
                            dic[k] = new List<int>();
                        
                        dic[k].Add(x);// добавляем в список по существующему ключу
                        break;
                    }
                x++;
            }
            return dic;
        }

        public static List<Bitmap> ExcelTo(string arg, string format="png", int err=1)
        {
            //переменные для Excel
             Application application = null;
             Workbooks workbooks = null;
             Workbook workbook = null;
             //Sheets sheets = null;
             Worksheet sheet = null;
             //Range cell = null;
            //переменные для Excel end
            List<Bitmap> bitmaps= new List<Bitmap>();
            try
            {
                application = new Application(); //запуск программы excel
                workbooks = application.Workbooks;
                workbook = workbooks.Open(arg);//получаем доступ к первому листу                                               
                sheet = workbook.ActiveSheet;

                int y = 2;
                int x = 1;
                StringBuilder strQrCode = new StringBuilder();

                while (sheet.Cells[y, x].text != "")//проверка на пустую строку в vba
                {
                    while (sheet.Cells[y, x].text != "")//проверка на пустую строку в vba
                    {
                        strQrCode.Append(sheet.Cells[y, x].text);
                        strQrCode.Append(" ");
                        x++;
                    }

                    QrCode qr = QrCode.EncodeText(strQrCode.ToString(), _eCorLev[err]);
                    switch (format) {
                        case "png":
                            qr.SaveAsPng($"{strQrCode}.png", 10, 3);
                            break;
                        case "bitmap":
                            bitmaps.Add(qr.ToBitmap(10, 3));
                            break;
                    }

                    

                    strQrCode.Clear();

                    y++;
                    x = 1;
                }
                //surcell = sheet.Cells[2,1];
                //namecell = sheet.Cells[2, 1];
                //parcell = sheet.Cells[2, 1];
                //orgcell = sheet.Cells[2, 1];


                application.Quit();//для выхода из приложения excel
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                //освобождаем память, занятую объектами
                Marshal.ReleaseComObject(application);
                Marshal.ReleaseComObject(workbooks);
                Marshal.ReleaseComObject(workbook);
                //Marshal.ReleaseComObject(sheets);
                Marshal.ReleaseComObject(sheet);

            }
            return bitmaps;
        }
        public static List<Bitmap> ExcelToPngQrcodes(string arg)
        {
            return ExcelTo(arg,"png");
        }

        public static List<Bitmap> ExcelToBitmapQrcodes(string arg, int err=1)
        {
            return ExcelTo(arg, "bitmap", err);
        }

    }
}
