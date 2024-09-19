using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public partial interface ICartBusiness
    {
        List<CartModel> GetAllCart(int User_id);
        bool Create(CartModel model);
        bool Delete(int MaCart, int user_id);
    }
}
