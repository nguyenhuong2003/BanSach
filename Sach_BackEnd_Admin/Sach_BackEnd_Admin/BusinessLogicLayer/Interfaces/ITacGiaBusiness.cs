using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public partial interface ITacGiaBusiness

    {
        List<TacGiaModel> GetAllTacGia();
        TacGiaModel GetDatabyID(int id);
        bool Create(TacGiaModel model);
        bool Update(TacGiaModel model);
        List<TacGiaModel> Search(string keyword);
        bool Delete(int id);



    }
}
