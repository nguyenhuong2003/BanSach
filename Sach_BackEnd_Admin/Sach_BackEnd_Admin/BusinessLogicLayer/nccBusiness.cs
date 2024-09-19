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
    public class nccBusiness : InccBusiness
    {
        private InccRepository _res;
        public nccBusiness(InccRepository res)
        {
            _res = res;
        }
        public List<nccModel> AllNCC()
        {
            return _res.AllNCC();
        }
        public bool Create(nccModel model)
        {
            return _res.Create(model);
        }
        public bool Update(nccModel model)
        {
            return _res.Update(model);
        }
        public List<nccModel> Search(string keyword)
        {
            return _res.Search(keyword);
        }
        public bool Delete(int id)
        {
            return  _res.Delete(id);
        }
        public nccModel GetDatabyID(int id)
        {
            return _res.GetDatabyID(id);
        }
    }
}
