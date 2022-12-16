using GeneralClassLibrary;
using Microsoft.Office.Interop.Excel;
using Net.Codecrete.QrCodeGenerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeMake_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string filename = "C:\\Users\\User\\Desktop\\пример QR кода.html";
            //using (FileStream fs = File.OpenRead(filename))
            //{
            //    fs.Position = 0;
            //    fs.
            //}
            if (args.Length>0 && args[0]!=string.Empty)
            {

                ExcelClass.ExcelToPngQrcodes(args[0]);                 
            }
            else
            {
                Console.WriteLine($"QrCodeMake_Console version {Assembly.GetExecutingAssembly().GetName().Version}");
                Console.WriteLine("Usage: QrCodeMake_Console [путь к файлу]");
            }
        }
    }
}
