using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using DataModel;
using Microsoft.AspNetCore.Mvc;

namespace API.BanSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HDBController
    {
        private IHDBBusiness _hdbBusiness;
        public HDBController(IHDBBusiness hdbBusiness)
        {
            _hdbBusiness = hdbBusiness;
        }
        [Route("GetAll")]
        [HttpGet]
        public List<HDBModel> AllHDB()
        {
            return _hdbBusiness.AllHDB();
        }
        [Route("create-HDB")]
        [HttpPost]
        public HDBModel CreateItem([FromBody] HDBModel model)
        {
            _hdbBusiness.Create(model);
            return model;
        }
        [Route("update-HDB")]
        [HttpPut]
        public HDBModel Update([FromBody] HDBModel model)
        {
            _hdbBusiness.Update(model);
            return model;
        }
        [Route("search-HDB/{keyword}")]
        [HttpGet]
        public List<HDBModel> Search(string keyword)
        {
            return _hdbBusiness.Search(keyword);
        }
        [Route("delete-HDB/{id}")]
        [HttpGet]
        public bool Delete(int id)
        {
            return  _hdbBusiness.Delete(id);
        }
        [Route("get-by-id/{id}")]
        [HttpGet]
        public HDBModel GetDatabyID(int id)
        {
            return _hdbBusiness.GetDatabyID(id);
        }
    }
}
