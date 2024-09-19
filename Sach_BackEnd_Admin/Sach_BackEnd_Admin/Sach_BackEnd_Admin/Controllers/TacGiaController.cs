using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using DataModel;
using Microsoft.AspNetCore.Mvc;
namespace API.Bantacgia.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class TacGiaController
    {
        private ITacGiaBusiness _TacGiaBusiness;
        public TacGiaController(ITacGiaBusiness TacGiaBusiness)
        {
            _TacGiaBusiness = TacGiaBusiness;
        }
        [Route("GetAll")]
        [HttpGet]
        public List<TacGiaModel> GetAllLtg()
        {
            return _TacGiaBusiness.GetAllTacGia();
        }
        [Route("create-tacgia")]
        [HttpPost]
        public TacGiaModel CreateItem([FromBody] TacGiaModel model)
        {
            _TacGiaBusiness.Create(model);
            return model;
        }
        [Route("update-tacgia")]
        [HttpPut]
        public TacGiaModel Update([FromBody] TacGiaModel model)
        {
            _TacGiaBusiness.Update(model);
            return model;
        }
        [Route("search-tacgia/{keyword}")]
        [HttpGet]
        public List<TacGiaModel> Search(string keyword)
        {
            return _TacGiaBusiness.Search(keyword);
        }
        [Route("delete-tacgia/{id}")]
        [HttpDelete]
        public bool Delete(int id)
        {
            return _TacGiaBusiness.Delete(id);
        }
        [Route("get-by-id/{id}")]
        [HttpGet]
        public TacGiaModel GetDatabyID(int id)
        {
            return _TacGiaBusiness.GetDatabyID(id);
        }
       
       

       
    }

}

