using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    
        public partial interface INhaXuatBanRepository
        {
            List<NhaXuatBanModel> GetAllNhaXuatBan();
        NhaXuatBanModel GetDatabyID(int id);
        bool Create(NhaXuatBanModel model);
        bool Update(NhaXuatBanModel model);
        List<NhaXuatBanModel> Search(string keyword);
        bool Delete(int id);

    }
    
}
