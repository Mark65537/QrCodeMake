using GeneralClassLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QrCodeMake_WinForm.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QrCodeMake_WinForm
{
    public partial class OptionForm : Form
    {
        public OptionForm(string confPath)
        {
            InitializeComponent();

            // Читаем JSON файл
            var jsonString = File.ReadAllText(confPath);

            // Десериализуем JSON строку в объект
            var jsonObject = JObject.Parse(jsonString);
            foreach (var jo in jsonObject)
            {
                dGV_changeable.Rows.Add(jo.Key, jo.Value["val"], jo.Value["desc"]);
            }
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            //Dictionary<string, string> confDic = HtmlClass.ReadCfg();

        }

        private void b_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
