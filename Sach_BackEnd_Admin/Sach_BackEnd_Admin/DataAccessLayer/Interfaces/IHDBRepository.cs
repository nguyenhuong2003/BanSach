using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public partial interface IHDBRepository
    {

        List<HDBModel> AllHDB();
        HDBModel GetDatabyID(int id);
        bool Create(HDBModel model);
        bool Update(HDBModel model);
        List<HDBModel> Search(string keyword);
        bool Delete(int id);




    }
}
