using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities;
using DAL;
using BUS;

namespace Admin
{
    public partial class frmThemNhanVien : Form
    {
        public frmThemNhanVien()
        {
            InitializeComponent();
        }
        BUSNhanVienVaTaiKhoan busnvk;
        ErrorProvider ep;
        private void frmThemNhanVien_Load(object sender, EventArgs e)
        {
            busnvk = new BUSNhanVienVaTaiKhoan();
            ItemsCboChucVu(cboChucVu);
            ep = new ErrorProvider();
        }

        void ItemsCboChucVu(KryptonComboBox cbo)
        {
            List<clsChucVu> list = new List<clsChucVu>()
            {
                new clsChucVu {Ten = "Nhân Viên Tư Vấn", Value = 1},
                new clsChucVu {Ten = "Nhân Viên Kỹ Thuật", Value = 2},
                new clsChucVu {Ten = "Nhân Viên Kế Toán", Value = 3}
            };
            cbo.DataSource = list;
            cbo.DisplayMember = "Ten";
            cbo.ValueMember = "Value";
            cbo.SelectedItem = list.FirstOrDefault();
        }

        eNhanVien TaoNV()
        {
            eNhanVien nv = new eNhanVien();
            nv.Active = true;
            nv.ChucVu = Convert.ToInt32(cboChucVu.SelectedValue);
            nv.DiaChi = rtxtDiaChi.Text;
            nv.Email = txtEmail.Text;
            nv.NgaySinh = dtmNgaySinh.Value;
            nv.QueQuan = rtxtQueQuan.Text;
            nv.SDT = txtSoDT.Text;
            nv.SoCMND = txtSoCMND.Text;
            nv.TenNV = txtHoTen.Text;
            if (rdoNam.Checked == true)
                nv.GioiTinh = true;
            else
                nv.GioiTinh = false;
            return nv;
        }

        eTaiKhoan TaoTK(int maNV)
        {
            eTaiKhoan tk = new eTaiKhoan();
            tk.TenTK = txtTenTK.Text;
            tk.MaNV = maNV;
            tk.MatKhau = txtMatKhau.Text;
            return tk;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if ((DateTime.Now - dtmNgaySinh.Value).TotalDays < 18 * 365 + 4)
                MessageBox.Show("Nhân viên cần thuê chưa đủ tuổi đi làm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtHoTen.Text.Trim().Length == 0 || txtEmail.Text.Trim().Length == 0 || txtMatKhau.Text.Trim().Length == 0 || txtSoCMND.Text.Trim().Length == 0 || txtSoDT.Text.Trim().Length == 0 || txtTenTK.Text.Trim().Length == 0 || txtXacNhanMk.Text.Trim().Length == 0 || rtxtDiaChi.Text.Trim().Length == 0 || rtxtQueQuan.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (!txtEmail.Text.KtraEmail())
                ep.SetError(txtEmail, "Email phải đúng định dang bắt đầu bằng chữ cái. Ex: abc123@gmail.com");
            else if (!txtTenTK.Text.KtraTenTK())
                ep.SetError(txtTenTK, "Tài khoản tối thiểu phải có 5 kí tự, bắt đầu bằng chữ cái. Ex: admin");
            else if (!txtSoCMND.Text.KtraSCMND())
                ep.SetError(txtSoCMND, "Số chứng minh(căn cước) phải là số, có 9 hoặc 12 số, bắt đầu bằng 1-9");
            else if (!txtSoDT.Text.KtraSDT())
                ep.SetError(txtSoDT, "Số điện thoại phải là số và có 10 số, bắt đầu bằng 01, 03, 05, 07 hoặc 09");
            else
            {
                eTaiKhoan ktratk = busnvk.KiemTraTKTonTai(txtTenTK.Text);
                if (ktratk != null)
                    MessageBox.Show("Tên tài khoản đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (!txtMatKhau.Text.Equals(txtXacNhanMk.Text))
                    MessageBox.Show("Mật khẩu xác thực không đúng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DialogResult hoi = MessageBox.Show("Bạn có chắc chắn muốn thêm nhân viên này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (hoi == DialogResult.Yes)
                    {
                        eNhanVien nv = TaoNV();
                        busnvk.ThemNhanVien(nv);
                        eTaiKhoan tk = TaoTK(nv.MaNV);
                        busnvk.ThemTK(tk);
                        MessageBox.Show("Thêm thông tin nhân viên thành công", "Thông báo");
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
        }

        private void txtSoDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtSoCMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtSoCMND_TextChanged(object sender, EventArgs e)
        {
            if (txtSoCMND.Text.KtraSCMND())
                ep.Clear();
        }

        private void txtSoDT_TextChanged(object sender, EventArgs e)
        {
            if (txtSoDT.Text.KtraSDT())
                ep.Clear();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (txtEmail.Text.KtraEmail())
                ep.Clear();
        }

        private void txtTenTK_TextChanged(object sender, EventArgs e)
        {
            if (txtTenTK.Text.KtraTenTK())
                ep.Clear();
        }
    }
}
