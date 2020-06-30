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
        eChiTietHoaDon cthd;
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
        private void LoadDuLieuTextBox(eChiTietHoaDon cthd)
        {
            txtMaHoaDon.Text = cthd.MaHoaDon.ToString();
            txtTenNV.Text = cthd.EHoaDon.ENhanVien.TenNV.ToString();
            txtTenKhachHang.Text = cthd.EHoaDon.EHopDong.EKhachHang.TenKH.ToString();
            txtMaHopDong.Text = cthd.EHoaDon.EHopDong.MaHopDong.ToString();
            txtNgayLapHopDong.Text = cthd.EHoaDon.EHopDong.NgayTao.ToString("dd/mm/yyyy");
            txtNgayLapHoaDon.Text = cthd.EHoaDon.NgayLapHoaDon.ToString("dd/mm/yyyy");
            txtNgayCanLapHoaDon.Text = cthd.EHoaDon.NgayLapHoaDon.ToString("dd/mm/yyyy");
            if (cthd.EHoaDon.TinhTrangHD == false)
            {
                txtNgayThanhToan.Text = ("Chưa thanh toán");
            }
            else
                txtNgayThanhToan.Text = (cthd.EHoaDon.NgayThanhToan.ToString("dd/mm/yyyy"));
            if (cthd.EHoaDon.TinhTrangHD == false)
            {
                txtTinhTrangHD.Text = ("Chưa thanh toán");
            }
            else
                txtTinhTrangHD.Text = ("Đã thanh toán");
            txtTienPhong.Text = cthd.TienPhong.ToString();
            txtTienDien.Text = cthd.TienDien.ToString();
            txtTienNuoc.Text = cthd.TienNuoc.ToString();
            txtTienGuiXe.Text = cthd.TienGuiXe.ToString();
            txtPhiBaoTri.Text = cthd.PhiBaoTri.ToString();
            txtPhiVeSinh.Text = cthd.PhiVeSinh.ToString();
            txtPhiBaoVe.Text = cthd.PhiBaoVe.ToString();
            txtPhiThangMay.Text = cthd.PhiThangMay.ToString();
            decimal tongtien = cthd.TienPhong + cthd.TienDien + cthd.TienNuoc + cthd.TienGuiXe + cthd.PhiBaoTri + cthd.PhiVeSinh + cthd.PhiBaoVe + cthd.PhiThangMay;
            string tongtienvnd = string.Format("{0:0,0 VNĐ}", tongtien);
            txtTongTien.Text = tongtienvnd;
        }

        private void lvwDSHoaDon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwDSHoaDon.SelectedItems.Count > 0)
            {
                cthd = (eChiTietHoaDon)lvwDSHoaDon.SelectedItems[0].Tag;
                LoadDuLieuTextBox(cthd);
            }
        }
        void MoSuaTextBox()
        {
            txtTienDien.ReadOnly = false;
            txtTienNuoc.ReadOnly = false;
            txtTienGuiXe.ReadOnly = false;
            txtPhiBaoTri.ReadOnly = false;
            txtPhiVeSinh.ReadOnly = false;
            txtPhiThangMay.ReadOnly = false;
            txtPhiBaoVe.ReadOnly = false;

            txtTienDien.Clear();
            txtTienNuoc.Clear();
            txtTienGuiXe.Clear();
            txtPhiBaoTri.Clear();
            txtPhiVeSinh.Clear();
            txtPhiThangMay.Clear();
            txtPhiBaoVe.Clear();
        }
        void KhoaSuaTextBox()
        {
            txtTienDien.ReadOnly = true;
            txtTienNuoc.ReadOnly = true;
            txtTienGuiXe.ReadOnly = true;
            txtPhiBaoTri.ReadOnly = true;
            txtPhiVeSinh.ReadOnly = true;
            txtPhiThangMay.ReadOnly = true;
            txtPhiBaoVe.ReadOnly = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            MoSuaTextBox();
        }

        private void btnLuuSua_Click(object sender, EventArgs e)
        {
            if(txtTienDien.Text.Trim().Length==0 || txtTienNuoc.Text.Trim().Length == 0|| txtTienGuiXe.Text.Trim().Length == 0|| txtPhiBaoTri.Text.Trim().Length == 0|| txtPhiBaoVe.Text.Trim().Length == 0|| txtPhiThangMay.Text.Trim().Length == 0|| txtPhiVeSinh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                    decimal tiendien = Convert.ToDecimal(txtTienDien.Text);
                    decimal tiennuoc = Convert.ToDecimal(txtTienNuoc.Text);
                    decimal tienguixe = Convert.ToDecimal(txtTienGuiXe.Text);
                    decimal phibaotri = Convert.ToDecimal(txtPhiBaoTri.Text);
                    decimal phivesinh = Convert.ToDecimal(txtPhiVeSinh.Text);
                    decimal phithangmay = Convert.ToDecimal(txtPhiThangMay.Text);
                    decimal phibaove = Convert.ToDecimal(txtPhiBaoVe.Text);

                    if (tiendien < 0 || tiennuoc < 0 || tienguixe < 0 || phibaotri < 0 || phivesinh < 0 || phithangmay < 0 || phibaove < 0)
                    {
                        MessageBox.Show("Các giá trị phải lớn hơn 0 !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        if (lvwDSHoaDon.SelectedItems.Count > 0)
                        {
                            DialogResult HoiSua = MessageBox.Show("Bạn có chắc chắn muốn lưu lại thông tin này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (HoiSua == DialogResult.Yes)
                            {
                                bushd.SuaHoaDon(cthd);
                                MessageBox.Show("Sửa thông tin hoàn tất", "Thông báo");
                            }
                            KhoaSuaTextBox();
                            List<eChiTietHoaDon> dshd = bushd.LayDSHoaDon(maPhongChon);
                            LoadDSHoaDonLenListView(dshd, lvwDSHoaDon);
                        }
                    }
                }
            }
    }
}
