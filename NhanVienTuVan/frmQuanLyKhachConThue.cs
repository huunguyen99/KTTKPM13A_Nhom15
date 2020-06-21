using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NhanVienTuVan.VanPhongService;

namespace NhanVienTuVan
{
    public partial class frmQuanLyKhachConThue : Form
    {
        public frmQuanLyKhachConThue()
        {
            InitializeComponent();
        }

        ChoThueVanPhongServiceClient dt;
        List<eVanPhong> dsphong;
        private void frmQuanLyKhachConThue_Load(object sender, EventArgs e)
        {
            dt = new ChoThueVanPhongServiceClient();
            dsphong = dt.LayDanhSachPhong().ToList();
            LoadPhongLenTreeView(treDSPhong, dsphong);
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

        private void btnSuaThongTin_Click(object sender, EventArgs e)
        {

        }

        
    }
}
