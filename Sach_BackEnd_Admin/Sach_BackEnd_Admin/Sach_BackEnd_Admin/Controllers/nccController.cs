using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using DataModel;
using Microsoft.AspNetCore.Mvc;

namespace API.BanSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class nccController
    {
        private InccBusiness _nccBusiness;
        public nccController(InccBusiness nccBusiness)
        {
            _nccBusiness = nccBusiness;
        }
        [Route("GetAll")]
        [HttpGet]
        public List<nccModel> AllNCC()
        {
            return _nccBusiness.AllNCC();
        }
        [Route("create-nhacc")]
        [HttpPost]
        public nccModel CreateItem([FromBody] nccModel model)
        {
            _nccBusiness.Create(model);
            return model;
        }
        [Route("update-nhacc")]
        [HttpPut]
        public nccModel Update([FromBody] nccModel model)
        {
            _nccBusiness.Update(model);
            return model;
        }
        [Route("search-nhacc/{keyword}")]
        [HttpGet]
        public List<nccModel> Search(string keyword)
        {
            return  _nccBusiness.Search(keyword);
        }
        [Route("delete-nhacc/{id}")]
        [HttpGet]
        public bool Delete(int id)
        {
            return _nccBusiness.Delete(id);
        }
        [Route("get-by-id/{id}")]
        [HttpGet]
        public nccModel GetDatabyID(int id)
        {
            return _nccBusiness.GetDatabyID(id);
        }
    }
}
