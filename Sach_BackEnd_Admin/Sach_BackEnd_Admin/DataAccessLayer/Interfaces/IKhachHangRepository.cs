using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public partial interface IKhachHangRepository
    {

        List<KhachHangModel> AllKH();
        KhachHangModel GetDatabyID(int id);
        bool Create(KhachHangModel model);
        bool Update(KhachHangModel model);
        List<KhachHangModel> Search(string keyword);
        bool Delete(int id);
        UserModel Register(UserModel model);
        UserModel Login(string email, string pass);
        UserModel GetUserById(int user_id);
    }
}
