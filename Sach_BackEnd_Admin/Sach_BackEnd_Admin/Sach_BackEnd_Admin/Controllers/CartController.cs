using BusinessLogicLayer.Interfaces;
using DataModel;
using Microsoft.AspNetCore.Mvc;

namespace API.BanSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBusiness _cartBusiness;

        public CartController(ICartBusiness cartBusiness)
        {
            _cartBusiness = cartBusiness;
        }

        [Route("GetAllCart")]
        [HttpGet]
        public List<CartModel> GetAllCart(int User_id)
        {
            return _cartBusiness.GetAllCart(User_id);
        }

        [Route("create-cart")]
        [HttpPost]
        public IActionResult CreateItem([FromBody] CartModel model)
        {
            try
            {
                _cartBusiness.Create(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [Route("delete-cart")]
        [HttpDelete]
        public bool Delete([FromQuery] int MaCart, [FromQuery] int user_id) 
        {
            return _cartBusiness.Delete(MaCart, user_id);
        }

    }
}
