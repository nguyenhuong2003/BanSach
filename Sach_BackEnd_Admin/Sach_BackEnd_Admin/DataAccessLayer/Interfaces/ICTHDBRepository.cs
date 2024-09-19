using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public partial interface ICTHDBRepository
    {
        bool DeleteCheckOut(int MaCheckOut);
       List<CT_HDBModel> AllCT_HDB();
        CT_HDBModel GetDatabyID(int id);
        bool Create(CT_HDBModel model);
        bool Update(CT_HDBModel model);
        List<CT_HDBModel> Search(string keyword);
        bool Delete(int id);

        bool CreateCheckOut(CheckOutModel model);
        List<HistoryModel> History(int user_id);
        List<CheckOutModel1> GetAllCheckOut();
        DetailCheckOutModel GetDetail(int MaCheckout);
        bool DeleteDetailCheckOut(int maDetail);
    }
}
