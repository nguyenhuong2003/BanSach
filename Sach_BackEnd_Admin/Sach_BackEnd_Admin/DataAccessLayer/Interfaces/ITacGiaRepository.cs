using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{

    public partial interface ITacGiaRepository
    {
        List<TacGiaModel> GetAllTacGia();
        TacGiaModel GetDatabyID(int id);
        bool Create(TacGiaModel model);
        bool Update(TacGiaModel model);
        List<TacGiaModel> Search(string keyword);
        bool Delete(int id);

    }

}
