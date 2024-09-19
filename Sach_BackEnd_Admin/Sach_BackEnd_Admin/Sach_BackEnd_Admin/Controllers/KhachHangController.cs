using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.BanSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController:ControllerBase
    {
        private IKhachHangBusiness _khBusiness;
        public KhachHangController(IKhachHangBusiness khBusiness)
        {
            _khBusiness = khBusiness;
        }
        [Route("GetAll")]
        [HttpGet]
        public  List<KhachHangModel> AllKH()
        {
            return _khBusiness.AllKH();
        }
        [Route("create-khachhang")]
        [HttpPost]
        public KhachHangModel CreateItem([FromBody] KhachHangModel model)
        {
            _khBusiness.Create(model);
            return model;
        }
        [Route("update-khachhang")]
        [HttpPut]
        public KhachHangModel Update([FromBody] KhachHangModel model)
        {
            _khBusiness.Update(model);
            return model;
        }
        [Route("search-khachhang/{keyword}")]
        [HttpGet]
        public List<KhachHangModel> Search(string keyword)
        {
            return  _khBusiness.Search(keyword);
        }
        [Route("delete-khachhang/{id}")]
        [HttpGet]
        public bool Delete(int id)
        {
            return _khBusiness.Delete(id);
        }
        [Route("get-by-id/{id}")]
        [HttpGet]
        public KhachHangModel GetDatabyID(int id)
        {
            return _khBusiness.GetDatabyID(id);
        }
        [Route("register")]
        [HttpPost]
        public UserModel Register([FromBody] UserModel model)
        {
            _khBusiness.Register(model);
            return model;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            var user = _khBusiness.Login(model.email, model.pass);
            if (user == null)
                return BadRequest(new { message = "Tài khoản hoặc mật khẩu không đúng!" });

            // Trả về tất cả các thông tin của người dùng từ UserModel
            return Ok(new
            {
                User_id = user.user_id,
                Username = user.username,
                Email = user.email
            });
        }
        [Route("get_user_by_id")]
        [HttpGet]
        public UserModel GetUserById(int user_id)
        {
            return _khBusiness.GetUserById(user_id);
        }
    }
}
