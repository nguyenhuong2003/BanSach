using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using DataModel;
using Microsoft.AspNetCore.Mvc;

namespace API.BanSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CT_HDNController
    {
        private ICT_HDNBusiness _cthdnBusiness;
        public CT_HDNController(ICT_HDNBusiness cthdnBusiness)
        {
            _cthdnBusiness = cthdnBusiness;
        }
        [Route("GetAll")]
        [HttpGet]
        public List<CT_HDNModel> AllCT_HDN()
        {
            return  _cthdnBusiness.AllCT_HDN();
        }
        [Route("create-CTHDN")]
        [HttpPost]
        public CT_HDNModel CreateItem([FromBody] CT_HDNModel model)
        {
            _cthdnBusiness.Create(model);
            return model;
        }
        [Route("update-CTHDN")]
        [HttpPut]
        public CT_HDNModel Update([FromBody] CT_HDNModel model)
        {
            _cthdnBusiness.Update(model);
            return model;
        }
        [Route("search-CTHDN/{keyword}")]
        [HttpGet]
        public List<CT_HDNModel> Search(string keyword)
        {
            return _cthdnBusiness.Search(keyword);
        }
        [Route("delete-CTHDN/{id}")]
        [HttpGet]
        public bool Delete(int id)
        {
            return  _cthdnBusiness.Delete(id);
        }
        [Route("get-by-id/{id}")]
        [HttpGet]
        public CT_HDNModel GetDatabyID(int id)
        {
            return _cthdnBusiness.GetDatabyID(id);
        }
    }
}
