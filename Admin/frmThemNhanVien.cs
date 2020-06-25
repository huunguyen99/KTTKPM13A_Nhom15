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
        private void frmThemNhanVien_Load(object sender, EventArgs e)
        {
            busnvk = new BUSNhanVienVaTaiKhoan();
            ItemsCboChucVu(cboChucVu);
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
            if ((DateTime.Now - dtmNgaySinh.Value).TotalDays < 18 * 365)
                MessageBox.Show("Nhân viên cần thuê chưa đủ tuổi đi làm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        
    }
}
