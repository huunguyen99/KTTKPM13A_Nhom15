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
    public partial class frmQuanLyKhachConThue : Form
    {
        public frmQuanLyKhachConThue()
        {
            InitializeComponent();
        }

        BUSVanPhong busvp;
        BUSKhachHang buskh;
        List<eVanPhong> dsphong;
        string maPhongChon;
        ErrorProvider ep;
        private void frmQuanLyKhachConThue_Load(object sender, EventArgs e)
        {
            ep = new ErrorProvider();
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
            foreach(eKhachHang item in ds)
            {
                ThemItem(item, lvw);
            }    
        }

        eKhachHang TaoKHSua()
        {
            eKhachHang kh = new eKhachHang();
            kh.Active = true;
            kh.TenKH = txtHoTen.Text;
            kh.Email = txtEmail.Text;
            kh.SDT = txtSoDT.Text;
            kh.NgaySinh = dtpNgaySinh.Value;
            kh.SoCMND = txtSoCMND.Text;
            kh.DiaChi = rtxtDiaChi.Text;
            kh.GioiTinh = khChon.GioiTinh;
            return kh;
        }
        private void btnSuaThongTin_Click(object sender, EventArgs e)
        {
            if (lvwDSKhachHang.SelectedItems.Count > 0)
            {
                if ((DateTime.Now - dtpNgaySinh.Value).TotalDays < 18 * 365 + 4)
                    MessageBox.Show("ngày sinh cần sửa không hợp lệ, khách hàng chưa đủ 18 tuổi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (txtHoTen.Text.Trim().Length == 0 || txtEmail.Text.Trim().Length == 0 || txtSoCMND.Text.Trim().Length == 0 || txtSoDT.Text.Trim().Length == 0 || rtxtDiaChi.Text.Trim().Length == 0)
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (!txtEmail.Text.KtraEmail())
                    ep.SetError(txtEmail, "Email phải đúng định dang bắt đầu bằng chữ cái. Ex: abc123@gmail.com");
                else if (!txtSoCMND.Text.KtraSCMND())
                    ep.SetError(txtSoCMND, "Số chứng minh(căn cước) phải là số, có 9 hoặc 12 số, bắt đầu bằng 1-9");
                else if (!txtSoDT.Text.KtraSDT())
                    ep.SetError(txtSoDT, "Số điện thoại phải là số và có 10 số, bắt đầu bằng 01, 03, 05, 07 hoặc 09");
                else
                {
                    DialogResult hoiSua = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin khách hàng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (hoiSua == DialogResult.Yes)
                    {
                        eKhachHang khSua = TaoKHSua();
                        buskh.SuaKH(khChon, khSua);
                        MessageBox.Show("Sửa thông tin khách hàng thành công", "Thông báo");
                        List<eKhachHang> dskh = buskh.LayDSKhachHangDangThue(maPhongChon);
                        LoadDSKhachHangLenListView(dskh, lvwDSKhachHang);
                    }
                }
            }
            else
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa thông tin", "Thông báo");
        }

        private void treDSPhong_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(treDSPhong.SelectedNode != null)
            {
                maPhongChon = treDSPhong.SelectedNode.Tag.ToString();
                List<eKhachHang> dskh = buskh.LayDSKhachHangDangThue(maPhongChon);
                LoadDSKhachHangLenListView(dskh, lvwDSKhachHang);
            }    
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
            dtpNgaySinh.Value = kh.NgaySinh;
            rtxtDiaChi.Text = kh.DiaChi;
            txtSoCMND.Text = kh.SoCMND;
            txtSoDT.Text = kh.SDT;
        }
        eKhachHang khChon;
        private void lvwDSKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvwDSKhachHang.SelectedItems.Count > 0)
            {
                khChon = (eKhachHang)lvwDSKhachHang.SelectedItems[0].Tag;
                TaiHienKHTuListView(khChon);
            }    
        }

        private void txtSoDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtSoCMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtSoDT_TextChanged(object sender, EventArgs e)
        {
            if (txtSoDT.Text.KtraSDT())
                ep.Clear();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (txtEmail.Text.KtraEmail())
                ep.Clear();
        }

        private void txtSoCMND_TextChanged(object sender, EventArgs e)
        {
            if (txtSoCMND.Text.KtraSCMND())
                ep.Clear();
        }
    }
}
