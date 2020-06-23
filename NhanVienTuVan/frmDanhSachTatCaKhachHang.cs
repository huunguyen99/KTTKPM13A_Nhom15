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
using System.Security.Cryptography.X509Certificates;

namespace NhanVienTuVan
{
    public partial class frmDanhSachTatCaKhachHang : Form
    {
        private static int MaNV;
        private static decimal GiaThue;
        private static int MaPhieu;
        public frmDanhSachTatCaKhachHang(int manv, int maphieu, decimal giathue)
        {
            InitializeComponent();
            MaNV = manv;
            GiaThue = giathue;
            MaPhieu = maphieu;
        }
        DALKhachHang dalkh;
        List<eKhachHang> dskh;
        eKhachHang khChon;
        private void frmDanhSachTatCaKhachHang_Load(object sender, EventArgs e)
        {
            dalkh = new DALKhachHang();
            dskh = dalkh.LayDSTatCaKhachHang();
            LoadDSKhachHangLenListView(dskh, lvwDSKhachHang);
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

        private void lvwDSKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvwDSKhachHang.SelectedItems.Count > 0)
            {
                khChon = (eKhachHang)lvwDSKhachHang.SelectedItems[0].Tag;
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
