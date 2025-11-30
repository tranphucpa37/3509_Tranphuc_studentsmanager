using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Utils.HashCodeHelper;

namespace Quản_Lý_Sinh_viên
{
    public partial class UC_QuanLiSinhVien : UserControl
    {
        public UC_QuanLiSinhVien()
        {
            InitializeComponent();
            dgvSinhVien.CellContentClick += dgvSinhVien_CellContentClick;
            dgvSinhVien.AutoGenerateColumns = false;
            dgvSinhVien.CellMouseClick += dgvSinhVien_CellMouseClick;

            this.Load += UC_QuanLySinhVien_Load;   
        }

        private int editingRowIndex = -1;
        DataTable dtSinhVienGoc = new DataTable();

        private void LoadDanhSach()
        {
            dtSinhVienGoc = GetDataSinhVien();
            dgvSinhVien.DataSource = dtSinhVienGoc;

            // Ẩn cột phụ
            if (dgvSinhVien.Columns["HoTenLower"] != null)
                dgvSinhVien.Columns["HoTenLower"].Visible = false;
        }

        private DataTable GetDataSinhVien()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaSV");
            dt.Columns.Add("HoTen");
            dt.Columns.Add("Lop");
            dt.Columns.Add("NgaySinh");

            // Thêm cột phụ (không hiển thị lên DataGridView)
            dt.Columns.Add("HoTenLower");

            // Dữ liệu mẫu (bạn có thể thay bằng DB)
            dt.Rows.Add("SV01", "Nguyễn Văn A", "TPM1", "2005-01-10");
            dt.Rows.Add("SV02", "Lê Thị B", "TPM2", "2004-12-20");
            dt.Rows.Add("SV03", "Trần Minh C", "TPM1", "2005-04-15");
            dt.Rows.Add("SV04", "Huỳnh An", "TPM3", "2004-09-12");

            foreach (DataRow r in dt.Rows)
            {
                r["HoTenLower"] = r["HoTen"].ToString().ToLower();
            } 

