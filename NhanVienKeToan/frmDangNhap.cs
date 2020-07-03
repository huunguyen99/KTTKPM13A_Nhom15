using BUS;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhanVienKeToan
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }
        BUSNhanVienVaTaiKhoan busnv;
        eNhanVien nv;
        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            busnv = new BUSNhanVienVaTaiKhoan();
        }
        int demDN;
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            nv = busnv.LayNhanVienDangNhap(txtTaiKhoan.Text, txtMatKhau.Text);
            if (txtTaiKhoan.Text.Trim().Length == 0 || txtMatKhau.Text.Trim().Length == 0)
            {
                demDN++;
                MessageBox.Show("Vui lòng điền đầy đủ thông tin đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (nv != null)
            {
                frmMenu frmMN;
                if (nv.ChucVu == 4 || nv.ChucVu == 3)
                {
                    frmMN = new frmMenu(nv);
                    frmMN.Owner = this;
                    frmMN.Show();
                    this.Hide();
                    if (chkLuuMK.Checked == false)
                        txtMatKhau.Clear();
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền truy cập vào hệ thống này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    demDN++;
                }
            }
            else
            {
                demDN++;
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (demDN == 3)
            {
                MessageBox.Show("Bạn đã hết quyền đăng nhập! vui lòng truy cập lại!", "THÔNG BÁO");
                Application.Exit();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
