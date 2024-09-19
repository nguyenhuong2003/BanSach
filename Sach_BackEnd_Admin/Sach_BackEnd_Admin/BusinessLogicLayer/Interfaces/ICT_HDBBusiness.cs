using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public partial interface ICT_HDBBusiness

    {
        List<CT_HDBModel> AllCT_HDB();
        CT_HDBModel GetDatabyID(int id);
        bool Create(CT_HDBModel model);
        bool Update(CT_HDBModel model);
        List<CT_HDBModel> Search(string keyword);
        bool Delete(int id);
        bool DeleteCheckOut(int MaCheckOut);
        List<CheckOutModel1> GetAllCheckOut();
        bool CreateCheckOut(CheckOutModel model);
        DetailCheckOutModel GetDetail(int MaCheckout);
        List<HistoryModel> History(int user_id);
        bool DeleteDetailCheckOut(int maDetail);
    }
}
