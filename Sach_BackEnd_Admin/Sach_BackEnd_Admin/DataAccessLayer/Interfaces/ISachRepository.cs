using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public partial interface ISachRepository
    {
        List<SachModel> GetAllSach();
        SachModel GetDatabyID(int id);
        bool Create(SachModel model);
        bool Update(SachModel model);
        List <SachModel> Search(string keyword);
        bool Delete(int id);
        List<SachModel> GetBookForCategory(int MaLoai);
    }
}
