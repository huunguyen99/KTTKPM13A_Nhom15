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

namespace NhanVienTuVan
{
    public partial class frmDanhSachTatCaKhachHang : Form
    {
        private static int MaNV { get; set; }
        private static decimal GiaThue { get; set; }
        private static int MaPhieu { get; set; }
        public frmDanhSachTatCaKhachHang(int manv, int maphieu, decimal giathue)
        {
            InitializeComponent();
            MaNV = manv;
            GiaThue = giathue;
            MaPhieu = maphieu;
        }
        BUSKhachHang buskh;
        List<eKhachHang> dskh;
        eKhachHang khChon;
        private void frmDanhSachTatCaKhachHang_Load(object sender, EventArgs e)
        {
            buskh = new BUSKhachHang();
            dskh = buskh.LayDSTatCaKhachHang();
            LoadDSKhachHangLenListView(dskh, lvwDSKhachHang);
            XuLyAutoComplete();
        }
        void ThemItem(eKhachHang k, ListView lvw)
        {
            ListViewItem lvwitem = new ListViewItem(k.MaKH.ToString());
            lvwitem.SubItems.Add(k.TenKH);
            lvwitem.SubItems.Add(k.SoCMND);
            lvwitem.SubItems.Add(k.NgaySinh.ToString("dd/MM/yyyy"));
            lvwitem.SubItems.Add(k.SDT);
            lvwitem.SubItems.Add(k.Email);
            lvwitem.SubItems.Add(k.DiaChi);
            if (k.GioiTinh == true)
                lvwitem.SubItems.Add("Nam");
            else
                lvwitem.SubItems.Add("Nữ");
            lvwitem.Tag = k;
            lvw.Items.Add(lvwitem);
        }

        void LoadDSKhachHangLenListView(List<eKhachHang> ds, ListView lvw)
        {
            lvw.Items.Clear();
            foreach (eKhachHang item in ds)
            {
                ThemItem(item, lvw);
            }
        }

        private void btnLapHopDong_Click(object sender, EventArgs e)
        {
            if (lvwDSKhachHang.SelectedItems.Count > 0)
            {
                DialogResult hoi = MessageBox.Show("Bạn có chắc chắn muốn tạo hợp đồng cho khách hàng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if(hoi == DialogResult.Yes)
                {
                    frmDienThongTinHopDong frmhd = new frmDienThongTinHopDong(MaNV, GiaThue, MaPhieu, khChon.MaKH);
                    if(frmhd.ShowDialog() == DialogResult.OK)
                    {
                        this.DialogResult = DialogResult.OK;
                    }    
                }    
            }
            else
                MessageBox.Show("Vui lòng chọn khách hàng cần thuê phòng", "Thông báo");
        }

        void TaiHienKHTuListView(eKhachHang kh)
        {
            txtEmail.Text = kh.Email;
            if (kh.GioiTinh == true)
                txtGioiTinh.Text = "Nam";
            else
                txtGioiTinh.Text = "Nữ";
            txtHoTen.Text = kh.TenKH;
            txtMaKH.Text = kh.MaKH.ToString();
            txtNgaySinh.Text = kh.NgaySinh.ToString("dd/MM/yyyy");
            rtxtDiaChi.Text = kh.DiaChi;
            txtSoCMND.Text = kh.SoCMND;
            txtSoDT.Text = kh.SDT;
        }
        private void lvwDSKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvwDSKhachHang.SelectedItems.Count > 0)
            {
                khChon = (eKhachHang)lvwDSKhachHang.SelectedItems[0].Tag;
                TaiHienKHTuListView(khChon);
            }    
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult hoiThoat = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (hoiThoat == DialogResult.Yes)
                this.Close();
        }

        void XuLyAutoComplete()
        {
            if(rdoTimTheoCMND.Checked == true)
            {
                txtGiaTriTim.AutoCompleteCustomSource.Clear();
                foreach(var item in dskh)
                {
                    txtGiaTriTim.AutoCompleteCustomSource.Add(item.SoCMND);
                }    
            }    
            else if(rdoTimTheoSDT.Checked == true)
            {
                txtGiaTriTim.AutoCompleteCustomSource.Clear();
                foreach (var item in dskh)
                {
                    txtGiaTriTim.AutoCompleteCustomSource.Add(item.SDT);
                }
            } 
            else if(rdoTimTheoTen.Checked == true)
            {
                txtGiaTriTim.AutoCompleteCustomSource.Clear();
                foreach (var item in dskh)
                {
                    txtGiaTriTim.AutoCompleteCustomSource.Add(item.TenKH);
                }
            }    
        }



        List<eKhachHang> TimKiemKH(string giaTriTim)
        {
            List<eKhachHang> dsTim = new List<eKhachHang>();
            if (rdoTimTheoCMND.Checked == true)
            {
                dsTim = dskh.Where(x => x.SoCMND.Contains(giaTriTim)).ToList();
            }
            else if (rdoTimTheoSDT.Checked == true)
            {
                dsTim = dskh.Where(x => x.SDT.Contains(giaTriTim)).ToList();
            }
            else
            {
                dsTim = dskh.Where(x => x.TenKH.ToLower().Contains(giaTriTim.ToLower())).ToList();
            }
            return dsTim;
        }
        private void rdoTimTheoCMND_CheckedChanged(object sender, EventArgs e)
        {
            XuLyAutoComplete();
            LoadDSKhachHangLenListView(dskh, lvwDSKhachHang);
        }

        private void rdoTimTheoSDT_CheckedChanged(object sender, EventArgs e)
        {
            XuLyAutoComplete();
            LoadDSKhachHangLenListView(dskh, lvwDSKhachHang);
        }

        private void rdoTimTheoTen_CheckedChanged(object sender, EventArgs e)
        {
            XuLyAutoComplete();
            LoadDSKhachHangLenListView(dskh, lvwDSKhachHang);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            List<eKhachHang> kquaTim = TimKiemKH(txtGiaTriTim.Text);
            if (kquaTim.Count == 0)
            {
                MessageBox.Show("Không có thông tin khách hàng nào như yêu cầu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadDSKhachHangLenListView(dskh, lvwDSKhachHang);
            }
            else
                LoadDSKhachHangLenListView(kquaTim, lvwDSKhachHang);
        }

        private void txtGiaTriTim_TextChanged(object sender, EventArgs e)
        {
            if(txtGiaTriTim.Text == "")
                LoadDSKhachHangLenListView(dskh, lvwDSKhachHang);
        }
    }
}
