using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public partial interface IHDNRepository
    {

        List<HDNModel> AllHDN();
        HDNModel GetDatabyID(int id);
        bool Create(HDNModel model);
        bool Update(HDNModel model);
        List<HDNModel> Search(string keyword);
        bool Delete(int id);




    }
}
