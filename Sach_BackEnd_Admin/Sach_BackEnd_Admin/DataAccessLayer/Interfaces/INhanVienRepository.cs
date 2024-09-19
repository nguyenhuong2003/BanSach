using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public partial interface INhanVienRepository
    {

        List<NhanVienModel> GetAllNhanVien();
        NhanVienModel GetDatabyID(int id);
        bool Create(NhanVienModel model);
        bool Update(NhanVienModel model);
       List<NhanVienModel> Search(string keyword);
        bool Delete(int id);




    }
}
