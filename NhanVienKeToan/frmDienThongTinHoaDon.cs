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
        public frmDienThongTinHoaDon(int manv, decimal giathue, int mahopdong, DateTime hdgn)
        {
            InitializeComponent();
            MaHopDong = mahopdong;
            MaNV = manv;
            TienPhong = giathue;
            HDGanNhat = hdgn;
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
            if ((dtpNgayCanLap.Value - HDGanNhat).TotalDays >= 32)
                MessageBox.Show("Tháng trước bạn vẫn chưa lập hóa đơn!\n Không thể lập hóa đơn cho tháng tiếp theo", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txtTienDien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
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
    }
}
