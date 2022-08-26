using MISA.Core.Interface;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace MISA.Infrastructure.Repository
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>
    {
        #region Constructor
        public BaseRepository()
        {
            _connectionString = "Host=localhost;" +
               "Port=3306;" +
               "Database= misa.dmviet;" +
               "User=root;" +
               "Password=viet0965990497;";
            tableName = typeof(MISAEntity).Name;
        }
        #endregion

        #region Properties
        //Chuỗi kết nối tới CSDL
        protected string _connectionString;
        protected MySqlConnection _sqlConnection;
        //Tên bảng 
        protected string tableName;
        #endregion

               
        #region Method
        /// <summary>
        /// Xoá dữ liệu của đối tượng qua ID
        /// </summary>
        /// <param name="id"> ID đối tượng cần xoá dữ liệu </param>
        /// <returns> Trả về 1 - nếu xoá thành công </returns>
        /// Author: Dương Minh Việt (6/8/2022)
        public int Delete(Guid id)
        {
            using(_sqlConnection = new MySqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                using var transaction = _sqlConnection.BeginTransaction();
                try
                {
                    //Câu truy vấn 
                    var query = $"Delete from {tableName} where {tableName}.{tableName}ID = @{tableName}ID";
                    //Khai báo tham số param cho câu truy vấn
                    var param = new DynamicParameters();
                    param.Add($"@{tableName}ID", id);
                    //Lấy ra kết quả 
                    var res = _sqlConnection.Execute(sql: query,param:param,transaction:transaction);
                    transaction.Commit();
                    //Trả về kết quả
                    return res;
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                }
                finally
                {
                    transaction.Dispose();
                    _sqlConnection.Close();
                }
                return 0;
            }    
        }

        /// <summary>
        /// Lấy ra toàn bộ dữ liệu của đối tượng 
        /// </summary>
        /// <returns> Danh sách thông tin dữ liệu </returns>
        /// Author: Dương Minh Việt (5/8/2022)
        public IEnumerable<MISAEntity> GetAll()
        {
            using (_sqlConnection = new MySqlConnection(_connectionString))
            {
                //Khởi tạo câu truy vấn 
                string query = $"Select * from {tableName}  order by {tableName}.CreateDate DESC";
                //Lấy ra kết quả
                var res = _sqlConnection.Query<MISAEntity>(sql: query);
                //Trả về kết quả
                return res;
            }    
        }

        /// <summary>
        /// Lấy ra thông tin dữ liệu bằng ID
        /// </summary>
        /// <param name="id"> ID đối tượng cần lấy thông tin dữ liệu </param>
        /// <returns> Thông tin đối tượng </returns>
        /// Author: Dương Minh Việt (6/8/2022)
        public MISAEntity GetById(Guid id)
        {
           using(_sqlConnection = new MySqlConnection(_connectionString))
            {
                //Câu truy vấn 
                var query = $"select * from {tableName} where {tableName}ID = @{tableName}ID";
                //Truyên vào tham số
                var param = new DynamicParameters();
                param.Add($"{tableName}ID", id.ToString());
                //Lấy ra kết quả
                var res = _sqlConnection.QueryFirstOrDefault<MISAEntity>(sql: query,param: param);
                //Trả về kết quả
                return res;
            }    
        }

        /// <summary>
        /// Lấy ra thông tin dữ liệu bằng mã Code
        /// </summary>
        /// <param name="code"> Code đối tượng cần lấy thông tin dữ liệu </param>
        /// <returns> Thông tin đối tượng </returns>
        /// Author: Dương Minh Việt (8/8/2022)
        public MISAEntity GetByCode(string code)
        {
            using(_sqlConnection = new MySqlConnection(_connectionString))
            {
                //Câu truy vấn
                var query = $"Select * from {tableName} where {tableName}Code = @{tableName}Code";
                //Truyên vào tham số
                var param = new DynamicParameters();
                param.Add($"{tableName}Code", code);
                //Lấy ra kết quả
                var res = _sqlConnection.QueryFirstOrDefault<MISAEntity>(sql: query,param:param);
                //Trả về kết quả
                return res;
             }
        }
        #endregion
    }
}
