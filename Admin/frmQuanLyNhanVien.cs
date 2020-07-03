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
        ErrorProvider ep;
        private void frmQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            busnvtk = new BUSNhanVienVaTaiKhoan();
            dsnvtk = busnvtk.LayDSTaiKhoanVaNhanVienDangLamViec();
            LoadDSNhanVienLenListView(dsnvtk, lvwDSNhanVien);
            ItemsCboChucVu(cboChucVu);
            XuLyAutoComplete();
            ep = new ErrorProvider();
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
            if (tk.ENhanVien.ChucVu == 1)
                lvwitem.SubItems.Add("Nhân Viên Tư Vấn");
            else if (tk.ENhanVien.ChucVu == 2)
                lvwitem.SubItems.Add("Nhân Viên Kỹ Thuật");
            else
                lvwitem.SubItems.Add("Nhân Viên Kế Toán");
            lvwitem.Tag = tk;
            lvw.Items.Add(lvwitem);
        }

        void LoadDSNhanVienLenListView(List<eTaiKhoan> ds, ListView lvw)
        {
            lvw.Items.Clear();
            foreach (var item in ds)
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
            if (lvwDSNhanVien.SelectedItems.Count > 0)
            {
                tkChon = (eTaiKhoan)lvwDSNhanVien.SelectedItems[0].Tag;
                TaiHienThongTinNhanVien(tkChon);
            }
        }

        private void btnThemNhanVien_Click(object sender, EventArgs e)
        {
            if (rdoDSNhanVienDaNghiViec.Checked == true)
                MessageBox.Show("Không thể thêm nhân viên vào danh sách này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                frmThemNhanVien frmtnv = new frmThemNhanVien();
                if (frmtnv.ShowDialog() == DialogResult.OK)
                {
                    dsnvtk = busnvtk.LayDSTaiKhoanVaNhanVienDangLamViec();
                    LoadDSNhanVienLenListView(dsnvtk, lvwDSNhanVien);
                }
            }
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            if (lvwDSNhanVien.SelectedItems.Count > 0)
            {
                if (tkChon.ENhanVien.Active == false)
                    MessageBox.Show("Nhân viên này đã bị xa thải rồi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DialogResult hoiXoa = MessageBox.Show("Bạn có chắc chắn muốn xa thải nhân viên này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (hoiXoa == DialogResult.Yes)
                    {
                        busnvtk.XoaNV(tkChon);
                        MessageBox.Show("Xa thải nhân viên thành công", "Thông báo");
                        dsnvtk = busnvtk.LayDSTaiKhoanVaNhanVienDangLamViec();
                        LoadDSNhanVienLenListView(dsnvtk, lvwDSNhanVien);
                    }
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
                if (tkChon.ENhanVien.Active == false)
                    MessageBox.Show("Nhân viên này đã bị xa thải. Không thể sửa thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if ((DateTime.Now - dtpNgaySinh.Value).TotalDays < 18 * 365 + 4)
                        MessageBox.Show("Ngày sinh không hợp lệ, nhân viên chưa đủ 18 tuổi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (txtTenNV.Text.Trim().Length == 0 || txtEmail.Text.Trim().Length == 0 || txtSoCMND.Text.Trim().Length == 0 || txtSoDT.Text.Trim().Length == 0 || rtxtDiaChi.Text.Trim().Length == 0 || rtxtQueQuan.Text.Trim().Length == 0)
                        MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else if (!txtEmail.Text.KtraEmail())
                        ep.SetError(txtEmail, "Email phải đúng định dạng bắt đầu bằng chữ cái. Ex: abc123@gmail.com");
                    else if (!txtSoCMND.Text.KtraSCMND())
                        ep.SetError(txtSoCMND, "Số chứng minh(căn cước) phải là số, có 9 hoặc 12 số, bắt đầu bằng 1-9");
                    else if (!txtSoDT.Text.KtraSDT())
                        ep.SetError(txtSoDT, "Số điện thoại phải là số và có 10 số, bắt đầu bằng 01, 03, 05, 07 hoặc 09");
                    else
                    {
                        DialogResult hoiSua = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin nhân viên này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (hoiSua == DialogResult.Yes)
                        {
                            eNhanVien nv = TaoNVSua();
                            busnvtk.SuaTTNhanVien(tkChon.MaNV, nv);
                            MessageBox.Show("Sửa thông tin nhân viên thành công", "Thông báo");
                            dsnvtk = busnvtk.LayDSTaiKhoanVaNhanVienDangLamViec();
                            LoadDSNhanVienLenListView(dsnvtk, lvwDSNhanVien);
                        }
                    }
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


        List<eTaiKhoan> TimKiemNV(string giaTriTim)
        {
            List<eTaiKhoan> dsTim = new List<eTaiKhoan>();
            if (rdoTimTheoCMND.Checked == true)
            {
                dsTim = dsnvtk.Where(x => x.ENhanVien.SoCMND.Contains(giaTriTim)).ToList();
            }
            else if (rdoTimTheoSDT.Checked == true)
            {
                dsTim = dsnvtk.Where(x => x.ENhanVien.SDT.Contains(giaTriTim)).ToList();
            }
            else
            {
                dsTim = dsnvtk.Where(x => x.ENhanVien.TenNV.ToLower().Contains(giaTriTim.ToLower())).ToList();
            }
            return dsTim;
        }
        private void btnTimKiemNV_Click(object sender, EventArgs e)
        {
            List<eTaiKhoan> kquaTim = TimKiemNV(txtGiaTriTim.Text);
            if (kquaTim.Count == 0)
            {
                MessageBox.Show("Không tìm thấy nhân viên nào có thông tin như yêu cầu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadDSNhanVienLenListView(dsnvtk, lvwDSNhanVien);
            }
            else
                LoadDSNhanVienLenListView(kquaTim, lvwDSNhanVien);
        }

        private void rdoTimTheoCMND_CheckedChanged(object sender, EventArgs e)
        {
            XuLyAutoComplete();
            LoadDSNhanVienLenListView(dsnvtk, lvwDSNhanVien);
        }

        private void rdoTimTheoSDT_CheckedChanged(object sender, EventArgs e)
        {
            XuLyAutoComplete();
            LoadDSNhanVienLenListView(dsnvtk, lvwDSNhanVien);
        }

        private void rdoTimTheoTen_CheckedChanged(object sender, EventArgs e)
        {
            XuLyAutoComplete();
            LoadDSNhanVienLenListView(dsnvtk, lvwDSNhanVien);
        }

        private void txtSoCMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtSoDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtGiaTriTim_TextChanged(object sender, EventArgs e)
        {
            if (txtGiaTriTim.Text == "")
                LoadDSNhanVienLenListView(dsnvtk, lvwDSNhanVien);
        }

        private void txtSoCMND_TextChanged(object sender, EventArgs e)
        {
            if (txtSoCMND.Text.KtraSCMND())
                ep.Clear();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (txtEmail.Text.KtraEmail())
                ep.Clear();
        }

        private void txtSoDT_TextChanged(object sender, EventArgs e)
        {
            if (txtSoDT.Text.KtraSDT())
                ep.Clear();
        }

        private void rdoDSNhanVienDangLam_CheckedChanged(object sender, EventArgs e)
        {
            
            dsnvtk = busnvtk.LayDSTaiKhoanVaNhanVienDangLamViec();
            LoadDSNhanVienLenListView(dsnvtk, lvwDSNhanVien);
            XuLyAutoComplete();
        }

        private void rdoDSNhanVienDaNghiViec_CheckedChanged(object sender, EventArgs e)
        {
            
            dsnvtk = busnvtk.LayDSTaiKhoanVaNhanVienDaNghiViec();
            LoadDSNhanVienLenListView(dsnvtk, lvwDSNhanVien);
            XuLyAutoComplete();
        }
    }
}
