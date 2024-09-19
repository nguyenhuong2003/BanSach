using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using DataModel;
using Microsoft.AspNetCore.Mvc;

namespace API.BanSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiSachController
    {
        private ILoaiSachBusiness _loaisachBusiness;
        public LoaiSachController(ILoaiSachBusiness loaisachBusiness)
        {
            _loaisachBusiness = loaisachBusiness;
        }
        [Route("GetAll")]
        [HttpGet]
        public List<LoaiSachModel> GetAllLoaiSach()
        {
            return _loaisachBusiness.GetAllLoaiSach();
        }
        [Route("create-loaisach")]
        [HttpPost]
        public LoaiSachModel CreateItem([FromBody] LoaiSachModel model)
        {
            _loaisachBusiness.Create(model);
            return model;
        }
        [Route("update-loaisach")]
        [HttpPut]
        public LoaiSachModel Update([FromBody] LoaiSachModel model)
        {
            _loaisachBusiness.Update(model);
            return model;
        }
        [Route("search-loaisach/{keyword}")]
        [HttpGet]
        public List<LoaiSachModel> Search(string keyword)
        {
            return  _loaisachBusiness.Search(keyword);
        }
        [Route("delete-loaisach/{id}")]
        [HttpDelete]
        public bool Delete(int id)
        {
            return  _loaisachBusiness.Delete(id);
        }
        [Route("get-by-id/{id}")]
        [HttpGet]
        public LoaiSachModel GetDatabyID(int id)
        {
            return _loaisachBusiness.GetDatabyID(id);
        }
        [Route("GetTop8")]
        [HttpGet]
        public List<LoaiSachModel> GetTop8()
        {
            return _loaisachBusiness.GetTop8();
        }
    }
}
