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
        ErrorProvider ep;
        private void frmDienThongTinPhong_Load(object sender, EventArgs e)
        {
            busvp = new BUSVanPhong();
            ep = new ErrorProvider();
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
            if (!txtMaPhong.Text.KTraMaPhong())
                ep.SetError(txtMaPhong, "Mã phòng phải theo định dạng, bắt đầu bằng p, theo sau là 3 hoặc 4 chữ số. Ex: p102");
            else if (!txtDienTich.Text.KTraDienTich())
                ep.SetError(txtDienTich, "Diện tích không thể vượt quá 1000m2");
            else if (!txtGiaThue.Text.KTraTien())
                ep.SetError(txtGiaThue, "Giá thuê không thể vượt quá 100 triệu");
            else if (!txtSoBongDen.Text.KTraSoBongDen())
                ep.SetError(txtSoBongDen, "Số bóng đèn không thể vượt quá 100 bóng");
            else if (!txtSoMayLanh.Text.KTraSoMayLanh())
                ep.SetError(txtSoMayLanh, "Số máy lạnh phải từ 3 đến 9 máy");
            else if (!txtTangLau.Text.KTraTangLau())
                ep.SetError(txtTangLau, "Tầng lầu không thể vượt quá 99 tầng");
            else
            {
                DialogResult hoithem = MessageBox.Show("Bạn có chắc chắn muốn thêm phòng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (hoithem == DialogResult.Yes)
                {
                    eVanPhong vp = TaoPhong();
                    bool kq = busvp.ThemPhong(vp);
                    if (kq == false)
                        MessageBox.Show("Mã phòng đã bị trùng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        MessageBox.Show("Thêm phòng thành công", "Thông báo");
                        this.DialogResult = DialogResult.OK;
                    }
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

        private void txtMaPhong_TextChanged(object sender, EventArgs e)
        {
            if (txtMaPhong.Text.KTraMaPhong())
                ep.Clear();
        }

        private void txtGiaThue_TextChanged(object sender, EventArgs e)
        {
            if (txtGiaThue.Text.KTraTien())
                ep.Clear();
        }

        private void txtTangLau_TextChanged(object sender, EventArgs e)
        {
            if (txtTangLau.Text.KTraTangLau())
                ep.Clear();
        }

        private void txtDienTich_TextChanged(object sender, EventArgs e)
        {
            if (txtDienTich.Text.KTraDienTich())
                ep.Clear();
        }

        private void txtSoBongDen_TextChanged(object sender, EventArgs e)
        {
            if (txtSoBongDen.Text.KTraSoBongDen())
                ep.Clear();
        }

        private void txtSoMayLanh_TextChanged(object sender, EventArgs e)
        {
            if (txtSoMayLanh.Text.KTraSoMayLanh())
                ep.Clear();
        }
    }
}
