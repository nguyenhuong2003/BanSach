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
    public class LoaiSachBusiness : ILoaiSachBusiness
    {
        private ILoaiSachRepository _res;
        public LoaiSachBusiness(ILoaiSachRepository res)
        {
            _res = res;
        }
        public List<LoaiSachModel> GetAllLoaiSach()
        {
            return _res.GetAllLoaiSach();
        }
        public bool Create(LoaiSachModel model)
        {
            return _res.Create(model);
        }
        public bool Update(LoaiSachModel model)
        {
            return _res.Update(model);
        }
        public List<LoaiSachModel> Search(string keyword)
        {
            return _res.Search(keyword);
        }
        public bool Delete(int id)
        {
            return  _res.Delete(id);
        }
        public LoaiSachModel GetDatabyID(int id)
        {
            return _res.GetDatabyID(id);
        }
        public List<LoaiSachModel> GetTop8()
        {
            return _res.GetTop8();
        }
    }
}
