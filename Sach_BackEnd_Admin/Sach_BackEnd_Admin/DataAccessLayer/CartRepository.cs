using DataAccessLayer.Interfaces;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CartRepository : ICartRepository
    {
        private IDatabaseHelper _dbHelper;
        public CartRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public List<CartModel> GetAllCart(int User_id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_GetCartDetails",
                    "@UserId", User_id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<CartModel>().ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Create(CartModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_AddToCart",
                "@MaSach", model.MaSach,
                "@UserId", model.User_id,
                "@SoLuong", model.SoLuong);

                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception("Stored Procedure Error: " + msgError);
                }

                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    throw new Exception("Stored Procedure Result Error: " + result.ToString());
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception here if necessary
                throw;
            }
        }
        public bool Delete(int MaCart, int user_id)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_DeleteCartRecord",
                    "@MaCart", MaCart,
                    "@User_id", user_id);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
