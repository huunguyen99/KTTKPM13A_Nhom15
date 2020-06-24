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
                txtChucVu.Text = "Nhân Viên Tư Vấn";
            else if(tk.ENhanVien.ChucVu == 2)
                txtChucVu.Text = "Nhân Viên Kỹ Thuật";
            else
                txtChucVu.Text = "Nhân Viên Kế Toán";
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
    }
}
