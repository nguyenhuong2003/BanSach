using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public partial interface INhaXuatBanBusiness

    {
        List<NhaXuatBanModel> GetAllNhaXuatBan();
        NhaXuatBanModel GetDatabyID(int id);
        bool Create(NhaXuatBanModel model);
        bool Update(NhaXuatBanModel model);
        List<NhaXuatBanModel> Search(string keyword);
        bool Delete(int id);



    }
}
