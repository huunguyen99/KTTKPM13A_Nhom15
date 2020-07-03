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
    public partial class frmQuanLyPhong : Form
    {
        public frmQuanLyPhong()
        {
            InitializeComponent();
        }

        BUSVanPhong busvp;
        List<eVanPhong> dsvp;
        ErrorProvider ep;
        private void frmQuanLyPhong_Load(object sender, EventArgs e)
        {
            busvp = new BUSVanPhong();
            dsvp = busvp.LayDanhSachPhong();
            LoadDSPhongLenListView(dsvp, lvwDSPhong);
            ep = new ErrorProvider();
            XuLyAutoComplete();
        }

        void ThemItem(eVanPhong p, ListView lvw)
        {
            ListViewItem lvwitem = new ListViewItem(p.MaPhong);
            lvwitem.SubItems.Add(p.TenPhong);
            lvwitem.SubItems.Add(p.TangLau.ToString());
            string giathue = string.Format("{0:0,0 VNĐ}", p.GiaThue);
            lvwitem.SubItems.Add(giathue);
            lvwitem.SubItems.Add(p.DienTich.ToString() + "m2");
            lvwitem.SubItems.Add(p.SoMayLanh.ToString());
            lvwitem.SubItems.Add(p.SoBongDen.ToString());
            if (p.TinhTrang == false)
                lvwitem.SubItems.Add("Phòng đang hỏng");
            else
                lvwitem.SubItems.Add("Phòng tốt");
            lvwitem.Tag = p;
            lvw.Items.Add(lvwitem);
        }

        void LoadDSPhongLenListView(List<eVanPhong> ds, ListView lvw)
        {
            lvw.Items.Clear();
            foreach (eVanPhong item in ds)
            {
                ThemItem(item, lvw);
            }
        }

        void TaiHienThongTinVanPhong(eVanPhong p)
        {
            txtDienTich.Text = p.DienTich.ToString();
            txtGiaThue.Text = p.GiaThue.ToString();
            txtMaPhong.Text = p.MaPhong;
            txtSoBongDen.Text = p.SoBongDen.ToString();
            txtSoMayLanh.Text = p.SoMayLanh.ToString();
            txtTangLau.Text = p.TangLau.ToString();
            txtTenPhong.Text = p.TenPhong;
            if (p.TinhTrang == false)
                txtTinhTrang.Text = "Phòng đang hỏng";
            else
                txtTinhTrang.Text = "Phòng tốt";
        }
        eVanPhong vpChon;
        private void lvwDSPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvwDSPhong.SelectedItems.Count > 0)
            {
                vpChon = (eVanPhong)lvwDSPhong.SelectedItems[0].Tag;
                TaiHienThongTinVanPhong(vpChon);
                if (vpChon.TinhTrang == false)
                    btnXoa.Text = "TÁI SỬ DỤNG";
                else
                    btnXoa.Text = "XÓA";
            }    
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(lvwDSPhong.SelectedItems.Count > 0)
            {
                if(vpChon.TinhTrang == false)
                {
                    DialogResult hoi = MessageBox.Show("Bạn có chắc chắn muốn tái sử dụng lại phòng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if(hoi == DialogResult.Yes)
                    {
                        bool kq = busvp.Xoa_TaiSuDung_Phong(vpChon, true);
                        if (kq == true)
                            MessageBox.Show("Tái sử dụng phòng thành công.", "Thông báo");
                        dsvp = busvp.LayDanhSachPhong();
                        LoadDSPhongLenListView(dsvp, lvwDSPhong);
                    }    
                }    
                else
                {
                    DialogResult hoi = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (hoi == DialogResult.Yes)
                    {
                        bool kq = busvp.Xoa_TaiSuDung_Phong(vpChon, false);
                        if (kq == true)
                            MessageBox.Show("Xóa phòng thành công.", "Thông báo");
                        else
                            MessageBox.Show("Phòng này đang cho thuê, không thể xóa đi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dsvp = busvp.LayDanhSachPhong();
                        LoadDSPhongLenListView(dsvp, lvwDSPhong);
                    }
                }    
            }    
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnXoa.Text = "XÓA";
            frmDienThongTinPhong frm = new frmDienThongTinPhong();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                dsvp = busvp.LayDanhSachPhong();
                LoadDSPhongLenListView(dsvp, lvwDSPhong);
            }    
        }
        
        eVanPhong TaoPhongSua()
        {
            eVanPhong vp = new eVanPhong();
            vp.DienTich = Convert.ToDouble(txtDienTich.Text);
            vp.SoBongDen = Convert.ToInt32(txtSoBongDen.Text);
            vp.SoMayLanh = Convert.ToInt32(txtSoMayLanh.Text);
            vp.GiaThue = Convert.ToDecimal(txtGiaThue.Text);
            vp.TenPhong = txtTenPhong.Text;
            vp.TangLau = Convert.ToInt32(txtTangLau.Text);
            return vp;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            btnXoa.Text = "XÓA";
            if (lvwDSPhong.SelectedItems.Count > 0)
            {
                if (!txtDienTich.Text.KTraDienTich())
                    ep.SetError(txtDienTich, "Diện tích không thể vượt quá 1000m2");
                else if (!txtGiaThue.Text.KTraTien())
                    ep.SetError(txtGiaThue, "Giá thuê không thể vượt quá 100 triệu");
                else if (!txtSoBongDen.Text.KTraSoBongDen())
                    ep.SetError(txtSoBongDen, "Số bóng đèn không thể vượt quá 100 bóng");
                else if (!txtTangLau.Text.KTraTangLau())
                    ep.SetError(txtTangLau, "Tầng lầu không thể vượt quá 99 tầng");
                else
                {
                    DialogResult hoi = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin phòng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (hoi == DialogResult.Yes)
                    {
                        eVanPhong vpSua = TaoPhongSua();
                        busvp.SuaPhong(vpChon, vpSua);
                        dsvp = busvp.LayDanhSachPhong();
                        LoadDSPhongLenListView(dsvp, lvwDSPhong);
                    }
                }
            }
            else
                MessageBox.Show("Vui lòng chọn phòng cần chỉnh sửa thông tin", "Thông báo");
        }

        void XuLyAutoComplete()
        {
            txtGiaTriTim.AutoCompleteCustomSource.Clear();
            for(int i = 0; i < lvwDSPhong.Items.Count; i++)
            {
                eVanPhong vp = (eVanPhong)lvwDSPhong.Items[i].Tag;
                txtGiaTriTim.AutoCompleteCustomSource.Add(vp.MaPhong);
            }    
        }

        int TimKiem(string giaTriTim)
        {
            for(int i = 0; i < lvwDSPhong.Items.Count; i++)
            {
                eVanPhong vp = (eVanPhong)lvwDSPhong.Items[i].Tag;
                if (vp.MaPhong.ToLower().Contains(giaTriTim.ToLower()) || vp.TenPhong.ToLower().Contains(giaTriTim.ToLower()))
                    return i;
            }
            return -1;
        }

        private void btnTimKiemPhong_Click(object sender, EventArgs e)
        {
            btnXoa.Text = "XÓA";
            if (txtGiaTriTim.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng nhập phòng cần tìm");
            else
            {
                int i = TimKiem(txtGiaTriTim.Text);
                if (i == -1)
                    MessageBox.Show("Không có phòng nào có thông tin như yêu cầu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None);
                else
                {
                    if(lvwDSPhong.SelectedItems.Count > 0)
                    {
                        int vitritruoc = lvwDSPhong.SelectedIndices[0];
                        lvwDSPhong.Items[vitritruoc].Selected = false;
                    }
                    lvwDSPhong.Items[i].Selected = true;
                    lvwDSPhong.Focus();
                }    
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult hoiThoat = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
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

        private void txtSoMayLanh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }

        private void txtSoBongDen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8))
                e.Handled = true;
        }
    }
}
