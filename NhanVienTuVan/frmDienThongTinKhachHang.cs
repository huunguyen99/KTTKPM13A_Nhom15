using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using Entities;
using BUS;

namespace NhanVienTuVan
{
    public partial class frmDienThongTinKhachHang : Form
    {
        private static int MaNV { get; set; }
        private static decimal GiaThue { get; set; }
        private static int MaPhieu { get; set; }
        public frmDienThongTinKhachHang(int manv, int maphieu, decimal giathue)
        {
            InitializeComponent();
            MaNV = manv;
            GiaThue = giathue;
            MaPhieu = maphieu;
        }
        BUSKhachHang buskh;
        ErrorProvider ep;
        private void frmDienThongTinKhachHang_Load(object sender, EventArgs e)
        {
            ep = new ErrorProvider();
            buskh = new BUSKhachHang();
        }

        eKhachHang TaoKH()
        {
            eKhachHang kh = new eKhachHang();
            kh.Active = true;
            kh.TenKH = txtHoTen.Text;
            kh.Email = txtEmail.Text;
            kh.SDT = txtSDT.Text;
            kh.NgaySinh = dtpNgaySinh.Value;
            kh.SoCMND = txtSoCMND.Text;
            kh.DiaChi = rtxtDiaChi.Text;
            if (rdoNam.Checked == true)
                kh.GioiTinh = true;
            else
                kh.GioiTinh = false;
            return kh;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if ((DateTime.Now - dtpNgaySinh.Value).TotalDays < 18*365 + 4)
                MessageBox.Show("Khách hàng chưa đủ tuổi thuê phòng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtHoTen.Text.Trim().Length == 0 || txtEmail.Text.Trim().Length == 0 || txtSoCMND.Text.Trim().Length == 0 || txtSDT.Text.Trim().Length == 0 || rtxtDiaChi.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (!txtEmail.Text.KtraEmail())
                ep.SetError(txtEmail, "Email phải đúng định dang bắt đầu bằng chữ cái. Ex: abc123@gmail.com");
            else if (!txtSoCMND.Text.KtraSCMND())
                ep.SetError(txtSoCMND, "Số chứng minh(căn cước) phải là số, có 9 hoặc 12 số, bắt đầu bằng 1-9");
            else if (!txtSDT.Text.KtraSDT())
                ep.SetError(txtSDT, "Số điện thoại phải là số và có 10 số, bắt đầu bằng 01, 03, 05, 07 hoặc 09");
            else
            {
                DialogResult hoithem = MessageBox.Show("bạn có chắc chắn muốn thêm thông tin khách hàng này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                {
                    if (hoithem == DialogResult.Yes)
                    {
                        eKhachHang kh = TaoKH();
                        bool kq = buskh.ThemKH(kh);
                        if (kq == false)
                        {
                            MessageBox.Show("Khách hàng này đã từng thuê văn phòng ở đây", "Thông báo");
                        }
                        else
                        {
                            MessageBox.Show("Thêm thông tin khách hàng thành công", "Thông báo");
                            frmDienThongTinHopDong frmhd = new frmDienThongTinHopDong(MaNV, GiaThue, MaPhieu, kh.MaKH);
                            if (frmhd.ShowDialog() == DialogResult.OK)
                            {
                                this.DialogResult = DialogResult.OK;
                            }
                        }
                    }
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult hoiThoat = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (hoiThoat == DialogResult.Yes)
                this.Close();
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {
            if (txtSDT.Text.KtraSDT())
                ep.Clear();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (txtEmail.Text.KtraEmail())
                ep.Clear();
        }
    }
}
