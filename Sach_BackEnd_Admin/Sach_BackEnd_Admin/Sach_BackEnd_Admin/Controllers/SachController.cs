using BusinessLogicLayer.Interfaces;
using DataModel;
using Microsoft.AspNetCore.Mvc;

using System.IO;
using System.Text;

namespace API.BanSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SachController: ControllerBase  
    {
        private readonly ISachBusiness _sachBusiness;
        public SachController(ISachBusiness sachBusiness)
        {
            _sachBusiness = sachBusiness;
        }
        [Route("GetAll")]
        [HttpGet]
        public List<SachModel> GetAllLProduct()
        {
            return  _sachBusiness.GetAllSach();
        }
        [Route("create-sach")]
        [HttpPost]
        public SachModel CreateItem([FromBody] SachModel model)
        {
            _sachBusiness.Create(model);
            return model;
        }
        [Route("update-sach")]
        [HttpPut]
        public SachModel Update([FromBody] SachModel model)
        {
            _sachBusiness.Update(model);
            return model;
        }
        [Route("search-sach/{keyword}")]
        [HttpGet]
        public List<SachModel> Search(string keyword)
        {
            return _sachBusiness.Search(keyword);
        }
        [Route("delete-sach/{id}")]
        [HttpDelete]
        public bool Delete(int id)
        {
            return _sachBusiness.Delete(id);
        }
        [Route("get-by-id/{id}")]
        [HttpGet]
        public SachModel GetDatabyID(int id)
        {
            return _sachBusiness.GetDatabyID(id);
        }
        [NonAction]
        public string CreatePathFile(string RelativePathFileName)
        {
            try
            {
                string serverRootPathFolder = @"D:\HUONG\BanSach_ReactJs\ban_sach\public\";
                string fullPathFile = $@"{serverRootPathFolder}\{RelativePathFileName}";
                return fullPathFile;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [Route("GetBookForCategory")]
        [HttpGet]
        public List<SachModel> GetBookForCategory(int maLoai)
        {
            return _sachBusiness.GetBookForCategory(maLoai);
        }
        [Route("upload")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = $"images/{file.FileName}";
                    var fullPath = CreatePathFile(filePath);

                    // Kiểm tra xem file đã tồn tại chưa
                    if (!System.IO.File.Exists(fullPath))
                    {
                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        return Ok(new { filePath });
                    }
                    else
                    {
                        return Ok(new { message = "File already exists." });
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

    }
}
