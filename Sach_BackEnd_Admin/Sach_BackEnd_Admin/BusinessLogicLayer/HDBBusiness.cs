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
    public class HDBBusiness : IHDBBusiness
    {
        private IHDBRepository _res;
        public HDBBusiness(IHDBRepository res)
        {
            _res = res;
        }
        public List<HDBModel> AllHDB()
        {
            return  _res.AllHDB();
        }
        public bool Create(HDBModel model)
        {
            return _res.Create(model);
        }
        public bool Update(HDBModel model)
        {
            return _res.Update(model);
        }
        public List<HDBModel> Search(string keyword)
        {
            return  _res.Search(keyword);
        }
        public bool Delete(int id)
        {
            return  _res.Delete(id);
        }
        public HDBModel GetDatabyID(int id)
        {
            return _res.GetDatabyID(id);
        }

    }
}
