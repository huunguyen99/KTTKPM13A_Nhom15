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
using BUS;

namespace NhanVienTuVan
{
    public partial class frmQuanLyKhachKhongConThue : Form
    {
        public frmQuanLyKhachKhongConThue()
        {
            InitializeComponent();
        }
        BUSKhachHang buskh;
        List<eKhachHang> dskh;
        private void frmQuanLyKhachKhongConThue_Load(object sender, EventArgs e)
        {
            buskh = new BUSKhachHang();
            dskh = buskh.LayDSKhachHangKhongConThue();
            LoadDSKhachHangLenListView(dskh, lvwDSKhachHang);
            XuLyAutoComplete();
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
                DialogResult hoiSua = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin khách hàng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (hoiSua == DialogResult.Yes)
                {
                    eKhachHang khSua = TaoKHSua();
                    buskh.SuaKH(khChon, khSua);
                    MessageBox.Show("Sửa thông tin khách hàng thành công", "Thông báo");
                    dskh = buskh.LayDSKhachHangKhongConThue();
                    LoadDSKhachHangLenListView(dskh, lvwDSKhachHang);
                }
            }
            else
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa thông tin", "Thông báo");
        }
        void TaiHienKHTuListView(eKhachHang kh)
        {
            dtpNgaySinh.Value = kh.NgaySinh;
            txtEmail.Text = kh.Email;
            if (kh.GioiTinh == true)
                txtGioiTinh.Text = "Nam";
            else
                txtGioiTinh.Text = "Nữ";
            txtHoTen.Text = kh.TenKH;
            txtMaKH.Text = kh.MaKH.ToString();
            rtxtDiaChi.Text = kh.DiaChi;
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lvwDSKhachHang.SelectedItems.Count > 0)
            {
                DialogResult hoiSua = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin khách hàng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (hoiSua == DialogResult.Yes)
                {
                    buskh.XoaKH(khChon);
                    MessageBox.Show("xóa thông tin khách hàng thành công", "Thông báo");
                    dskh = buskh.LayDSKhachHangKhongConThue();
                    LoadDSKhachHangLenListView(dskh, lvwDSKhachHang);
                }
            }
            else
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa thông tin", "Thông báo");
        }

        void XuLyAutoComplete()
        {
            if (rdoTimTheoCMND.Checked == true)
            {
                txtGiaTriTim.AutoCompleteCustomSource.Clear();
                foreach (var item in dskh)
                {
                    txtGiaTriTim.AutoCompleteCustomSource.Add(item.SoCMND);
                }
            }
            else if (rdoTimTheoSDT.Checked == true)
            {
                txtGiaTriTim.AutoCompleteCustomSource.Clear();
                foreach (var item in dskh)
                {
                    txtGiaTriTim.AutoCompleteCustomSource.Add(item.SDT);
                }
            }
            else if (rdoTimTheoTen.Checked == true)
            {
                txtGiaTriTim.AutoCompleteCustomSource.Clear();
                foreach (var item in dskh)
                {
                    txtGiaTriTim.AutoCompleteCustomSource.Add(item.TenKH);
                }
            }
        }

        int TimKiem(string giaTriTim)
        {
            eKhachHang kh;
            if(rdoTimTheoCMND.Checked == true)
            {
                for(int i = 0; i < lvwDSKhachHang.Items.Count; i++)
                {
                    kh = (eKhachHang)lvwDSKhachHang.Items[i].Tag;
                    if (kh.SoCMND.Contains(giaTriTim))
                        return i;
                } 
            }    
            else if(rdoTimTheoSDT.Checked == true)
            {
                for (int i = 0; i < lvwDSKhachHang.Items.Count; i++)
                {
                    kh = (eKhachHang)lvwDSKhachHang.Items[i].Tag;
                    if (kh.SDT.Contains(giaTriTim))
                        return i;
                }
            }    
            else
            {
                for (int i = 0; i < lvwDSKhachHang.Items.Count; i++)
                {
                    kh = (eKhachHang)lvwDSKhachHang.Items[i].Tag;
                    if (kh.TenKH.Contains(giaTriTim))
                        return i;
                }
            }
            return -1;
        }

        private void rdoTimTheoSDT_CheckedChanged(object sender, EventArgs e)
        {
            XuLyAutoComplete();
        }

        private void rdoTimTheoCMND_CheckedChanged(object sender, EventArgs e)
        {
            XuLyAutoComplete();
        }

        private void rdoTimTheoTen_CheckedChanged(object sender, EventArgs e)
        {
            XuLyAutoComplete();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            int kq = TimKiem(txtGiaTriTim.Text);
            if (kq == -1)
                MessageBox.Show("Không tìm thấy khách hàng có thông tin điền vào", "Thông báo");
            else
            {
                if (lvwDSKhachHang.SelectedItems.Count > 0)
                {
                    int vitritruoc = lvwDSKhachHang.SelectedIndices[0];
                    lvwDSKhachHang.Items[vitritruoc].Selected = false;
                }
                lvwDSKhachHang.Items[kq].Selected = true;
                lvwDSKhachHang.Focus();
                khChon = (eKhachHang)lvwDSKhachHang.SelectedItems[kq].Tag;
                TaiHienKHTuListView(khChon);
            }
        }
    }
}
