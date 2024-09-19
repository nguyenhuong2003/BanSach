using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class CT_HDBBusiness : ICT_HDBBusiness
    {
        private ICTHDBRepository _res;
        public CT_HDBBusiness(ICTHDBRepository res)
        {
            _res = res;
        }
        public List<CT_HDBModel> AllCT_HDB()
        {
            return _res.AllCT_HDB();
        }
        public bool Create(CT_HDBModel model)
        {
            return _res.Create(model);
        }
        public bool Update(CT_HDBModel model)
        {
            return _res.Update(model);
        }
        public List<CT_HDBModel> Search(string keyword)
        {
            return _res.Search(keyword);
        }
        public bool Delete(int id)
        {
            return _res.Delete(id);
        }
        public CT_HDBModel GetDatabyID(int id)
        {
            return _res.GetDatabyID(id);
        }
        public bool DeleteCheckOut(int MaCheckOut)
        {
            return _res.DeleteCheckOut(MaCheckOut);
        }
        public bool DeleteDetailCheckOut(int maDetail)
        {
            return _res.DeleteDetailCheckOut(maDetail);
        }
        public bool CreateCheckOut(CheckOutModel model)
        {
            return _res.CreateCheckOut(model);
        }
        public List<CheckOutModel1> GetAllCheckOut()
        {
            return _res.GetAllCheckOut();
        }
        public DetailCheckOutModel GetDetail(int MaCheckOut)
        {
            return _res.GetDetail(MaCheckOut);
        }
        public List<HistoryModel> History(int user_id)
        {
            return _res.History(user_id);
        }
    }
}
