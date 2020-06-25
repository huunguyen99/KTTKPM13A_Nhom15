using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class eHopDong
    {
        public eHopDong()
        {
            dsHoaDon = new HashSet<eHoaDon>();
        }
        [Key]
        public int MaHopDong { get; set; }
        public int MaNV { get; set; }
        public int MaKH { get; set; }
        public int MaPhieuKTra { get; set; }
        public decimal TienCoc { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayThue { get; set; }
        public DateTime NgayTra { get; set; }
        public DateTime NgayTraThucTe { get; set; }
        public Boolean TinhTrangHD { get; set; }
        public virtual eNhanVien ENhanVien { get; set; }
        public virtual eKhachHang EKhachHang { get; set; }

        public virtual ePhieuYeuCauKiemTraPhong EPhieu { get; set; }
        public ICollection<eHoaDon> dsHoaDon { get; set; }

    }
}
