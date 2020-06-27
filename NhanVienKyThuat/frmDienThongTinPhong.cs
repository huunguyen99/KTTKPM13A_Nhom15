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

namespace NhanVienKyThuat
{
    public partial class frmDienThongTinPhong : Form
    {
        public frmDienThongTinPhong()
        {
            InitializeComponent();
        }

        BUSVanPhong busvp;
        private void frmDienThongTinPhong_Load(object sender, EventArgs e)
        {
            busvp = new BUSVanPhong();
        }

        eVanPhong TaoPhong()
        {
            eVanPhong vp = new eVanPhong();
            vp.TenPhong = txtTenPhong.Text;
            vp.MaPhong = txtMaPhong.Text;
            vp.TangLau = Convert.ToInt32(txtTangLau.Text);
            vp.DienTich = Convert.ToDouble(txtDienTich.Text);
            vp.GiaThue = Convert.ToDecimal(txtGiaThue.Text);
            vp.SoBongDen = Convert.ToInt32(txtSoBongDen.Text);
            vp.SoMayLanh = Convert.ToInt32(txtSoMayLanh.Text);
            vp.TinhTrang = true;
            return vp;
        }

        private void btnThemPhong_Click(object sender, EventArgs e)
        {
            DialogResult hoithem = MessageBox.Show("Bạn có chắc chắn muốn thêm phòng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if(hoithem == DialogResult.Yes)
            {
                eVanPhong vp = TaoPhong();
                bool kq = busvp.ThemPhong(vp);
                if (kq == false)
                    MessageBox.Show("Mã phòng đã bị trùng", "Thông báo");
                else
                {
                    MessageBox.Show("Thêm phòng thành công", "Thông báo");
                    this.DialogResult = DialogResult.OK;
                }    
            }    
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult hoiThoat = MessageBox.Show("Bạn có chắc chắn muốn hủy không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (hoiThoat == DialogResult.Yes)
                this.Close();
        }

        private void txtGiaThue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtTangLau_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtDienTich_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtSoBongDen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtSoMayLanh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }
    }
}
