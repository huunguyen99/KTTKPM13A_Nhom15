using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhanVienKyThuat
{
    public partial class frmMenu : Form
    {
        private static int MaNV { get; set; }
        public frmMenu(eNhanVien nv)
        {
            InitializeComponent();
            MaNV = nv.MaNV;
            
        }
        frmDanhSachPhieuYeuCauKiemTra frmktp = new frmDanhSachPhieuYeuCauKiemTra(MaNV);
        frmQuanLyPhong frmqlp = new frmQuanLyPhong( );
        frmDoiMatKhau frmdmk = new frmDoiMatKhau(MaNV);
        private void mnuKiemTraPhong_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }
            if (frmktp.IsAccessible == false)
            {
                frmktp = new frmDanhSachPhieuYeuCauKiemTra(MaNV);
                frmktp.MdiParent = this;
                frmktp.Show();
            }
        }

        private void mnuQLPhong_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }
            if (frmqlp.IsAccessible == false)
            {
                frmqlp = new frmQuanLyPhong();
                frmqlp.MdiParent = this;
                frmqlp.Show();
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

        private void frmMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Show();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
