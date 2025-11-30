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
    public partial class UC_BangDiem : UserControl
    {
        public UC_BangDiem()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UC_BangDiem_Load(object sender, EventArgs e)
        {

        }

        private void btnNhapDiem_Click(object sender, EventArgs e)
        {
            panelnhapdiem.Visible = true;   // Hiển thị Form nhập điểm
         

        }

        private void cbosinhvien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
