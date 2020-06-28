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
using BUS;
using Entities;
namespace NhanVienKeToan
{
    public partial class frmQuanLyHoaDon : Form
    {
        public frmQuanLyHoaDon()
        {
            InitializeComponent();
        }
        BUSVanPhong busvp;
        BUSChiTietHoaDon bushd;
        List<eVanPhong> dsvp;
        string maPhongChon;
        private void frmQuanLyHoaDon_Load(object sender, EventArgs e)
        {
            bushd = new BUSChiTietHoaDon();
            busvp = new BUSVanPhong();
            dsvp = busvp.LayDanhSachPhong();
            LoadPhongLenTreeView(treDSVanPhong, dsvp);
        }
        

        void LoadPhongLenTreeView(TreeView tre, List<eVanPhong> dsp)
        {
            tre.Nodes.Clear();
            foreach (eVanPhong p in dsp)
            {
                TreeNode n = new TreeNode();
                n.Text = p.TenPhong;
                n.Tag = p.MaPhong;
                tre.Nodes.Add(n);
            }
            tre.ExpandAll();
        }

        void ThemItem(eChiTietHoaDon hd, ListView lvw)
        {
            ListViewItem lvwitem = new ListViewItem(hd.EHoaDon.MaHoaDon.ToString());
            lvwitem.SubItems.Add(hd.EHoaDon.ENhanVien.TenNV);
            lvwitem.SubItems.Add(hd.EHoaDon.EHopDong.EKhachHang.TenKH);
            lvwitem.SubItems.Add(hd.EHoaDon.EHopDong.MaHopDong.ToString());
            lvwitem.SubItems.Add(hd.EHoaDon.EHopDong.NgayThue.ToString("dd/MM/yyyy"));
            lvwitem.SubItems.Add(hd.EHoaDon.NgayLapHoaDon.ToString("dd/MM/yyyy"));
            lvwitem.SubItems.Add(hd.EHoaDon.NgayCanLap.ToString("dd/MM/yyyy"));
            if (hd.EHoaDon.TinhTrangHD == false)
                lvwitem.SubItems.Add("Chưa thanh toán");
            else
                lvwitem.SubItems.Add(hd.EHoaDon.NgayThanhToan.ToString("dd/MM/yyyy"));
            if (hd.EHoaDon.TinhTrangHD == false)
                lvwitem.SubItems.Add("Chưa thanh toán");
            else
                lvwitem.SubItems.Add("Đã thành toán");
            lvwitem.Tag = hd;
            lvw.Items.Add(lvwitem);
        }

        void LoadDSHoaDonLenListView(List<eChiTietHoaDon> ds, ListView lvw)
        {
            lvw.Items.Clear();
            foreach(var hd in ds)
            {
                ThemItem(hd, lvw);
            }    
        }
        private void treDSVanPhong_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treDSVanPhong.SelectedNode != null)
            {
                maPhongChon = treDSVanPhong.SelectedNode.Tag.ToString();
                List<eChiTietHoaDon> dshd = bushd.LayDSHoaDon(maPhongChon);
                LoadDSHoaDonLenListView(dshd, lvwDSHoaDon);
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
