using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ePhieuYeuCauKiemTraPhong
    {
        [Key]
        public int MaPhieuKTra { get; set; }
        public string MaPhong { get; set; }
        public int MaNV { get; set; }
        public int MaNVKyThuat { get; set; }
        public DateTime NgayTao { get; set; }
        public Boolean TinhTrangPhong { get; set; }
        public Boolean TrangThaiPhieu { get; set; }
        public string GhiChu { get; set; }
        public virtual eVanPhong EVanPhong { get; set; }
        public virtual eNhanVien ENhanVien { get; set; }

    }
}
