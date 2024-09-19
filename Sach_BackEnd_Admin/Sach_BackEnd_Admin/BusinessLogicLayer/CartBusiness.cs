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
    public class CartBusiness:ICartBusiness
    {
        private ICartRepository _res;
        public CartBusiness(ICartRepository res)
        {
            _res = res;
        }
        public List<CartModel> GetAllCart(int User_id)
        {
            return _res.GetAllCart(User_id);
        }
        public bool Create(CartModel model)
        {
            return _res.Create(model);
        }
        public bool Delete(int MaCart, int user_id)
        {
            return _res.Delete(MaCart, user_id);
        }
    }
}
