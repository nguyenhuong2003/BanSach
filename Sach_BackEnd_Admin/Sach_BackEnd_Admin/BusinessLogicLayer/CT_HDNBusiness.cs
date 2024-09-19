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
    public class CT_HDNBusiness : ICT_HDNBusiness
    {
        private ICTHDNRepository _res;
        public CT_HDNBusiness(ICTHDNRepository res)
        {
            _res = res;
        }
        public List<CT_HDNModel> AllCT_HDN()
        {
            return _res.AllCT_HDN();
        }
        public bool Create(CT_HDNModel model)
        {
            return _res.Create(model);
        }
        public bool Update(CT_HDNModel model)
        {
            return _res.Update(model);
        }
        public List<CT_HDNModel> Search(string keyword)
        {
            return _res.Search(keyword);
        }
        public bool Delete(int id)
        {
            return _res.Delete(id);
        }
        public CT_HDNModel GetDatabyID(int id)
        {
            return _res.GetDatabyID(id);
        }
    }
}
