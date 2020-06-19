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
    public class ePhieuYeuCauKiemTraPhong
    {
        [Key]
        [DataMember]
        public int MaPhieuKTra { get; set; }
        [DataMember]
        public string MaPhong { get; set; }
        [DataMember]
        public int MaNV { get; set; }
        [DataMember]
        public int MaNVKyThuat { get; set; }
        [DataMember]
        public DateTime NgayTao { get; set; }
        [DataMember]
        public Boolean TinhTrangPhong { get; set; }
        [DataMember]
        public Boolean TrangThaiPhieu { get; set; }
        [DataMember]
        public string GhiChu { get; set; }
        [DataMember]
        public virtual eVanPhong EVanPhong { get; set; }
        [DataMember]
        public virtual eNhanVien ENhanVien { get; set; }

    }
}
