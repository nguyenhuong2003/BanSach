using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public partial interface ILoaiSachRepository
    {

       List<LoaiSachModel> GetAllLoaiSach();
        LoaiSachModel GetDatabyID(int id);
        bool Create(LoaiSachModel model);
        bool Update(LoaiSachModel model);
        List<LoaiSachModel> Search(string keyword);
        bool Delete(int id);
        List<LoaiSachModel> GetTop8();

    }
}
