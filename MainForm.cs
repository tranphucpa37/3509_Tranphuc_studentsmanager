using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quản_Lý_Sinh_viên
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SelectTab(0);
        }
        private void SelectTab(int idx)
        {
            panelMain.Controls.Clear();

            // style buttons
            Color activeBg = ColorTranslator.FromHtml("#4A4AFF");
            Color inactiveBg = Color.White;
            Color activeFore = Color.White;
            Color inactiveFore = Color.Black;

            if (idx == 0)
            {
                btnQuanLi.BackColor = activeBg;
                btnQuanLi.ForeColor = activeFore;
                btnBangDiem.BackColor = inactiveBg;
                btnBangDiem.ForeColor = inactiveFore;

                var uc = new UC_QuanLiSinhVien();
                uc.Dock = DockStyle.Fill;
                panelMain.Controls.Add(uc);
            }
            else
            {
                btnBangDiem.BackColor = activeBg;
                btnBangDiem.ForeColor = activeFore;
                btnQuanLi.BackColor = inactiveBg;
                btnQuanLi.ForeColor = inactiveFore;

                var uc = new UC_BangDiem();
                uc.Dock = DockStyle.Fill;
                panelMain.Controls.Add(uc);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    
        
        private void btnQuanLi_Click(object sender, EventArgs e)
        {
            SelectTab(0);
        }

        private void btnBangDiem_Click(object sender, EventArgs e)
        {
            SelectTab(1);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mainpanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
