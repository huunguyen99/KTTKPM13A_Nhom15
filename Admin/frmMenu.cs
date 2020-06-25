using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Admin;
using Entities;

namespace NhanVienTuVan
{
    public partial class frmMenu : Form
    {
        private static int MaNV {get;set;}
        public frmMenu(eNhanVien nv)
        {
            InitializeComponent();
            MaNV = nv.MaNV;
        }
        frmDoiMatKhau frmdmk = new frmDoiMatKhau(MaNV);
        frmQuanLyNhanVien frmqlnv = new frmQuanLyNhanVien();
        private void frmMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Show();
        }

        private void mnuQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }
            if (frmqlnv.IsAccessible == false)
            {
                frmqlnv = new frmQuanLyNhanVien();
                frmqlnv.MdiParent = this;
                frmqlnv.Show();
            }
        }

        private void mnuDoiMatKhau_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }
            if (frmdmk.IsAccessible == false)
            {
                frmdmk = new frmDoiMatKhau(MaNV);
                frmdmk.MdiParent = this;
                frmdmk.Show();
            }
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }
}
