using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using DataModel;
using Microsoft.AspNetCore.Mvc;

namespace API.BanSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CT_HDBController
    {
        private ICT_HDBBusiness _cthdbBusiness;
        public CT_HDBController(ICT_HDBBusiness cthdbBusiness)
        {
            _cthdbBusiness = cthdbBusiness;
        }
        [Route("GetAll")]
        [HttpGet]
        public List<CT_HDBModel> AllCT_HDB()
        {
            return  _cthdbBusiness.AllCT_HDB();
        }
        [Route("create-CTHDB")]
        [HttpPost]
        public CT_HDBModel CreateItem([FromBody] CT_HDBModel model)
        {
            _cthdbBusiness.Create(model);
            return model;
        }
        [Route("update-CTHDB")]
        [HttpPut]
        public CT_HDBModel Update([FromBody] CT_HDBModel model)
        {
            _cthdbBusiness.Update(model);
            return model;
        }
        [Route("search-CTHDB/{keyword}")]
        [HttpGet]
        public List<CT_HDBModel> Search(string keyword)
        {
            return  _cthdbBusiness.Search(keyword);
        }
        [Route("delete-CTHDB")]
        [HttpDelete]
        public bool Delete(int id)
        {
            return _cthdbBusiness.Delete(id);
        }
        [Route("get-by-id/{id}")]
        [HttpGet]
        public CT_HDBModel GetDatabyID(int id)
        {
            return _cthdbBusiness.GetDatabyID(id);
        }
        [Route("create-checkout")]
        [HttpPost]
        public CheckOutModel CreateCheckOut([FromBody] CheckOutModel model)
        {
            _cthdbBusiness.CreateCheckOut(model);
            return model;
        }
        [Route("delete-checkout")]
        [HttpDelete]
        public bool DeleteCheckOut(int maCheckOut)
        {
            return _cthdbBusiness.DeleteCheckOut(maCheckOut);
        }
        [Route("delete-detailcheckout")]
        [HttpDelete]
        public bool DeleteDetailCheckOut(int maDetail)
        {
            return _cthdbBusiness.DeleteDetailCheckOut(maDetail);
        }
        [Route("GetAllCheckOut")]
        [HttpGet]
        public List<CheckOutModel1> GetAllCheckOut()
        {
            return _cthdbBusiness.GetAllCheckOut();
        }
        [Route("History")]
        [HttpGet]
        public List<HistoryModel> History(int user_id)
        {
            return _cthdbBusiness.History(user_id);
        }
        [Route("get-detail")]
        [HttpGet]
        public DetailCheckOutModel GetDetail(int maCheckOut)
        {
            return _cthdbBusiness.GetDetail(maCheckOut);
        }
    }
}
