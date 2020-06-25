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
using ComponentFactory.Krypton.Toolkit;

namespace Admin
{
    public partial class frmQuanLyNhanVien : Form
    {
        public frmQuanLyNhanVien()
        {
            InitializeComponent();
        }
        BUSNhanVienVaTaiKhoan busnvtk;
        List<eTaiKhoan> dsnvtk;
        private void frmQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            busnvtk = new BUSNhanVienVaTaiKhoan();
            dsnvtk = busnvtk.LayDSTaiKhoanVaNhanVien();
            LoadDSNhanVienLenListView(dsnvtk, lvwDSNhanVien);
            ItemsCboChucVu(cboChucVu);
            XuLyAutoComplete();
        }
        void ItemsCboChucVu(KryptonComboBox cbo)
        {
            List<clsChucVu> list = new List<clsChucVu>()
            {
                new clsChucVu {Ten = "Nhân Viên Tư Vấn", Value = 1},
                new clsChucVu {Ten = "Nhân Viên Kỹ Thuật", Value = 2},
                new clsChucVu {Ten = "Nhân Viên Kế Toán", Value = 3}
            };
            cbo.DataSource = list;
            cbo.DisplayMember = "Ten";
            cbo.ValueMember = "Value";
            cbo.SelectedItem = list.FirstOrDefault();
        }

        void ThemItem(eTaiKhoan tk, ListView lvw)
        {
            ListViewItem lvwitem = new ListViewItem(tk.MaNV.ToString());
            lvwitem.SubItems.Add(tk.ENhanVien.TenNV);
            lvwitem.SubItems.Add(tk.ENhanVien.NgaySinh.ToString("dd/MM/yyyy"));
            lvwitem.SubItems.Add(tk.ENhanVien.SoCMND);
            lvwitem.SubItems.Add(tk.ENhanVien.SDT);
            lvwitem.SubItems.Add(tk.ENhanVien.Email);
            lvwitem.SubItems.Add(tk.ENhanVien.QueQuan);
            lvwitem.SubItems.Add(tk.ENhanVien.DiaChi);
            if (tk.ENhanVien.GioiTinh == true)
                lvwitem.SubItems.Add("Nam");
            else
                lvwitem.SubItems.Add("Nữ");
            if(tk.ENhanVien.ChucVu == 1)
                lvwitem.SubItems.Add("Nhân Viên Tư Vấn");
            else if(tk.ENhanVien.ChucVu == 2)
                lvwitem.SubItems.Add("Nhân Viên Kỹ Thuật");
            else
                lvwitem.SubItems.Add("Nhân Viên Kế Toán");
            lvwitem.Tag = tk;
            lvw.Items.Add(lvwitem);
        }

