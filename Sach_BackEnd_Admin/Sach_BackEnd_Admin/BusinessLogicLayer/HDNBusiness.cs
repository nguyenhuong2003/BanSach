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
    public class HDNBusiness : IHDNBusiness
    {
        private IHDNRepository _res;
        public HDNBusiness(IHDNRepository res)
        {
            _res = res;
        }
        public List<HDNModel> AllHDN()
        {
            return _res.AllHDN();
        }
        public bool Create(HDNModel model)
        {
            return _res.Create(model);
        }
        public bool Update(HDNModel model)
        {
            return _res.Update(model);
        }
        public List<HDNModel> Search(string keyword)
        {
            return  _res.Search(keyword);
        }
        public bool Delete(int id)
        {
            return  _res.Delete(id);
        }
        public HDNModel GetDatabyID(int id)
        {
            return _res.GetDatabyID(id);
        }
    }
}
