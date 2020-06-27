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
        public frmMenu()
        {
            InitializeComponent();
        }
        frmQuanLyHoaDon frmqlhd = new frmQuanLyHoaDon();
        private void kryptonRibbonGroupButton1_Click(object sender, EventArgs e)
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
    }
}