        void LoadDSNhanVienLenListView(List<eTaiKhoan> ds, ListView lvw)
        {
            lvw.Items.Clear();
            foreach(var item in ds)
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
        
        void TaiHienThongTinNhanVien(eTaiKhoan tk)
        {
            txtMaNV.Text = tk.ENhanVien.MaNV.ToString();
            txtTenNV.Text = tk.ENhanVien.TenNV;
            dtpNgaySinh.Value = tk.ENhanVien.NgaySinh;
            txtEmail.Text = tk.ENhanVien.Email;
            txtSoCMND.Text = tk.ENhanVien.SoCMND;
            txtSoDT.Text = tk.ENhanVien.SDT;
            rtxtDiaChi.Text = tk.ENhanVien.DiaChi;
            rtxtQueQuan.Text = tk.ENhanVien.QueQuan;
            txtTaiKhoan.Text = tk.TenTK;
            txtMatKhau.Text = tk.MatKhau;
            if (tk.ENhanVien.GioiTinh == true)
                txtGioiTinh.Text = "Nam";
            else
                txtGioiTinh.Text = "Nữ";
            if (tk.ENhanVien.ChucVu == 1)
                cboChucVu.SelectedValue = 1;
            else if (tk.ENhanVien.ChucVu == 2)
                cboChucVu.SelectedValue = 2;
            else
                cboChucVu.SelectedValue = 3;
            if (tk.ENhanVien.Active == true)
                txtTinhTrang.Text = "Vẫn còn làm việc";
            else
                txtTinhTrang.Text = "Đã nghỉ việc";

        }

        eTaiKhoan tkChon;
        private void lvwDSNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvwDSNhanVien.SelectedItems.Count > 0)
            {
                tkChon = (eTaiKhoan)lvwDSNhanVien.SelectedItems[0].Tag;
                TaiHienThongTinNhanVien(tkChon);
            }    
        }

        private void btnThemNhanVien_Click(object sender, EventArgs e)
        {
            frmThemNhanVien frmtnv = new frmThemNhanVien();
            if(frmtnv.ShowDialog() == DialogResult.OK)
            {
                dsnvtk = busnvtk.LayDSTaiKhoanVaNhanVien();
                LoadDSNhanVienLenListView(dsnvtk, lvwDSNhanVien);
            }    
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            if (lvwDSNhanVien.SelectedItems.Count > 0)
            {
                DialogResult hoiXoa = MessageBox.Show("Bạn có chắc chắn muốn xa thải nhân viên này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if(hoiXoa == DialogResult.Yes)
                {
                    busnvtk.XoaNV(tkChon);
                    MessageBox.Show("Xa thải nhân viên thành công", "Thông báo");
                    dsnvtk = busnvtk.LayDSTaiKhoanVaNhanVien();
                    LoadDSNhanVienLenListView(dsnvtk, lvwDSNhanVien);
                }    
            }
            else
                MessageBox.Show("Vui lòng chọn nhân viên cần xa thải", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        eNhanVien TaoNVSua()
        {
            eNhanVien nv = new eNhanVien();
            nv.ChucVu = Convert.ToInt32(cboChucVu.SelectedValue);
            nv.DiaChi = rtxtDiaChi.Text;
            nv.QueQuan = rtxtQueQuan.Text;
            nv.Email = txtEmail.Text;
            nv.SDT = txtSoDT.Text;
            nv.SoCMND = txtSoCMND.Text;
            nv.TenNV = txtTenNV.Text;
            nv.NgaySinh = dtpNgaySinh.Value;
            return nv;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (lvwDSNhanVien.SelectedItems.Count > 0)
            {
                DialogResult hoiSua = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin nhân viên này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (hoiSua == DialogResult.Yes)
                {
                    eNhanVien nv = TaoNVSua();
                    busnvtk.SuaTTNhanVien(tkChon.MaNV, nv);
                    MessageBox.Show("Sửa thông tin nhân viên thành công", "Thông báo");
                    dsnvtk = busnvtk.LayDSTaiKhoanVaNhanVien();
                    LoadDSNhanVienLenListView(dsnvtk, lvwDSNhanVien);
                }
            }
            else
                MessageBox.Show("Vui lòng chọn nhân viên cần chỉnh sửa thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        void XuLyAutoComplete()
        {
            if (rdoTimTheoCMND.Checked == true)
            {
                txtGiaTriTim.AutoCompleteCustomSource.Clear();
                foreach (var item in dsnvtk)
                {
                    txtGiaTriTim.AutoCompleteCustomSource.Add(item.ENhanVien.SoCMND);
                }
            }
            else if (rdoTimTheoSDT.Checked == true)
            {
                txtGiaTriTim.AutoCompleteCustomSource.Clear();
                foreach (var item in dsnvtk)
                {
                    txtGiaTriTim.AutoCompleteCustomSource.Add(item.ENhanVien.SDT);
                }
            }
            else if (rdoTimTheoTen.Checked == true)
            {
                txtGiaTriTim.AutoCompleteCustomSource.Clear();
                foreach (var item in dsnvtk)
                {
                    txtGiaTriTim.AutoCompleteCustomSource.Add(item.ENhanVien.TenNV);
                }
            }
        }



        int TimKiem(string giaTriTim)
        {
            eTaiKhoan tk;
            if (rdoTimTheoCMND.Checked == true)
            {
                for (int i = 0; i < lvwDSNhanVien.Items.Count; i++)
                {
                    tk = (eTaiKhoan)lvwDSNhanVien.Items[i].Tag;
                    if (tk.ENhanVien.SoCMND.Contains(giaTriTim))
                        return i;
                }
            }
            else if (rdoTimTheoSDT.Checked == true)
            {
                for (int i = 0; i < lvwDSNhanVien.Items.Count; i++)
                {
                    tk = (eTaiKhoan)lvwDSNhanVien.Items[i].Tag;
                    if (tk.ENhanVien.SDT.Contains(giaTriTim))
                        return i;
                }
            }
            else
            {
                for (int i = 0; i < lvwDSNhanVien.Items.Count; i++)
                {
                    tk = (eTaiKhoan)lvwDSNhanVien.Items[i].Tag;
                    if (tk.ENhanVien.TenNV.Contains(giaTriTim))
                        return i;
                }
            }
            return -1;
        }

        private void btnTimKiemNV_Click(object sender, EventArgs e)
        {
            int kq = TimKiem(txtGiaTriTim.Text);
            if (kq == -1)
                MessageBox.Show("Không tìm thấy khách hàng có thông tin điền vào", "Thông báo");
            else
            {
                if (lvwDSNhanVien.SelectedItems.Count > 0)
                {
                    int vitritruoc = lvwDSNhanVien.SelectedIndices[0];
                    lvwDSNhanVien.Items[vitritruoc].Selected = false;
                }
                lvwDSNhanVien.Items[kq].Selected = true;
                lvwDSNhanVien.Focus();
                tkChon = (eTaiKhoan)lvwDSNhanVien.Items[kq].Tag;
                TaiHienThongTinNhanVien(tkChon);
            }
        }

        private void rdoTimTheoCMND_CheckedChanged(object sender, EventArgs e)
        {
            XuLyAutoComplete();
        }

        private void rdoTimTheoSDT_CheckedChanged(object sender, EventArgs e)
        {
            XuLyAutoComplete();
        }

        private void rdoTimTheoTen_CheckedChanged(object sender, EventArgs e)
        {
            XuLyAutoComplete();
        }
    }
}
