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
        ErrorProvider ep;
        private void frmQuanLyHoaDon_Load(object sender, EventArgs e)
        {
            ep = new ErrorProvider();
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
        eChiTietHoaDon hdChon;
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
                hdChon = (eChiTietHoaDon)lvwDSHoaDon.SelectedItems[0].Tag;
                LoadDuLieuTextBox(hdChon);
            }
        }


        eChiTietHoaDon TaoHoaDonSua()
        {
            eChiTietHoaDon hd = new eChiTietHoaDon();
            decimal tiendien = Convert.ToDecimal(txtTienDien.Text);
            decimal tiennuoc = Convert.ToDecimal(txtTienNuoc.Text);
            decimal tienguixe = Convert.ToDecimal(txtTienGuiXe.Text);
            decimal phibaotri = Convert.ToDecimal(txtPhiBaoTri.Text);
            decimal phivesinh = Convert.ToDecimal(txtPhiVeSinh.Text);
            decimal phithangmay = Convert.ToDecimal(txtPhiThangMay.Text);
            decimal phibaove = Convert.ToDecimal(txtPhiBaoVe.Text);
            hd.TienDien = tiendien;
            hd.TienNuoc = tiennuoc;
            hd.TienGuiXe = tienguixe;
            hd.PhiBaoTri = phibaotri;
            hd.PhiBaoVe = phibaove;
            hd.PhiThangMay = phithangmay;
            hd.PhiVeSinh = phivesinh;
            return hd;
        }

        private void btnLuuSua_Click(object sender, EventArgs e)
        {
            if (lvwDSHoaDon.SelectedItems.Count > 0)
            {
                if (hdChon.EHoaDon.TinhTrangHD == true)
                    MessageBox.Show("Hóa đơn này đã thanh toán, không thể chỉnh sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    decimal tiendien;
                    decimal tiennuoc;
                    decimal tienguixe;
                    decimal phibaotri;
                    decimal phivesinh;
                    decimal phithangmay;
                    decimal phibaove;
                    if (txtTienDien.Text.Trim().Length == 0 || txtTienNuoc.Text.Trim().Length == 0 || txtTienGuiXe.Text.Trim().Length == 0 || txtPhiBaoTri.Text.Trim().Length == 0 || txtPhiBaoVe.Text.Trim().Length == 0 || txtPhiThangMay.Text.Trim().Length == 0 || txtPhiVeSinh.Text.Trim().Length == 0)
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    else
                    {
                        try
                        {
                            tiendien = Convert.ToDecimal(txtTienDien.Text);
                            tiennuoc = Convert.ToDecimal(txtTienNuoc.Text);
                            tienguixe = Convert.ToDecimal(txtTienGuiXe.Text);
                            phibaotri = Convert.ToDecimal(txtPhiBaoTri.Text);
                            phivesinh = Convert.ToDecimal(txtPhiVeSinh.Text);
                            phithangmay = Convert.ToDecimal(txtPhiThangMay.Text);
                            phibaove = Convert.ToDecimal(txtPhiBaoVe.Text);
                            if (tiendien >= 100000000)
                                ep.SetError(txtTienDien, "Các khoản phí không thể quá 100 triệu");
                            else if (tiennuoc >= 100000000)
                                ep.SetError(txtTienGuiXe, "Các khoản phí không thể quá 100 triệu");
                            else if (tienguixe >= 100000000)
                                ep.SetError(txtTienNuoc, "Các khoản phí không thể quá 100 triệu");
                            else if (phibaotri >= 100000000)
                                ep.SetError(txtPhiBaoTri, "Các khoản phí không thể quá 100 triệu");
                            else if (phibaove >= 100000000)
                                ep.SetError(txtPhiBaoVe, "Các khoản phí không thể quá 100 triệu");
                            else if (phithangmay >= 100000000)
                                ep.SetError(txtPhiThangMay, "Các khoản phí không thể quá 100 triệu");
                            else if (phivesinh >= 100000000)
                                ep.SetError(txtPhiVeSinh, "Các khoản phí không thể quá 100 triệu");
                            else
                            {
                                DialogResult HoiSua = MessageBox.Show("Bạn có chắc chắn muốn lưu lại thông tin này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                if (HoiSua == DialogResult.Yes)
                                {
                                    eChiTietHoaDon hdSua = TaoHoaDonSua();
                                    bushd.SuaHoaDon(hdChon, hdSua);
                                    MessageBox.Show("Sửa thông tin hoàn tất", "Thông báo");
                                }
                                List<eChiTietHoaDon> dshd = bushd.LayDSHoaDon(maPhongChon);
                                LoadDSHoaDonLenListView(dshd, lvwDSHoaDon);
                            }
                        }
                        catch (OverflowException)
                        {
                            MessageBox.Show("Số tiền quá lớn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch(Exception)
                        {
                            MessageBox.Show("Có lỗi xảy ra", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
                MessageBox.Show("Vui lòng chọn hóa đơn cần chỉnh sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void txtTienDien_TextChanged(object sender, EventArgs e)
        {
            if (txtTienDien.Text.KTraTien())
                ep.Clear();
        }

        private void txtTienNuoc_TextChanged(object sender, EventArgs e)
        {
            if (txtTienNuoc.Text.KTraTien())
                ep.Clear();
        }

        private void txtTienGuiXe_TextChanged(object sender, EventArgs e)
        {
            if (txtTienGuiXe.Text.KTraTien())
                ep.Clear();
        }

        private void txtPhiBaoTri_TextChanged(object sender, EventArgs e)
        {
            if (txtPhiBaoTri.Text.KTraTien())
                ep.Clear();
        }

        private void txtPhiVeSinh_TextChanged(object sender, EventArgs e)
        {
            if (txtPhiVeSinh.Text.KTraTien())
                ep.Clear();

        }

        private void txtPhiThangMay_TextChanged(object sender, EventArgs e)
        {
            if (txtPhiThangMay.Text.KTraTien())
                ep.Clear();
        }

        private void txtPhiBaoVe_TextChanged(object sender, EventArgs e)
        {
            if (txtPhiBaoVe.Text.KTraTien())
                ep.Clear();
        }

        private void txtTienNuoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtTienDien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtTienGuiXe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtPhiBaoTri_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtPhiVeSinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtPhiThangMay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtPhiBaoVe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }
    }
}
