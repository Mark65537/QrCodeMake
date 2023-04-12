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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;

namespace QrCodeMake_WinForm
{
    public partial class OptionForm : Form
    {
        string confPath;
        public OptionForm(string confPath)
        {
            try
            {
                InitializeComponent();

                this.confPath = confPath;
                // Читаем JSON файл
                var jsonString = File.ReadAllText(confPath);

                // Десериализуем JSON строку в объект
                var jsonObject = JObject.Parse(jsonString);
                int key;
                foreach (var jo in jsonObject)
                {
                    if (int.TryParse(jo.Key, out key))
                    {
                        dGV_unChangeable.Rows.Add(key, jo.Value["val"], jo.Value["desc"]);                        
                    }
                    else
                    {
                        dGV_changeable.Rows.Add(jo.Key, jo.Value["val"], jo.Value["desc"]);
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void SettingForm_Load(object sender, EventArgs e)
        {
            MinimumSize = Size;//для того что бы нельзя было уменьшить форму
        }

        private void b_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void b_save_Click(object sender, EventArgs e)
        {
            try
            {
                //можно ли упростить
                JObject jsonContent = new JObject();

                SaveDataFromDataGridView(dGV_changeable.Rows, jsonContent);
                SaveDataFromDataGridView(dGV_unChangeable.Rows, jsonContent);
                
                File.WriteAllText(confPath, jsonContent.ToString());

                MessageBox.Show("Данные успешно сохранены в файл.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveDataFromDataGridView(DataGridViewRowCollection rows, JObject jsonContent)
        {
            //можно ли упростить, не используя LINQ
            foreach (DataGridViewRow row in rows)
            {
                if (row.Cells[0].Value != null)
                {
                    string key = row.Cells[0].Value.ToString();
                    string val = row.Cells[1].Value?.ToString() ?? "";
                    string desc = row.Cells[2].Value?.ToString() ?? "";
                    JObject newContent = new JObject(
                        new JProperty("val", val),
                        new JProperty("desc", desc)
                    );
                    jsonContent[key] = newContent;
                }
            }
        }
    }
}
