using BUS;
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
    public partial class frmDoiMatKhau : Form
    {
        private static int MaNV;
        public frmDoiMatKhau(int manv)
        {
            InitializeComponent();
            MaNV = manv;
        }
        BUSNhanVienVaTaiKhoan busnv;
        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
            busnv = new BUSNhanVienVaTaiKhoan();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (txtMatKhauMoi.Text.Trim().Length == 0 || txtMatKhauHienTai.Text.Trim().Length == 0 || txtXacNhanMatKhauMoi.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (txtMatKhauMoi.Text.Equals(txtXacNhanMatKhauMoi.Text) == false)
                    MessageBox.Show("Mật khẩu xác nhận không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DialogResult hoi = MessageBox.Show("Bạn có chắc chắn muốn thay đổi mật khẩu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (hoi == DialogResult.Yes)
                    {
                        bool kq = busnv.DoiMatKhau(MaNV, txtMatKhauHienTai.Text, txtMatKhauMoi.Text);
                        if (kq == false)
                            MessageBox.Show("Mật khẩu không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            MessageBox.Show("Đổi mật khẩu thành công", "Thông báo");
                            this.Close();
                        }
                    }
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult hoiThoat = MessageBox.Show("Bạn có chắc chắn muốn hủy không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (hoiThoat == DialogResult.Yes)
                this.Close();
        }
    }
}
