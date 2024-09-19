using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public partial interface ICartRepository
    {

        List<CartModel> GetAllCart(int User_id);
        bool Create(CartModel model);

        bool Delete(int MaCart, int user_id);
    }
}
