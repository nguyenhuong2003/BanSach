using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using DataModel;
using Microsoft.AspNetCore.Mvc;

namespace API.BanSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController
    {
        private INhanVienBusiness _nhanvienBusiness;
        public NhanVienController(INhanVienBusiness nhanvienBusiness)
        {
            _nhanvienBusiness = nhanvienBusiness;
        }
        [Route("GetAll")]
        [HttpGet]
        public List<NhanVienModel> GetAllNhanVien()
        {
            return  _nhanvienBusiness.GetAllNhanVien();
        }
        [Route("create-nhanvien")]
        [HttpPost]
        public NhanVienModel CreateItem([FromBody] NhanVienModel model)
        {
            _nhanvienBusiness.Create(model);
            return model;
        }
        [Route("update-nhanvien")]
        [HttpPut]
        public NhanVienModel Update([FromBody] NhanVienModel model)
        {
            _nhanvienBusiness.Update(model);
            return model;
        }
        [Route("search-nhanvien/{keyword}")]
        [HttpGet]
        public List<NhanVienModel> Search(string keyword)
        {
            return  _nhanvienBusiness.Search(keyword);
        }
        [Route("delete-nhanvien/{id}")]
        [HttpGet]
        public bool Delete(int id)
        {
            return _nhanvienBusiness.Delete(id);
        }
        [Route("get-by-id/{id}")]
        [HttpGet]
        public NhanVienModel GetDatabyID(int id)
        {
            return _nhanvienBusiness.GetDatabyID(id);
        }
    }
}
