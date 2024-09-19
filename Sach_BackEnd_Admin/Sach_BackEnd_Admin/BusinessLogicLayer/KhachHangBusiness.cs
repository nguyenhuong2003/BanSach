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
    public class KhachHangBusiness : IKhachHangBusiness
    {
        private IKhachHangRepository _res;
        public KhachHangBusiness(IKhachHangRepository res)
        {
            _res = res;
        }
        public List<KhachHangModel> AllKH()
        {
            return  _res.AllKH();
        }
        public bool Create(KhachHangModel model)
        {
            return _res.Create(model);
        }
        public bool Update(KhachHangModel model)
        {
            return _res.Update(model);
        }
        public List<KhachHangModel> Search(string keyword)
        {
            return _res.Search(keyword);
        }
        public bool Delete(int id)
        {
            return  _res.Delete(id);
        }
        public KhachHangModel GetDatabyID(int id)
        {
            return _res.GetDatabyID(id);
        }
        public UserModel GetUserById(int user_id)
        {
            return _res.GetUserById(user_id);
        }
        public UserModel Register(UserModel model)
        {
            return _res.Register(model);
        }
        public UserModel Login(string email, string pass)
        {
            return _res.Login(email, pass);
        }
    }
}
