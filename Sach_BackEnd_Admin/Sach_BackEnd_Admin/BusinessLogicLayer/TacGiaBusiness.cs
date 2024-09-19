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
    public class TacGiaBusiness : ITacGiaBusiness
    {
        private ITacGiaRepository _res;
        public TacGiaBusiness(ITacGiaRepository res)
        {
            _res = res;
        }
        public List<TacGiaModel> GetAllTacGia()
        {
            return _res.GetAllTacGia();
        }
        public bool Create(TacGiaModel model)
        {
            return _res.Create(model);
        }
        public bool Update(TacGiaModel model)
        {
            return _res.Update(model);
        }
        public List<TacGiaModel> Search(string keyword)
        {
            return _res.Search(keyword);
        }
        public bool Delete(int id)
        {
            return _res.Delete(id);
        }
        public TacGiaModel GetDatabyID(int id)
        {
            return _res.GetDatabyID(id);
        }



    }
}
