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
    public partial class frmTraPhong : Form
    {
        public frmTraPhong()
        {
            InitializeComponent();
        }
        BUSVanPhong busvp;
        BUSKhachHang buskh;
        List<eVanPhong> dsphong;
        string maPhongChon;

        private void frmTraPhong_Load(object sender, EventArgs e)
        {
            busvp = new BUSVanPhong();
            buskh = new BUSKhachHang();
            dsphong = busvp.LayDSVanPhongDangChoThue().ToList();
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
            foreach (eKhachHang item in ds)
            {
                ThemItem(item, lvw);
            }
        }

        private void treDSPhong_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treDSPhong.SelectedNode != null)
            {
                maPhongChon = treDSPhong.SelectedNode.Tag.ToString();
                List<eKhachHang> dskh = buskh.LayDSKhachHangDangThue(maPhongChon);
                LoadDSKhachHangLenListView(dskh, lvwDSKhachHang);
            }
        }
        private void btnTraPhong_Click(object sender, EventArgs e)
        {
            if (treDSPhong.SelectedNode != null)
            {
                DialogResult hoi = MessageBox.Show("Bạn có chắc chắn muốn trả phòng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if(hoi == DialogResult.Yes)
                {
                    busvp.TraPhong(maPhongChon);
                    MessageBox.Show("Trả phòng thành công", "Thông báo");
                    dsphong = busvp.LayDSVanPhongDangChoThue().ToList();
                    LoadPhongLenTreeView(treDSPhong, dsphong);
                }    
            }
            else
                MessageBox.Show("Vui lòng chọn văn phòng cần trả", "Thông báo");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult hoiThoat = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (hoiThoat == DialogResult.Yes)
                this.Close();
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
            txtDiaChi.Text = kh.DiaChi;
            txtSoCMND.Text = kh.SoCMND;
            txtSoDT.Text = kh.SDT;
        }
        eKhachHang khChon;
        private void lvwDSKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwDSKhachHang.SelectedItems.Count > 0)
            {
                khChon = (eKhachHang)lvwDSKhachHang.SelectedItems[0].Tag;
                TaiHienKHTuListView(khChon);
            }
        }
    }
}
