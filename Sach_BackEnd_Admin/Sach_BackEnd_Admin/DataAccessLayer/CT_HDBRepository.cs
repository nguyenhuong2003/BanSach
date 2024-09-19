using DataAccessLayer.Interfaces;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CT_HDBRepository : ICTHDBRepository
    {
        private IDatabaseHelper _dbHelper;
        public CT_HDBRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public List<CT_HDBModel> AllCT_HDB()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "AllCT_HDB");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<CT_HDBModel>().ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CT_HDBModel> Search(string keyword)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "chitietHDB _search",
                     "@maCTHDB", keyword);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<CT_HDBModel>().ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CT_HDBModel GetDatabyID(int id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "cthdb_get_by_id",
                     "@id", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<CT_HDBModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Create(CT_HDBModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "chitietHDB_create",
                "@hoadonBanID", model.HoaDonbanID,
                "@maSach", model.MaSach,
                "@soLuong", model.SoLuong,
                "@donGia", model.DonGia);
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
        public bool CreateCheckOut(CheckOutModel model)
        {
            string msgError = "";
            try
            {
                var groupMaCart = string.Join(",", model.MaCart);
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_InsertCheckOutList",
                "@MaCartList", groupMaCart,
                "@NameUser", model.NameUser,
                "@Email", model.Email,
                "@SoDienThoai", model.SoDienThoai,
                "@QuocGia", model.QuocGia,
                "@Tinh", model.Tinh,
                "@Quan", model.Quan,
                "@DiaChi", model.DiaChi,
                "@PhuongTienThanhToan", model.PhuongTienThanhToan);
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
        public bool Update(CT_HDBModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "chitietHDB_update",
                 "@maCTHDB", model.MaCTHDB,
                "@hoadonBanID", model.HoaDonbanID,
                "@maSach", model.MaSach,
                "@soLuong", model.SoLuong,
                "@donGia", model.DonGia);
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
                var result = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "chitietHDB_delete", "@maCTHDB", id);
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
        public bool DeleteCheckOut(int MaCheckOut)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_DeleteFromCheckOut", 
                    "@maCheckOut", MaCheckOut);
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
        public bool DeleteDetailCheckOut(int maDetail)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_DeleteDetailCheckOut",
                    "@MaDetail", maDetail);
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
        public List<CheckOutModel1> GetAllCheckOut()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_GetAllCheckOut");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<CheckOutModel1>().ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DetailCheckOutModel GetDetail(int MaCheckOut)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_GetDetailCheckOutAndSachInfo",
                     "@MaCheckOut", MaCheckOut);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<DetailCheckOutModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<HistoryModel> History(int user_id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_history",
                    "@UserId", user_id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<HistoryModel>().ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
