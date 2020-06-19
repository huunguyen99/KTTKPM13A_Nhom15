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
    public class eVanPhong
    {
        public eVanPhong()
        {
            dsPhieu = new HashSet<ePhieuYeuCauKiemTraPhong>();
        }
        [Key]
        [DataMember]
        public string MaPhong { get; set; }
        [DataMember]
        public string TenPhong { get; set; }
        [DataMember]
        public decimal GiaThue { get; set; }
        [DataMember]
        public int TangLau { get; set; }
        [DataMember]
        public double DienTich { get; set; }
        [DataMember]
        public int SoBongDen { get; set; }
        [DataMember]
        public int SoMayLanh { get; set; }
        [DataMember]
        public Boolean TinhTrang { get; set; }
        public ICollection<ePhieuYeuCauKiemTraPhong> dsPhieu { get; set; }
    }
}
