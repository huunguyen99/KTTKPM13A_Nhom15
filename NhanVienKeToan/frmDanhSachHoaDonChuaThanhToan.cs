using BUS;
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

namespace NhanVienKeToan
{
    public partial class frmDanhSachHoaDonChuaThanhToan : Form
    {
        public frmDanhSachHoaDonChuaThanhToan()
        {
            InitializeComponent();
        }

        BUSVanPhong busvp;
        BUSChiTietHoaDon bushd;
        BUSHoaDon bushdon;
        List<eVanPhong> dsvp;
        string maPhongChon;
        private void frmDanhSachHoaDonChuaThanhToan_Load(object sender, EventArgs e)
        {
            bushd = new BUSChiTietHoaDon();
            busvp = new BUSVanPhong();
            bushdon = new BUSHoaDon();
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
            foreach (var hd in ds)
            {
                ThemItem(hd, lvw);
            }
        }

        private void treDSVanPhong_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treDSVanPhong.SelectedNode != null)
            {
                maPhongChon = treDSVanPhong.SelectedNode.Tag.ToString();
                List<eChiTietHoaDon> dshd = bushd.LayDSHoaDonChuaThanhToan(maPhongChon);
                LoadDSHoaDonLenListView(dshd, lvwDSHoaDon);
            }
        }

        
        void TaiHienThongTinHoaDonLenTextBox(eChiTietHoaDon cthd)
        {
            string tienphong = string.Format("{0:0,0 VNĐ}", cthd.TienPhong);
            string tiendien = string.Format("{0:0,0 VNĐ}", cthd.TienDien);
            string tiennuoc = string.Format("{0:0,0 VNĐ}", cthd.TienNuoc);
            string tienguixe = string.Format("{0:0,0 VNĐ}", cthd.TienGuiXe);
            string phibaove = string.Format("{0:0,0 VNĐ}", cthd.PhiBaoTri);
            string phibaotri = string.Format("{0:0,0 VNĐ}", cthd.PhiBaoTri);
            string phithangmay = string.Format("{0:0,0 VNĐ}", cthd.PhiThangMay);
            string phivesinh = string.Format("{0:0,0 VNĐ}", cthd.PhiVeSinh);
            txtMaHoaDon.Text = cthd.MaHoaDon.ToString();
            txtTenNV.Text = cthd.EHoaDon.ENhanVien.TenNV.ToString();
            txtTenKhachHang.Text = cthd.EHoaDon.EHopDong.EKhachHang.TenKH.ToString();
            txtMaHopDong.Text = cthd.EHoaDon.EHopDong.MaHopDong.ToString();
            txtNgayLapHopDong.Text = cthd.EHoaDon.EHopDong.NgayTao.ToString("dd/MM/yyyy");
            txtNgayLapHoaDon.Text = cthd.EHoaDon.NgayLapHoaDon.ToString("dd/MM/yyyy");
            txtNgayCanLapHoaDon.Text = cthd.EHoaDon.NgayLapHoaDon.ToString("dd/MM/yyyy");
            if (cthd.EHoaDon.TinhTrangHD == false)
            {
                txtNgayThanhToan.Text = ("Chưa thanh toán");
            }
            else
                txtNgayThanhToan.Text = (cthd.EHoaDon.NgayThanhToan.ToString("dd/MM/yyyy"));
            if (cthd.EHoaDon.TinhTrangHD == false)
            {
                txtTinhTrangHD.Text = ("Chưa thanh toán");
            }
            else
                txtTinhTrangHD.Text = ("Đã thanh toán");
            txtTienPhong.Text = tienphong;
            txtTienDien.Text = tiendien;
            txtTienNuoc.Text = tiennuoc;
            txtTienGuiXe.Text = tienguixe;
            txtPhiBaoTri.Text = phibaotri;
            txtPhiVeSinh.Text = phivesinh;
            txtPhiBaoVe.Text = phibaove;
            txtPhiThangMay.Text = phithangmay;
            decimal tongtien = cthd.TienPhong + cthd.TienDien + cthd.TienNuoc + cthd.TienGuiXe + cthd.PhiBaoTri + cthd.PhiVeSinh + cthd.PhiBaoVe + cthd.PhiThangMay;
            string tongtienfm = string.Format("{0:0,0 VNĐ}", tongtien);
            txtTongTien.Text = tongtienfm;
        }
        eChiTietHoaDon hdChon;
        private void lvwDSHoaDon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvwDSHoaDon.SelectedItems.Count > 0)
            {
                hdChon = (eChiTietHoaDon)lvwDSHoaDon.SelectedItems[0].Tag;
                TaiHienThongTinHoaDonLenTextBox(hdChon);
            }    
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (lvwDSHoaDon.SelectedItems.Count > 0)
            {
                if (hdChon.EHoaDon.TinhTrangHD == true)
                {
                    MessageBox.Show("Hóa đơn này đã thanh toán rồi!\nKhông thể thanh toán nữa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult HoiTT = MessageBox.Show("Bạn có chắc chắn muốn thanh toán hóa đơn này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (HoiTT == DialogResult.Yes)
                    {
                        bushdon.ThanhToanHoaDon(hdChon.EHoaDon, DateTime.Now);
                        MessageBox.Show("Thanh toán thành công!", "Thông báo");
                        List<eChiTietHoaDon> dshd = bushd.LayDSHoaDonChuaThanhToan(maPhongChon);
                        LoadDSHoaDonLenListView(dshd, lvwDSHoaDon);
                    }
                }
            }
            else
                MessageBox.Show("Vui lòng chọn hóa đơn cần thanh toán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
