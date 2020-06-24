using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class eHoaDon
    {
        public eHoaDon()
        {
            dsCTHD = new HashSet<eChiTietHoaDon>();
        }
        [Key]
        public int MaHoaDon { get; set; }
        public int MaNV { get; set; }
        public int MaHopDong { get; set; }
        public DateTime NgayLapHoaDon { get; set; }
        public DateTime NgayCanLap { get; set; }
        public DateTime NgayThanhToan { get; set; }
        public Boolean TinhTrangHD { get; set; }
        public virtual eNhanVien ENhanVien { get; set; }
        public virtual eHopDong EHopDong { get; set; }
        public ICollection<eChiTietHoaDon> dsCTHD { get; set; }
    }
}
