using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DAL;
using Entities;

namespace NhanVienKeToan
{
    public partial class frmLapHoaDon : Form
    {
        private static int MaNV { get; set; }
        public frmLapHoaDon(int manv)
        {
            MaNV = manv;
            InitializeComponent();
        }
        BUSVanPhong busvp;
        BUSHopDong bushd;
        BUSHoaDon bushoadon;
        List<eVanPhong> dsvp;
        string maPhongChon;
        eVanPhong phChon;
        DateTime hdGanNhat;
        private void frmLapHoaDon_Load(object sender, EventArgs e)
        {
            busvp = new BUSVanPhong();
            bushd = new BUSHopDong();
            bushoadon = new BUSHoaDon();
            dsvp = busvp.LayDSVanPHongDenHanThanhToan();
            LoadPhongLenTreeView(treDSPhong, dsvp);
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
            lvwitem.SubItems.Add(hd.TienCoc.ToString());
            lvwitem.SubItems.Add(hd.NgayTao.ToString("dd/MM/yyyy"));
            lvwitem.SubItems.Add(hd.NgayThue.ToString("dd/MM/yyyy"));
            lvwitem.SubItems.Add(hd.NgayTra.ToString("dd/MM/yyyy"));
            lvwitem.Tag = hd;
            lvw.Items.Add(lvwitem);
        }

        void LoadDSHopDongLenListView(List<eHopDong> ds, ListView lvw)
        {
            lvw.Items.Clear();
            foreach (eHopDong hd in ds)
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
            txtTienCoc.Text = hd.TienCoc.ToString();
        }
        eHopDong hdChon;

        private void treDSPhong_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treDSPhong.SelectedNode != null)
            {
                maPhongChon = treDSPhong.SelectedNode.Tag.ToString();
                phChon = dsvp.Where(x => x.MaPhong.Equals(maPhongChon)).FirstOrDefault();
                List<eHopDong> dshd = bushd.LayDSHopDongConHan(maPhongChon);
                LoadDSHopDongLenListView(dshd, lvwDSHopDong);
                hdGanNhat = bushoadon.LayHDSauCung(maPhongChon);
                dtpHoaDonGanNhat.Value = hdGanNhat;
            }
        }

        private void lvwDSHopDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwDSHopDong.SelectedItems.Count > 0)
            {
                hdChon = (eHopDong)lvwDSHopDong.SelectedItems[0].Tag;
                TaiHienHopDongTuListView(hdChon);
            }
        }

        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            if (treDSPhong.SelectedNode != null)
            {
                if (lvwDSHopDong.SelectedItems.Count > 0)
                {
                    DialogResult hoi = MessageBox.Show("Bạn có chắc chắn muốn lập hóa đơn cho phòng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if(hoi == DialogResult.Yes)
                    {
                        frmDienThongTinHoaDon frm = new frmDienThongTinHoaDon(MaNV, phChon.GiaThue, hdChon.MaHopDong, hdGanNhat);
                        if(frm.ShowDialog() == DialogResult.OK)
                        {
                            dsvp = busvp.LayDSVanPHongDenHanThanhToan();
                            LoadPhongLenTreeView(treDSPhong, dsvp);
                            lvwDSHopDong.Items.Clear();
                        }    
                    }    
                }
                else
                    MessageBox.Show("Vui lòng chọn vào hợp đồng", "Thông báo");
            }
            else
                MessageBox.Show("Vui lòng chọn phòng cần lập hóa đơn", "Thông báo");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult hoiThoat = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (hoiThoat == DialogResult.Yes)
                this.Close();
        }
    }
}
