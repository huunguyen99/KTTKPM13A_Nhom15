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
    public class eHoaDon
    {
        public eHoaDon()
        {
            dsCTHD = new HashSet<eChiTietHoaDon>();
        }
        [Key]
        [DataMember]
        public int MaHoaDon { get; set; }
        [DataMember]
        public int MaNV { get; set; }
        [DataMember]
        public int MaHopDong { get; set; }
        [DataMember]
        public DateTime NgayLapHoaDon { get; set; }
        [DataMember]
        public DateTime NgayCanLap { get; set; }
        [DataMember]
        public DateTime NgayThanhToan { get; set; }
        [DataMember]
        public Boolean TinhTrangHD { get; set; }
        [DataMember]
        public virtual eNhanVien ENhanVien { get; set; }
        [DataMember]
        public virtual eHopDong EHopDong { get; set; }
        public ICollection<eChiTietHoaDon> dsCTHD { get; set; }
    }
}
