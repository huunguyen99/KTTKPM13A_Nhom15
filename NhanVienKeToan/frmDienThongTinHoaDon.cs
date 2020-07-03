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
using BUS;
using DAL;

namespace NhanVienKeToan
{
    public partial class frmDienThongTinHoaDon : Form
    {
        private static int MaNV { get; set; }
        private static decimal TienPhong { get; set; }
        private static int MaHopDong { get; set; }
        private static DateTime HDGanNhat { get; set; }
        ErrorProvider ep;
        public frmDienThongTinHoaDon(int manv, decimal giathue, int mahopdong, DateTime hdgn)
        {
            InitializeComponent();
            MaHopDong = mahopdong;
            MaNV = manv;
            TienPhong = giathue;
            HDGanNhat = hdgn;
            ep = new ErrorProvider();
        }
        BUSChiTietHoaDon buscthd;
        BUSHoaDon bushd;
        private void frmDienThongTinHoaDon_Load(object sender, EventArgs e)
        {
            buscthd = new BUSChiTietHoaDon();
            bushd = new BUSHoaDon();
        }

        eHoaDon TaoHD()
        {
            eHoaDon hd = new eHoaDon();
            hd.MaHopDong = MaHopDong;
            hd.MaNV = MaNV;
            hd.NgayCanLap = dtpNgayCanLap.Value;
            hd.NgayLapHoaDon = DateTime.Now;
            hd.TinhTrangHD = false;
            hd.NgayThanhToan = dtpNgayCanLap.Value;
            return hd;
        }

        eChiTietHoaDon TaoCTHD(int maHD)
        {
            eChiTietHoaDon cthd = new eChiTietHoaDon();
            cthd.MaHoaDon = maHD;
            cthd.PhiBaoTri = Convert.ToDecimal(txtPhiBaoTri.Text);
            cthd.PhiBaoVe = Convert.ToDecimal(txtPhiBaoVe.Text);
            cthd.PhiThangMay = Convert.ToDecimal(txtPhiThangMay.Text);
            cthd.PhiVeSinh = Convert.ToDecimal(txtPhiVeSinh.Text);
            cthd.TienDien = Convert.ToDecimal(txtTienDien.Text);
            cthd.TienGuiXe = Convert.ToDecimal(txtTienGuiXe.Text);
            cthd.TienNuoc = Convert.ToDecimal(txtTienNuoc.Text);
            cthd.TienPhong = TienPhong;
            return cthd;
        }
        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {
            decimal tiendien;
            decimal tiennuoc;
            decimal tienguixe;
            decimal phibaotri;
            decimal phivesinh;
            decimal phithangmay;
            decimal phibaove;
            if (txtTienDien.Text.Trim().Length == 0 || txtTienNuoc.Text.Trim().Length == 0 || txtTienGuiXe.Text.Trim().Length == 0 || txtPhiBaoTri.Text.Trim().Length == 0 || txtPhiBaoVe.Text.Trim().Length == 0 || txtPhiThangMay.Text.Trim().Length == 0 || txtPhiVeSinh.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else
            {
                try
                {
                    tiendien = Convert.ToDecimal(txtTienDien.Text);
                    tiennuoc = Convert.ToDecimal(txtTienNuoc.Text);
                    tienguixe = Convert.ToDecimal(txtTienGuiXe.Text);
                    phibaotri = Convert.ToDecimal(txtPhiBaoTri.Text);
                    phivesinh = Convert.ToDecimal(txtPhiVeSinh.Text);
                    phithangmay = Convert.ToDecimal(txtPhiThangMay.Text);
                    phibaove = Convert.ToDecimal(txtPhiBaoVe.Text);
                    if (tiendien >= 100000000)
                        ep.SetError(txtTienDien, "Các khoản phí không thể quá 100 triệu");
                    else if (tiennuoc >= 100000000)
                        ep.SetError(txtTienGuiXe, "Các khoản phí không thể quá 100 triệu");
                    else if (tienguixe >= 100000000)
                        ep.SetError(txtTienNuoc, "Các khoản phí không thể quá 100 triệu");
                    else if (phibaotri >= 100000000)
                        ep.SetError(txtPhiBaoTri, "Các khoản phí không thể quá 100 triệu");
                    else if (phibaove >= 100000000)
                        ep.SetError(txtPhiBaoVe, "Các khoản phí không thể quá 100 triệu");
                    else if (phithangmay >= 100000000)
                        ep.SetError(txtPhiThangMay, "Các khoản phí không thể quá 100 triệu");
                    else if (phivesinh >= 100000000)
                        ep.SetError(txtPhiVeSinh, "Các khoản phí không thể quá 100 triệu");
                    else
                    {
                        DialogResult hoi = MessageBox.Show("Bạn có chắc chắn muốn thêm hóa đơn này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (hoi == DialogResult.Yes)
                        {
                            eHoaDon hd = TaoHD();
                            bushd.ThemHoaDon(hd);
                            eChiTietHoaDon cthd = TaoCTHD(hd.MaHoaDon);
                            buscthd.ThemCTHoaDon(cthd);
                            MessageBox.Show("Thêm hóa đơn thành công", "Thông báo");
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Số tiền quá lớn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtTienDien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57)))
                e.Handled = true;
        }

        private void txtTienNuoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtTienGuiXe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtPhiBaoTri_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtPhiVeSinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtPhiThangMay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtPhiBaoVe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult hoiThoat = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (hoiThoat == DialogResult.Yes)
                this.Close();
        }

        private void txtTienDien_TextChanged(object sender, EventArgs e)
        {
            if (txtTienDien.Text.KTraTien())
                ep.Clear();
        }

        private void txtTienNuoc_TextChanged(object sender, EventArgs e)
        {
            if (txtTienNuoc.Text.KTraTien())
                ep.Clear();
        }

        private void txtTienGuiXe_TextChanged(object sender, EventArgs e)
        {
            if (txtTienGuiXe.Text.KTraTien())
                ep.Clear();
        }

        private void txtPhiBaoTri_TextChanged(object sender, EventArgs e)
        {
            if (txtPhiBaoTri.Text.KTraTien())
                ep.Clear();
        }

        private void txtPhiVeSinh_TextChanged(object sender, EventArgs e)
        {
            if (txtPhiVeSinh.Text.KTraTien())
                ep.Clear();
        }

        private void txtPhiThangMay_TextChanged(object sender, EventArgs e)
        {
            if (txtPhiThangMay.Text.KTraTien())
                ep.Clear();
        }

        private void txtPhiBaoVe_TextChanged(object sender, EventArgs e)
        {
            if (txtPhiBaoVe.Text.KTraTien())
                ep.Clear();
        }
    }
}
