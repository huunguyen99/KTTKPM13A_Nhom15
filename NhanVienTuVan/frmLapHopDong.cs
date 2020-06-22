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
using NhanVienTuVan.VanPhongService;

namespace NhanVienTuVan
{
    public partial class frmLapHopDong : Form
    {
        public frmLapHopDong()
        {
            InitializeComponent();
        }
        ChoThueVanPhongServiceClient dt;
        List<eVanPhong> dsphongtrong;
        string maPhongChon;

        private void frmLapHopDong_Load(object sender, EventArgs e)
        {
            dt = new ChoThueVanPhongServiceClient();
            dsphongtrong = dt.LayDSVanPhongTrong().ToList();
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
            lvwitem.SubItems.Add(p.MaNV.ToString());
            lvwitem.SubItems.Add(p.MaNVKyThuat.ToString());
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
                //List<ePhieuYeuCauKiemTraPhong> dsphieu = dt.LayDSPhieuDaDuyet(maPhongChon).ToList();
                //LoadPhieuKiemTraLenListView(dsphieu, lvwDSPhieuKiemTra);
            }    
        }
        private void btnYeuCauKiemTraPhong_Click(object sender, EventArgs e)
        {

        }

        
    }
}
