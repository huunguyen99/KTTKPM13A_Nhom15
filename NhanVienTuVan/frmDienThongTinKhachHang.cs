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
        private static int MaNV;
        private static decimal GiaThue;
        private static int MaPhieu;
        public frmDienThongTinKhachHang(int manv, int maphieu, decimal giathue)
        {
            InitializeComponent();
            MaNV = manv;
            GiaThue = giathue;
            MaPhieu = maphieu;
        }
        BUSKhachHang buskh;
        private void frmDienThongTinKhachHang_Load(object sender, EventArgs e)
        {
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
            DialogResult hoithem = MessageBox.Show("bạn có chắc chắn muốn thêm thông tin khách hàng này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            {
                if(hoithem == DialogResult.Yes)
                {
                    eKhachHang kh = TaoKH();
                    bool kq = buskh.ThemKH(kh);
                    if(kq == false)
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult hoiThoat = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (hoiThoat == DialogResult.Yes)
                this.Close();
        }
    }
}
