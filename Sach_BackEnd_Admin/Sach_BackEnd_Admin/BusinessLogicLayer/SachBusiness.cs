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
    public class SachBusiness : ISachBusiness
    {
        private ISachRepository _res;
        public SachBusiness(ISachRepository res)
        {
            _res = res;
        }
        public List<SachModel> GetAllSach()
        {
            return _res.GetAllSach();
        }
        public bool Create(SachModel model)
        {
            return _res.Create(model);
        }
        public bool Update(SachModel model)
        {
            return _res.Update(model);
        }
        public List<SachModel> Search(string keyword)
        {
            return  _res.Search(keyword);
        }
        public bool Delete(int id)
        {
            return  _res.Delete(id);
        }
        public SachModel GetDatabyID(int id)
        {
            return _res.GetDatabyID(id);
        }
        public List<SachModel> GetBookForCategory(int MaLoai)
        {
            return _res.GetBookForCategory(MaLoai);
        }
    }
}
