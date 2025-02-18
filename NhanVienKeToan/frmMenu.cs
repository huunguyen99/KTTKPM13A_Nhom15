﻿using BUS;
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

namespace NhanVienKeToan
{
    public partial class frmMenu : Form
    {
        private static int MaNV { get; set; }
        public frmMenu(eNhanVien nv)
        {
            MaNV = nv.MaNV;
            InitializeComponent();
        }
        frmQuanLyHoaDon frmqlhd = new frmQuanLyHoaDon();
        frmLapHoaDon frmlhd = new frmLapHoaDon(MaNV);
        frmDanhSachHoaDonChuaThanhToan frmthanhtoan = new frmDanhSachHoaDonChuaThanhToan();
        private void mnuQuanLyHoaDon_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }
            if (frmqlhd.IsAccessible == false)
            {
                frmqlhd = new frmQuanLyHoaDon();
                frmqlhd.MdiParent = this;
                frmqlhd.Show();
            }
        }

        private void mnuLapHoaDon_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }
            if (frmlhd.IsAccessible == false)
            {
                frmlhd = new frmLapHoaDon(MaNV);
                frmlhd.MdiParent = this;
                frmlhd.Show();
            }
        }
        BUSHopDong bushd;
        private void frmMenu_Load(object sender, EventArgs e)
        {
            bushd = new BUSHopDong();
            bushd.AutoKetThucHopDong();
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

        private void mnuThanhToan_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }
            if (frmthanhtoan.IsAccessible == false)
            {
                frmthanhtoan = new frmDanhSachHoaDonChuaThanhToan();
                frmthanhtoan.MdiParent = this;
                frmthanhtoan.Show();
            }
        }
        frmDoiMatKhau frmdmk = new frmDoiMatKhau(MaNV);
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
    }
}
