using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using DataModel;
using Microsoft.AspNetCore.Mvc;

namespace API.BanSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HDNController
    {
        private IHDNBusiness _hdnBusiness;
        public HDNController(IHDNBusiness hdnBusiness)
        {
            _hdnBusiness = hdnBusiness;
        }
        [Route("GetAll")]
        [HttpGet]
        public List<HDNModel> AllHDN()
        {
            return _hdnBusiness.AllHDN();
        }
        [Route("create-HDN")]
        [HttpPost]
        public HDNModel CreateItem([FromBody] HDNModel model)
        {
            _hdnBusiness.Create(model);
            return model;
        }
        [Route("update-HDN")]
        [HttpPut]
        public HDNModel Update([FromBody] HDNModel model)
        {
            _hdnBusiness.Update(model);
            return model;
        }
        [Route("search-HDN/{keyword}")]
        [HttpGet]
        public List<HDNModel> Search(string keyword)
        {
            return  _hdnBusiness.Search(keyword);
        }
        [Route("delete-HDN/{id}")]
        [HttpGet]
        public bool Delete(int id)
        {
            return _hdnBusiness.Delete(id);
        }
        [Route("get-by-id/{id}")]
        [HttpGet]
        public HDNModel GetDatabyID(int id)
        {
            return _hdnBusiness.GetDatabyID(id);
        }
    }
}
