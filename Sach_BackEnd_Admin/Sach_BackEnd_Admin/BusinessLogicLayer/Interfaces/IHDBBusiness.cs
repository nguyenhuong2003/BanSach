using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public partial interface IHDBBusiness

    {
        List<HDBModel> AllHDB();
        HDBModel GetDatabyID(int id);
        bool Create(HDBModel model);
        bool Update(HDBModel model);
        List<HDBModel> Search(string keyword);
        bool Delete(int id);



    }
}
