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
    public class NhanVienBusiness : INhanVienBusiness
    {
        private INhanVienRepository _res;
        public NhanVienBusiness(INhanVienRepository res)
        {
            _res = res;
        }
        public List<NhanVienModel> GetAllNhanVien()
        {
            return  _res.GetAllNhanVien();
        }
        public bool Create(NhanVienModel model)
        {
            return _res.Create(model);
        }
        public bool Update(NhanVienModel model)
        {
            return _res.Update(model);
        }
        public List<NhanVienModel> Search(string keyword)
        {
            return _res.Search(keyword);
        }
        public bool Delete(int id)
        {
            return _res.Delete(id);
        }
        public NhanVienModel GetDatabyID(int id)
        {
            return _res.GetDatabyID(id);
        }

    }
}
