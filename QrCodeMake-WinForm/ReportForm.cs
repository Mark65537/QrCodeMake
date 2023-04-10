using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QrCodeMake_WinForm
{
    public partial class ReportForm : Form
    {
        public ReportForm(Dictionary<string, string> reportDic)
        {
            InitializeComponent();

            foreach (KeyValuePair<string, string> item in reportDic)
            {
                dGV_report.Rows.Add(item.Key, item.Value);
            }
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            //foreach (KeyValuePair<string, string> item in map)
            //{
            //    dGV_report.Rows.Add(item.Key, item.Value);
            //}
        }
    }
}
