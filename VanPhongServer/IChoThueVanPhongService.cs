using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DAL;

namespace VanPhongServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IChoThueVanPhongService" in both code and config file together.
    [ServiceContract]
    public interface IChoThueVanPhongService
    {
        [OperationContract]
        //public List<eVanPhong> LayDSVanPhongTrong();
        List<eVanPhong> LayDanhSachPhong();
    }
}
