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
    public class NhaXuatBanBusiness : INhaXuatBanBusiness
    {
        private INhaXuatBanRepository _res;
        public NhaXuatBanBusiness(INhaXuatBanRepository res)
        {
            _res = res;
        }
        public List<NhaXuatBanModel> GetAllNhaXuatBan()
        {
            return _res.GetAllNhaXuatBan();
        }
        public bool Create(NhaXuatBanModel model)
        {
            return _res.Create(model);
        }
        public bool Update(NhaXuatBanModel model)
        {
            return _res.Update(model);
        }
        public List<NhaXuatBanModel> Search(string keyword)
        {
            return _res.Search(keyword);
        }
        public bool Delete(int id)
        {
            return _res.Delete(id);
        }
        public NhaXuatBanModel GetDatabyID(int id)
        {
            return _res.GetDatabyID(id);
        }



    }
}