            return dt;
        }
    







        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            panelAddStudent.Visible = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panelAddStudent.Visible = false;

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();
            txtMaSV.Clear();
            dtNgaySinh.Value = DateTime.Now;
            cbLop.SelectedIndex = -1;
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
        }



       private void btnAdd_Click(object sender, EventArgs e)
{
    // Nếu đang sửa sinh viên
    if (editingRowIndex >= 0)
    {
        dgvSinhVien.Rows[editingRowIndex].Cells["MaSV"].Value = txtMaSV.Text;
        dgvSinhVien.Rows[editingRowIndex].Cells["HoTen"].Value = txtHoTen.Text;
        dgvSinhVien.Rows[editingRowIndex].Cells["Lop"].Value = cbLop.Text;
        dgvSinhVien.Rows[editingRowIndex].Cells["NgaySinh"].Value = dtNgaySinh.Value.ToString("yyyy-MM-dd");

        editingRowIndex = -1;
        panelAddStudent.Visible = false;

        MessageBox.Show("Sửa sinh viên thành công!");
        return;
    }

    // THÊM SINH VIÊN MỚI
    DataRow r = dtSinhVienGoc.NewRow();
    r["MaSV"] = txtMaSV.Text;
    r["HoTen"] = txtHoTen.Text;
    r["Lop"] = cbLop.Text;
    r["NgaySinh"] = dtNgaySinh.Value.ToString("yyyy-MM-dd");
    r["HoTenLower"] = txtHoTen.Text.ToLower();

    dtSinhVienGoc.Rows.Add(r);

    LocSinhVien();   // cập nhật lại lưới

    panelAddStudent.Visible = false;
    MessageBox.Show("Thêm sinh viên thành công!");
}



        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var rect = this.ClientRectangle;
            int radius = 15;
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);

                this.Region = new Region(path);
            }
        }
        private void UC_QuanLySinhVien_Load(object sender, EventArgs e)
        {
            LoadDanhSach();

            // Load danh sách lớp vào combo
            cboLopTim.Items.Clear();
            cboLopTim.Items.Add("");      // để tìm tất cả
            cboLopTim.Items.Add("TPM1");
            cboLopTim.Items.Add("TPM2");
            cboLopTim.Items.Add("TPM3");
            
        }


        private void dgvSinhVien_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex < 0) return;

            if (dgvSinhVien.Columns[e.ColumnIndex].Name == "Actions")
            {
                e.PaintBackground(e.CellBounds, true);

                int iconSize = 18;
                int padding = 5;

                // Icon Edit
                var editIcon = Properties.Resources.edit;
                Rectangle rectEdit = new Rectangle(
                    e.CellBounds.Left + padding,
                    e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2,
                    iconSize, iconSize);
                e.Graphics.DrawImage(editIcon, rectEdit);

                // Icon Delete
                var deleteIcon = Properties.Resources.delete;
                Rectangle rectDelete = new Rectangle(
                    e.CellBounds.Left + padding + iconSize + 10,
                    e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2,
                    iconSize, iconSize);
                e.Graphics.DrawImage(deleteIcon, rectDelete);

                e.Handled = true;
            }
        }




        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvSinhVien.Columns[e.ColumnIndex].Name == "colDelete")
            {
                dgvSinhVien.Rows.RemoveAt(e.RowIndex);
            }

            if (dgvSinhVien.Columns[e.ColumnIndex].Name == "colEdit")
            {
                MessageBox.Show("Edit clicked!");
            }
        }
        private void dgvSinhVien_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Chỉ xử lý nếu click vào cột Actions
            if (dgvSinhVien.Columns[e.ColumnIndex].Name != "Actions")
                return;

            int padding = 5;
            int iconSize = 16;

            // Lấy toạ độ click trong cell
            int mouseX = e.Location.X;

            // Tính vị trí icon Edit
            int editX = padding;

            // Tính vị trí icon Delete
            int deleteX = padding * 2 + iconSize;

            // --- CLICK EDIT ---
            if (mouseX >= editX && mouseX <= editX + iconSize)
            {
                editingRowIndex = e.RowIndex;

                // Gán dữ liệu lên form
                txtMaSV.Text = dgvSinhVien.Rows[e.RowIndex].Cells["MaSV"].Value.ToString();
                txtHoTen.Text = dgvSinhVien.Rows[e.RowIndex].Cells["HoTen"].Value.ToString();
                cbLop.Text = dgvSinhVien.Rows[e.RowIndex].Cells["Lop"].Value.ToString();
                dtNgaySinh.Value = DateTime.Parse(dgvSinhVien.Rows[e.RowIndex].Cells["NgaySinh"].Value.ToString());

                panelAddStudent.Visible = true;
                return;
            }

            // --- CLICK DELETE ---
            if (mouseX >= deleteX && mouseX <= deleteX + iconSize)
            {
                var confirm = MessageBox.Show("Bạn có chắc muốn xoá sinh viên này?",
                                              "Xác nhận xoá",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    dgvSinhVien.Rows.RemoveAt(e.RowIndex);
                }

                return;
            }
        }







        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LocSinhVien();
            string keyword = txtTimKiem.Text.Trim().ToLower();

            foreach (DataGridViewRow row in dgvSinhVien.Rows)
            {
                if (row.IsNewRow) continue;

                string masv = row.Cells["MaSV"].Value.ToString().ToLower();
                string hoten = row.Cells["HoTen"].Value.ToString().ToLower();
                string lop = row.Cells["Lop"].Value.ToString().ToLower();

                // Ghép tất cả lại để tìm
                bool match =
                    masv.Contains(keyword) ||
                    hoten.Contains(keyword) ||
                    lop.Contains(keyword);

                row.Visible = match;
            }
        }


        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();

            foreach (DataGridViewRow row in dgvSinhVien.Rows)
            {
                row.Visible = true;
            }
        }

        private void cboLopTim_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocSinhVien();
        }
        private void LocSinhVien()
        {
            if (dtSinhVienGoc == null || dtSinhVienGoc.Rows.Count == 0)
                return;

            string ten = txtTimKiem.Text.Trim().ToLower();
            string lop = cboLopTim.Text.Trim();

            DataView dv = dtSinhVienGoc.DefaultView;

            string filter = "1=1";

            if (!string.IsNullOrEmpty(lop))
                filter += $" AND Lop = '{lop}'";

            if (!string.IsNullOrEmpty(ten))
                filter += $" AND HoTenLower LIKE '%{ten}%'";

            dv.RowFilter = filter;

            dgvSinhVien.DataSource = dv;
        }

    }

}    




