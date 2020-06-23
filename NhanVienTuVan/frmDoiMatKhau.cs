using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhanVienTuVan
{
    public partial class frmDoiMatKhau : Form
    {
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult hoiThoat = MessageBox.Show("Bạn có chắc chắn muốn hủy không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (hoiThoat == DialogResult.Yes)
                this.Close();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {

        }
    }
}
