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

namespace NhanVienTuVan
{
    public partial class frmMenu : Form
    {
        private static int MaNV {get;set;}
        public frmMenu(eNhanVien nv)
        {
            InitializeComponent();
            
        }

        frmQuanLyKhachConThue frmkhachconthue = new frmQuanLyKhachConThue();
        frmQuanLyKhachKhongConThue frmkhachkhongconthue = new frmQuanLyKhachKhongConThue();
        frmDoiMatKhau frmdmk = new frmDoiMatKhau(MaNV);
        frmLapHopDong frmlaphopdong = new frmLapHopDong(MaNV);
        frmTraPhong frmtp = new frmTraPhong();
        frmQuanLyHopDong frmqlhd = new frmQuanLyHopDong();

        private void frmMenu_Load(object sender, EventArgs e)
        {

        }

        private void frmMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void mnuKHConThue_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }
            if (frmkhachconthue.IsAccessible == false)
            {
                frmkhachconthue = new frmQuanLyKhachConThue();
                frmkhachconthue.MdiParent = this;
                frmkhachconthue.Show();
            }
        }

        private void mnuKHKhongConThue_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }
            if (frmkhachkhongconthue.IsAccessible == false)
            {
                frmkhachkhongconthue = new frmQuanLyKhachKhongConThue();
                frmkhachkhongconthue.MdiParent = this;
                frmkhachkhongconthue.Show();
            }
        }

        private void mnuHopDong_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }
            if (frmqlhd.IsAccessible == false)
            {
                frmqlhd = new frmQuanLyHopDong();
                frmqlhd.MdiParent = this;
                frmqlhd.Show();
            }
        }

        private void mnuLapHopDong_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }
            if (frmlaphopdong.IsAccessible == false)
            {
                frmlaphopdong = new frmLapHopDong(MaNV);
                frmlaphopdong.MdiParent = this;
                frmlaphopdong.Show();
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

        }

        private void mnuTraPhong_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }
            if (frmtp.IsAccessible == false)
            {
                frmtp = new frmTraPhong();
                frmtp.MdiParent = this;
                frmtp.Show();
            }
        }
    }
}
