using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public partial interface InccRepository
    {

       List<nccModel> AllNCC();
        nccModel GetDatabyID(int id);
        bool Create(nccModel model);
        bool Update(nccModel model);
        List<nccModel> Search(string keyword);
        bool Delete(int id);




    }
}
