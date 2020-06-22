using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NhanVienTuVan.VanPhongService;
using Entities;

namespace NhanVienTuVan
{
    public partial class frmQuanLyKhachConThue : Form
    {
        public frmQuanLyKhachConThue()
        {
            InitializeComponent();
        }

        ChoThueVanPhongServiceClient dt;
        List<eVanPhong> dsphong;
        string maPhongChon;
        private void frmQuanLyKhachConThue_Load(object sender, EventArgs e)
        {
            dt = new ChoThueVanPhongServiceClient();
            dsphong = dt.LayDSVanPhongDangChoThue().ToList();
            LoadPhongLenTreeView(treDSPhong, dsphong);
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
            foreach(eKhachHang item in ds)
            {
                ThemItem(item, lvw);
            }    
        }
        private void btnSuaThongTin_Click(object sender, EventArgs e)
        {

        }

        private void treDSPhong_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(treDSPhong.SelectedNode != null)
            {
                maPhongChon = treDSPhong.SelectedNode.Tag.ToString();
                List<eKhachHang> dskh = dt.LayDSKhachHangDangThue(maPhongChon).ToList();
                LoadDSKhachHangLenListView(dskh, lvwDSKhachHang);
            }    
        }
    }
}
