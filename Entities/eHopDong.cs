using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [DataContract]
    public class eHopDong
    {
        public eHopDong()
        {
            dsHoaDon = new HashSet<eHoaDon>();
        }
        [Key]
        [DataMember]
        public int MaHopDong { get; set; }
        [DataMember]
        public int MaNV { get; set; }
        [DataMember]
        public int MaKH { get; set; }
        [DataMember]
        public int MaPhieuKTra { get; set; }
        [DataMember]
        public decimal TienCoc { get; set; }
        [DataMember]
        public DateTime NgayTao { get; set; }
        [DataMember]
        public DateTime NgayThue { get; set; }
        [DataMember]
        public DateTime NgayTra { get; set; }
        [DataMember]
        public DateTime NgayTraThucTe { get; set; }
        [DataMember]
        public virtual eNhanVien ENhanVien { get; set; }
        [DataMember]
        public virtual eKhachHang EKhachHang { get; set; }

        [DataMember]
        public virtual ePhieuYeuCauKiemTraPhong EPhieu { get; set; }
        public ICollection<eHoaDon> dsHoaDon { get; set; }

    }
}
