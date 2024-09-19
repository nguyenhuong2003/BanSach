using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public partial interface ICT_HDNBusiness

    {
        List<CT_HDNModel> AllCT_HDN();
        CT_HDNModel GetDatabyID(int id);
        bool Create(CT_HDNModel model);
        bool Update(CT_HDNModel model);
        List<CT_HDNModel> Search(string keyword);
        bool Delete(int id);



    }
}
