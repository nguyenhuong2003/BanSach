using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using DataModel;
using Microsoft.AspNetCore.Mvc;
namespace API.BanSach.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class NhaXuatBanController
    {
        private INhaXuatBanBusiness _NhaXuatBanBusiness;
        public NhaXuatBanController(INhaXuatBanBusiness NhaXuatBanBusiness)
        {
            _NhaXuatBanBusiness = NhaXuatBanBusiness;
        }
        [Route("GetAll")]
        [HttpGet]
        public List<NhaXuatBanModel> GetAllLnxb()
        {
            return _NhaXuatBanBusiness.GetAllNhaXuatBan();
        }
        [Route("create-nhaxuatban")]
        [HttpPost]
        public NhaXuatBanModel CreateItem([FromBody] NhaXuatBanModel model)
        {
            _NhaXuatBanBusiness.Create(model);
            return model;
        }
        [Route("update-nhaxuatban")]
        [HttpPut]
        public NhaXuatBanModel Update([FromBody] NhaXuatBanModel model)
        {
            _NhaXuatBanBusiness.Update(model);
            return model;
        }
        [Route("search-nhaxuatban/{keyword}")]
        [HttpGet]
        public List<NhaXuatBanModel> Search(string keyword)
        {
            return _NhaXuatBanBusiness.Search(keyword);
        }
        [Route("delete-nhaxuatban/{id}")]
        [HttpDelete]
        public bool Delete(int id)
        {
            return _NhaXuatBanBusiness.Delete(id);
        }
        [Route("get-by-id/{id}")]
        [HttpGet]
        public NhaXuatBanModel GetDatabyID(int id)
        {
            return _NhaXuatBanBusiness.GetDatabyID(id);
        }


    }
}
