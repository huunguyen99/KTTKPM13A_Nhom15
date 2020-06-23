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
using DAL;

namespace NhanVienTuVan
{
    public partial class frmQuanLyKhachKhongConThue : Form
    {
        public frmQuanLyKhachKhongConThue()
        {
            InitializeComponent();
        }
        DALKhachHang dalkh;
        List<eKhachHang> dskh;
        private void frmQuanLyKhachKhongConThue_Load(object sender, EventArgs e)
        {
            dalkh = new DALKhachHang();
            dskh = dalkh.LayDSKhachHangKhongConThue();
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
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult hoiThoat = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (hoiThoat == DialogResult.Yes)
                this.Close();
        }
    }
}
