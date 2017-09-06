using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace localdb_manager
{
    public partial class BadForm : Form
    {
        public BadForm()
        {
            InitializeComponent();
        }

        private void link11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.microsoft.com/ru-ru/download/details.aspx?id=42299");
        }

        private void link13_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.microsoft.com/ru-ru/download/details.aspx%3Fid%3D29062");

        }
    }
}
