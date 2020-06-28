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
    public partial class frmDienThongTinHopDong : Form
    {
        private static int MaNV {get;set;}
        private static int MaKH { get; set; }
        private static decimal GiaThue { get; set; }
        private static int MaPhieu { get; set; }
        public frmDienThongTinHopDong(int manv, decimal giathue, int maphieu, int makh)
        {
            InitializeComponent();
            MaNV = manv;
            GiaThue = giathue;
            MaPhieu = maphieu;
            MaKH = makh;
        }
        BUSHopDong bushopdong;
        BUSHoaDon bushoadon;
        BUSChiTietHoaDon buscthoadon;
        private void frmDienThongTinHopDong_Load(object sender, EventArgs e)
        {
            bushopdong = new BUSHopDong();
            buscthoadon = new BUSChiTietHoaDon();
            bushoadon = new BUSHoaDon();
        }

        eHopDong TaoHopDong()
        {
            eHopDong hd = new eHopDong();
            hd.MaNV = MaNV;
            hd.MaKH = MaKH;
            hd.MaPhieuKTra = MaPhieu;
            hd.NgayTao = DateTime.Now;
            hd.NgayThue = dtpNgayThue.Value;
            hd.NgayTra = dtpNgayTra.Value;
            hd.NgayTraThucTe = dtpNgayTra.Value;
            hd.TienCoc = GiaThue * 3;
            hd.TinhTrangHD = true;
            return hd;

        }

        eHoaDon TaoHoaDon(int maHopDong)
        {
            eHoaDon hd = new eHoaDon();
            hd.MaHopDong = maHopDong;
            hd.MaNV = MaNV;
            hd.NgayCanLap = dtpNgayThue.Value;
            hd.NgayLapHoaDon = DateTime.Now;
            hd.TinhTrangHD = false;
            hd.NgayThanhToan = dtpNgayThue.Value;
            return hd;
        }

        eChiTietHoaDon TaoCTHD(int maHD)
        {
            eChiTietHoaDon cthd = new eChiTietHoaDon();
            cthd.MaHoaDon = maHD;
            cthd.PhiBaoTri = 0;
            cthd.PhiBaoVe = 0;
            cthd.PhiThangMay = 0;
            cthd.PhiVeSinh = 0;
            cthd.TienDien = 0;
            cthd.TienGuiXe = 0;
            cthd.TienNuoc = 0;
            cthd.TienPhong = GiaThue;
            return cthd;
        }
        


        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult hoiThoat = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (hoiThoat == DialogResult.Yes)
                this.Close();
        }

        private void btnTaoHopDong_Click(object sender, EventArgs e)
        {
            DialogResult hoithem = MessageBox.Show("Bạn có chắc chắn muốn tạo hợp đồng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if(hoithem == DialogResult.Yes)
            {
                eHopDong hd = TaoHopDong();
                bushopdong.TaoHopDong(hd);
                eHoaDon hoadon = TaoHoaDon(hd.MaHopDong);
                bushoadon.ThemHoaDon(hoadon);
                eChiTietHoaDon cthd = TaoCTHD(hoadon.MaHoaDon);
                buscthoadon.ThemCTHoaDon(cthd);
                MessageBox.Show("Tạo hợp đồng thành công", "Thông báo");
                this.DialogResult = DialogResult.OK;
            }    
        }

        
    }
}
