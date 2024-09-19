using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public partial interface INhanVienBusiness

    {
        List<NhanVienModel> GetAllNhanVien();
        NhanVienModel GetDatabyID(int id);

        bool Create(NhanVienModel model);
        bool Update(NhanVienModel model);
        List<NhanVienModel> Search(string keyword);
        bool Delete(int id);



    }
}
