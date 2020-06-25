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
    public partial class frmLapHopDong : Form
    {
        private static int MaNV { get; set; }
        public frmLapHopDong(int manv)
        {
            InitializeComponent();
            MaNV = manv;
        }
        BUSVanPhong busvp;
        BUSPhieuYeuCauKiemTraPhong busphieu;
        List<eVanPhong> dsphongtrong;
        string maPhongChon;
        eVanPhong phongChon;

        private void frmLapHopDong_Load(object sender, EventArgs e)
        {
            busvp = new BUSVanPhong();
            busphieu = new BUSPhieuYeuCauKiemTraPhong();
            dsphongtrong = busvp.LayDSVanPhongTrong().ToList();
            LoadPhongLenTreeView(treDSPhong, dsphongtrong);
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

        void ThemItem(ePhieuYeuCauKiemTraPhong p, ListView lvw)
        {
            ListViewItem lvwitem = new ListViewItem(p.MaPhieuKTra.ToString());
            lvwitem.SubItems.Add(p.ENhanVien.TenNV.ToString());
            lvwitem.SubItems.Add(p.NgayTao.ToString("dd/MM/yyyy"));
            if(p.TrangThaiPhieu == false)
            {
                lvwitem.SubItems.Add("Chưa kiểm tra");
                lvwitem.SubItems.Add("Chưa duyệt");
            }    
            else
            {
                if (p.TinhTrangPhong == false)
                    lvwitem.SubItems.Add("Phòng Hỏng");
                else
                    lvwitem.SubItems.Add("Phòng dùng được");
                lvwitem.SubItems.Add("Đã duyệt");
            }
            lvwitem.Tag = p;
            lvw.Items.Add(lvwitem);
        }

        void LoadPhieuKiemTraLenListView(List<ePhieuYeuCauKiemTraPhong> ds, ListView lvw)
        {
            lvw.Items.Clear();
            foreach(ePhieuYeuCauKiemTraPhong item in ds)
            {
                ThemItem(item, lvw);
            }    
        }
        private void treDSPhong_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(treDSPhong.SelectedNode != null)
            {
                maPhongChon = treDSPhong.SelectedNode.Tag.ToString();
                phongChon = dsphongtrong.Where(x => x.MaPhong == maPhongChon).FirstOrDefault();
                List<ePhieuYeuCauKiemTraPhong> dsphieu = busphieu.LayDSPhieuDaDuyet(maPhongChon).ToList();
                LoadPhieuKiemTraLenListView(dsphieu, lvwDSPhieuKiemTra);
            }    
        }

        ePhieuYeuCauKiemTraPhong TaoPhieKiemTra()
        {
            ePhieuYeuCauKiemTraPhong ph = new ePhieuYeuCauKiemTraPhong();
            ph.TrangThaiPhieu = false;
            ph.TinhTrangPhong = false;
            ph.MaPhong = maPhongChon;
            ph.MaNV = MaNV;
            ph.GhiChu = null;
            ph.NgayTao = DateTime.Now;
            return ph;
        }
        private void btnYeuCauKiemTraPhong_Click(object sender, EventArgs e)
        {
            if (treDSPhong.SelectedNode != null)
            {
                if (busphieu.KiemTraPhongDaGuiPhieuKiemTraChua(maPhongChon) == true)
                    MessageBox.Show("Bạn đã gửi yêu cầu kiểm tra thông tin cho phòng này rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                else
                {
                    DialogResult hoi = MessageBox.Show("Bạn có chắc chắn muốn gửi yêu cầu kiểm tra thông tin phòng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (hoi == DialogResult.Yes)
                    {
                        ePhieuYeuCauKiemTraPhong ph = TaoPhieKiemTra();
                        busphieu.TaoPhieuKiemTra(ph);
                        MessageBox.Show("Gửi yêu cầu kiểm tra phòng thành công", "Thông báo");
                        List<ePhieuYeuCauKiemTraPhong> dsphieu = busphieu.LayDSPhieuDaDuyet(maPhongChon).ToList();
                        LoadPhieuKiemTraLenListView(dsphieu, lvwDSPhieuKiemTra);
                    }
                }
            }
            else
                MessageBox.Show("Vui lòng chọn phòng cần kiểm tra", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        ePhieuYeuCauKiemTraPhong phChon;
        private void lvwDSPhieuKiemTra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvwDSPhieuKiemTra.SelectedItems.Count > 0)
            {
                phChon = (ePhieuYeuCauKiemTraPhong)lvwDSPhieuKiemTra.SelectedItems[0].Tag;
                rtxtGhiChu.Text = phChon.GhiChu;
            }    
        }

        private void lvwDSPhieuKiemTra_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(phChon.TinhTrangPhong == false && phChon.TrangThaiPhieu == true)
            {
                MessageBox.Show("Phòng này đang hỏng. Không thể cho thuê", "Thông báo");
                rtxtGhiChu.Clear();
                busphieu.XoaPhieuKiemTra(phChon.MaPhieuKTra);
                List<ePhieuYeuCauKiemTraPhong> dsphieu = busphieu.LayDSPhieuDaDuyet(maPhongChon).ToList();
                LoadPhieuKiemTraLenListView(dsphieu, lvwDSPhieuKiemTra);
            }    
        }

        private void btnLapHopDong_Click(object sender, EventArgs e)
        {
            if (lvwDSPhieuKiemTra.SelectedItems.Count > 0)
            {
                if (phChon.TinhTrangPhong == false)
                {
                    MessageBox.Show("Phòng này đang hỏng. Không thể cho thuê", "Thông báo");
                }
                else
                {
                    DialogResult hoi = MessageBox.Show("Khách hàng đã từng thuê văn phòng ở đây chưa?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (hoi == DialogResult.Yes)
                    {
                        frmDanhSachTatCaKhachHang frmdskh = new frmDanhSachTatCaKhachHang(MaNV, phChon.MaPhieuKTra, phongChon.GiaThue);
                        if (frmdskh.ShowDialog() == DialogResult.OK)
                        {
                            dsphongtrong = busvp.LayDSVanPhongTrong().ToList();
                            LoadPhongLenTreeView(treDSPhong, dsphongtrong);
                        }
                    }
                    else if(hoi == DialogResult.No)
                    {
                        frmDienThongTinKhachHang frmkh = new frmDienThongTinKhachHang(MaNV, phChon.MaPhieuKTra, phongChon.GiaThue);
                        if(frmkh.ShowDialog() == DialogResult.OK)
                        {
                            dsphongtrong = busvp.LayDSVanPhongTrong().ToList();
                            LoadPhongLenTreeView(treDSPhong, dsphongtrong);
                        }    
                    }    
                }
            }
            else
                MessageBox.Show("Vui lòng kiểm tra phòng trước khi tạo hợp đồng cho khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult hoiThoat = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (hoiThoat == DialogResult.Yes)
                this.Close();
        }
    }
}
