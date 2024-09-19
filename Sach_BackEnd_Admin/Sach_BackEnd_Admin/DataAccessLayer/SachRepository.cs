using DataAccessLayer.Interfaces;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SachRepository : ISachRepository
    {
        private IDatabaseHelper _dbHelper;
        public SachRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public List<SachModel> GetAllSach()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "LayDanhSachSach");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<SachModel>().ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SachModel GetDatabyID(int id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sach_get_by_id",
                     "@id", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<SachModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SachModel>  Search(string keyword)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sach_search",
                     "@keyword", keyword);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<SachModel>().ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Create(SachModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sach_insert",
                "@maLoai", model.MaLoai,
                "@tenSach", model.TenSach,
                 "@hinhanh", model.HinhAnh,
                 "@slTon", model.slTon,
                 "@Gia",model.Gia,
                 "@MoTa",model.MoTa,
                 "@MaNhaXuatBan",model.MaNhaXuatBan,
                 "@MaTacGia",model.MaTacGia);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(SachModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sach_update",
                 "@maLoai", model.MaLoai,
                 "@maSach", model.MaSach,
                "@hinhanh", model.HinhAnh,
                 "@Gia", model.Gia,
                 "@MoTa", model.MoTa,
                 "@MaNhaXuatBan", model.MaNhaXuatBan,
                 "@MaTacGia", model.MaTacGia,
                "@tenSach", model.TenSach,
                "@slTon", model.slTon);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(int id)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sach_delete", "@maSach", id);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SachModel> GetBookForCategory(int MaLoai)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "GetBooksByCategory",
                     "@MaLoai", MaLoai);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<SachModel>().ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
