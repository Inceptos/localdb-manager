using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlLocalDb;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace localdb_manager
{
    public partial class Main : Form
    {
        Panel fillPanel = new Panel { Dock = DockStyle.Fill };

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            RefreshInstances();
            this.Controls.Add(fillPanel);
            foreach (string version in SqlLocalDbApi.Versions.Reverse())
            {
                cbVersion.Items.Add(version);
            }
            cbVersion.SelectedIndex = 0;
            this.AutoScroll = true;
            tick2second.Start();
            lbWait.Font = new Font(lbWait.Font.FontFamily, lbWait.Font.Size + 2, lbWait.Font.Style);
        }

        public void RefreshInstances()
        {
            fillPanel.Controls.Clear();
            
            foreach (var item in ManageLocalDB.GetInstance)
            {
                Panel panel = new Panel
                {
                    Name = item.Name,
                    Dock = DockStyle.Top,
                    Size = new Size(this.Width, 60)
                };

                Label lb = new Label
                {
                    Text = $"{item.Name} (v{item.LocalDbVersion.ToString(2)})",
                    Dock = DockStyle.Left
                };

                Button delInstance = new Button
                {
                    Text = "Удалить",
                    Dock = DockStyle.Right,
                    ForeColor = Color.IndianRed
                };
                delInstance.Click += DelInstance_Click;
                Button startStop = new Button
                {
                    Dock = DockStyle.Right
                };

                if (item.IsRunning)
                {
                    startStop.Text = "Остановить";
                    startStop.ForeColor = Color.IndianRed;
                    startStop.Click += StopClick;
                }
                else
                {
                    startStop.Text = "Запустить";
                    startStop.ForeColor = Color.SeaGreen;
                    startStop.Click += StartClick;
                }

                panel.Controls.Add(lb);
                panel.Controls.Add(startStop);
                panel.Controls.Add(delInstance);

                fillPanel.Controls.Add(panel);
                
            }
            fillPanel.Update();
            lbWait.Visible = false;
        }

        private void StartClick(object sender, EventArgs e)
        {
            Button obj = (Button)sender;
            WaitLabel();
            SqlLocalDbApi.StartInstance(obj.Parent.Name);
            RefreshInstances();
        }

        private void StopClick(object sender, EventArgs e)
        {
            Button obj = (Button)sender;
            WaitLabel();
            SqlLocalDbApi.StopInstance(obj.Parent.Name);
            RefreshInstances();
        }

        private void DelInstance_Click(object sender, EventArgs e)
        {
            string nameInstance = ((Button)sender).Parent.Name;
            DialogResult answer =
            MessageBox.Show("Вы действительно хотите удалить данный экзмепляр?", $"Удаление {nameInstance}", MessageBoxButtons.YesNo);
            if (answer == DialogResult.Yes)
            {
                WaitLabel();
                SqlLocalDbApi.DeleteInstance(nameInstance, true);
                RefreshInstances();
            }

        }

        private void btnAddInstance_Click(object sender, EventArgs e)
        {
            if (tbName.Text.Length <= 1)
            {
                MessageBox.Show("Слишком короткое имя экземпляра", "Ошибка создания");
                tbName.Focus();
                return;
            }
            WaitLabel();
            SqlLocalDbApi.CreateInstance(tbName.Text, cbVersion.Text);
            tbName.Text = null;
            RefreshInstances();
        }

        private void tick2second_Tick(object sender, EventArgs e)
        {
            WaitLabel();
            RefreshInstances();
        }

        public void WaitLabel()
        {
            
            lbWait.Visible = true;
            lbWait.Update();
        }
    }
}