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
    public partial class frmQuanLyHopDong : Form
    {
        public frmQuanLyHopDong()
        {
            InitializeComponent();
        }
        BUSVanPhong busvp;
        BUSHopDong bushd;
        List<eVanPhong> dsvp;
        string maPhongChon;
        private void frmQuanLyHopDong_Load(object sender, EventArgs e)
        {
            busvp = new BUSVanPhong();
            bushd = new BUSHopDong();
            dsvp = busvp.LayDSVanPhongDangChoThue();
            LoadPhongLenTreeView(treDSPhong, dsvp);
        }

        private void treDSPhong_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(treDSPhong.SelectedNode != null)
            {
                maPhongChon = treDSPhong.SelectedNode.Tag.ToString();
                List<eHopDong> dshd = bushd.LayDSHopDongConHan(maPhongChon);
                LoadDSHopDongLenListView(dshd, lvwDSHopDong);
            }    
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
        void ThemItemHD(eHopDong hd, ListView lvw)
        {
            ListViewItem lvwitem = new ListViewItem(hd.MaHopDong.ToString());
            lvwitem.SubItems.Add(hd.ENhanVien.TenNV);
            lvwitem.SubItems.Add(hd.EKhachHang.TenKH);
            string tiencoc = string.Format("{0:0,0 VNĐ}", hd.TienCoc);
            lvwitem.SubItems.Add(tiencoc);
            lvwitem.SubItems.Add(hd.NgayTao.ToString("dd/MM/yyyy"));
            lvwitem.SubItems.Add(hd.NgayThue.ToString("dd/MM/yyyy"));
            lvwitem.SubItems.Add(hd.NgayTra.ToString("dd/MM/yyyy"));
            lvwitem.Tag = hd;
            lvw.Items.Add(lvwitem);
        }

        void LoadDSHopDongLenListView(List<eHopDong> ds, ListView lvw)
        {
            lvw.Items.Clear();
            foreach(eHopDong hd in ds)
            {
                ThemItemHD(hd, lvw);
            }    
        }

        void TaiHienHopDongTuListView(eHopDong hd)
        {
            txtKhachHang.Text = hd.EKhachHang.TenKH;
            txtMaHopDong.Text = hd.MaHopDong.ToString();
            dtpNgayTao.Value = hd.NgayTao;
            dtpNgayThue.Value = hd.NgayThue;
            dtpNgayTra.Value = hd.NgayTra;
            txtNhanVienTao.Text = hd.ENhanVien.TenNV;
            string tiencoc = string.Format("{0:0,0 VNĐ}", hd.TienCoc);
            txtTienCoc.Text = tiencoc;
        }
        eHopDong hdChon;
        private void lvwDSHopDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvwDSHopDong.SelectedItems.Count > 0)
            {
                hdChon = (eHopDong)lvwDSHopDong.SelectedItems[0].Tag;
                TaiHienHopDongTuListView(hdChon);
            }    
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult hoiThoat = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (hoiThoat == DialogResult.Yes)
                this.Close();
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (lvwDSHopDong.SelectedItems.Count > 0)
            {
                DialogResult hoiSua = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin hợp đồng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (hoiSua == DialogResult.Yes)
                {
                    bushd.SuaHopDong(hdChon, dtpNgayThue.Value, dtpNgayTra.Value);
                    MessageBox.Show("Sửa thông tin hợp đồng thành công", "Thông báo");
                    List<eHopDong> dshd = bushd.LayDSHopDongConHan(maPhongChon);
                    LoadDSHopDongLenListView(dshd, lvwDSHopDong);
                }
            }
            else
                MessageBox.Show("Vui lòng chọn hợp đồng cần sửa thông tin", "Thông báo");
        }

        private void txtTienCoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }
    }
}
