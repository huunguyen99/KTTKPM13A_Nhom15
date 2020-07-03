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

namespace NhanVienKyThuat
{
    public partial class frmDanhSachPhieuYeuCauKiemTra : Form
    {
        private static int MaNV { get; set; }
        public frmDanhSachPhieuYeuCauKiemTra(int manv)
        {
            InitializeComponent();
            MaNV = manv;
        }
        BUSPhieuYeuCauKiemTraPhong busphieu;
        List<ePhieuYeuCauKiemTraPhong> dsphieu;
        private void frmDanhSachPhieuYeuCauKiemTra_Load(object sender, EventArgs e)
        {
            busphieu = new BUSPhieuYeuCauKiemTraPhong();
            dsphieu = busphieu.LayDSPhieuChuaDuyet();
            LoadPhieuKiemTraLenListView(dsphieu, lvwDSPhieu);
        }

        void ThemItem(ePhieuYeuCauKiemTraPhong p, ListView lvw)
        {
            ListViewItem lvwitem = new ListViewItem(p.MaPhieuKTra.ToString());
            lvwitem.SubItems.Add(p.EVanPhong.TenPhong);
            lvwitem.SubItems.Add(p.ENhanVien.TenNV.ToString());
            lvwitem.SubItems.Add(p.NgayTao.ToString("dd/MM/yyyy"));
            if (p.EVanPhong.SoBongDen < 15)
                lvwitem.SubItems.Add("Phòng đang thiếu bóng đèn");
            else if (p.EVanPhong.SoMayLanh <= 2)
                lvwitem.SubItems.Add("Máy điều hòa đang hỏng");
            else
                lvwitem.SubItems.Add("Phòng tốt");
            lvwitem.SubItems.Add("Chưa duyệt");
            lvwitem.Tag = p;
            lvw.Items.Add(lvwitem);
        }

        void LoadPhieuKiemTraLenListView(List<ePhieuYeuCauKiemTraPhong> ds, ListView lvw)
        {
            lvw.Items.Clear();
            foreach (ePhieuYeuCauKiemTraPhong item in ds)
            {
                ThemItem(item, lvw);
            }
        }
        ePhieuYeuCauKiemTraPhong phChon;
        private void lvwDSPhieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwDSPhieu.SelectedItems.Count > 0)
            {
                phChon = (ePhieuYeuCauKiemTraPhong)lvwDSPhieu.SelectedItems[0].Tag;
            }
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            if (lvwDSPhieu.SelectedItems.Count > 0)
            {
                if (rtxtGhichu.Text.Trim().Length == 0)
                    MessageBox.Show("Vui lòng điền vào ghi chú cho nhân viên tư vấn", "Thông báo");
                else if (phChon.EVanPhong.SoBongDen <= 15 || phChon.EVanPhong.SoMayLanh <= 2)
                    MessageBox.Show("Phòng này không thể cho khách thuê", "Thông báo");
                else
                {
                    DialogResult hoi = MessageBox.Show("Bạn có chắc chắn muốn duyệt cho thuê phòng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (hoi == DialogResult.Yes)
                    {
                        busphieu.DuyetPhieu(phChon, MaNV, true, rtxtGhichu.Text);
                        MessageBox.Show("Duyệt phiếu thành công", "Thông báo");
                        dsphieu = busphieu.LayDSPhieuChuaDuyet();
                        LoadPhieuKiemTraLenListView(dsphieu, lvwDSPhieu);
                        rtxtGhichu.Clear();
                    }
                }    
            }
            else
                MessageBox.Show("Vui lòng chọn phiếu cần duyệt", "Thông báo");
        }

        

        private void btnKhongduyet_Click(object sender, EventArgs e)
        {
            if (lvwDSPhieu.SelectedItems.Count > 0)
            {
                if (rtxtGhichu.Text.Trim().Length == 0)
                    MessageBox.Show("Vui lòng điền vào ghi chú cho nhân viên tư vấn", "Thông báo");
                else
                {
                    DialogResult hoi = MessageBox.Show("Bạn có chắc chắn không muốn cho thuê phòng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (hoi == DialogResult.Yes)
                    {
                        busphieu.DuyetPhieu(phChon, MaNV, false, rtxtGhichu.Text);
                        MessageBox.Show("Duyệt phiếu thành công", "Thông báo");
                        dsphieu = busphieu.LayDSPhieuChuaDuyet();
                        LoadPhieuKiemTraLenListView(dsphieu, lvwDSPhieu);
                        rtxtGhichu.Clear();
                    }
                }
            }
            else
                MessageBox.Show("Vui lòng chọn phiếu cần duyệt", "Thông báo");
        }

        private void btnNhanPhieu_Click(object sender, EventArgs e)
        {
            dsphieu = busphieu.LayDSPhieuChuaDuyet();
            LoadPhieuKiemTraLenListView(dsphieu, lvwDSPhieu);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult hoiThoat = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (hoiThoat == DialogResult.Yes)
                this.Close();
        }

        
    }
}
