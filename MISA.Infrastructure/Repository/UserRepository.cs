using Dapper;
using MISA.Core.Interface.Repository;
using MISA.Core.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        /// <summary>
        /// Thêm mới 1 người dùng 
        /// </summary>
        /// <param name="user"> Thông tin người dùng cần thêm mới </param>
        /// <param name="roles"> Danh sách vai trò công việc của người dùng</param>
        /// <returns> Trả về 1 - nếu thêm mới thành công </returns>
        /// Author: DƯơng Minh Việt (7/8/2022)
        public int Insert(User user)
        {
            using (_sqlConnection = new MySqlConnector.MySqlConnection(_connectionString))
            {
                var query = "Proc_InsertUser";
                //Định nghĩa lại đối tượng người dùng cần thêm mới 
                var paramObject = new
                {
                    UserCode = user.UserCode,
                    UserName = user.UserName,
                    DepartmentID = user.DepartmentID,
                    PositionID = user.PositionID,
                    Email = user.Email,
                    UserStatus = user.UserStatus,
                    RoleName = user.RoleName,
                };
                //Thực hiện thêm mới vào bảng User
                var res = _sqlConnection.Execute(sql: query, param: paramObject, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
        }
        /// <summary>
        /// lưu vai trò khi thêm mới user
        /// </summary>
        /// <param name="user"> Dối tượng user cần thêm mới </param>
        /// <returns>Trả về số dòng thêm mới vào bảng UserRole thành công</returns>
        /// Author: Dương Minh Việt (8/8/2022)
        public int SaveUserRole(User user)
        {
            using (var userRoleConnect = new MySqlConnector.MySqlConnection(_connectionString))
            {
                var d = 0;
                //lấy ra đối tượng user vừa thêm mới thành công 
                var newUser = GetByCode(user.UserCode);
                //Duyệt danh sách vai trò
                foreach (var role in user.Roles)
                {
                    //Gọi thủ tục thêm mới vào bảng UserRole
                    var res = InsertUserRole(newUser.UserID, role.RoleID);
                    if (res > 0)
                    {
                        d++;
                    }
                }
                return d;
            }
        }
        /// <summary>
        /// Thêm vai trò của user
        /// </summary>
        /// <param name="query"> Chuỗi truy vấn </param>
        /// <param name="RoleID"> ID Role cần cập nhật </param>
        /// <param name="UserID"> ID User cần cập nhật </param>
        /// <returns> Trả về 1 - nếu thêm / sửa thành công </returns>
        /// Author: Dương Minh Việt (8/8/2022)
        public int InsertUserRole(Guid UserID, Guid RoleID)
        {
            using (_sqlConnection = new MySqlConnector.MySqlConnection(_connectionString))
            {
                var query = "Proc_InsertUserRole";
                var param = new
                {
                    UserID = UserID,
                    RoleID = RoleID,
                };
                var res = _sqlConnection.Execute(sql: query, param: param, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
        }

        /// <summary>
        /// Xoá vai trò của user
        /// </summary>
        /// <param name="UserID"> ID user cần xoá vai trò </param>
        /// <param name="RoleID"> ID role cần xoá </param>
        /// <returns> Trả về 1 - nếu xoá thành công </returns>
        public int DeleteUserRole(Guid UserID, Guid RoleID)
        {
            using (_sqlConnection = new MySqlConnector.MySqlConnection(_connectionString))
            {
                //Xoá khỏi bảng UserRole
                var sqlQuery = "DELETE FROM userrole WHERE UserID = @UserID AND RoleID = @RoleID";
                var param = new DynamicParameters();
                param.Add("UserID", UserID);
                param.Add("RoleID", RoleID);
                var res = _sqlConnection.Execute(sql: sqlQuery, param: param);
                return res;
            }
        }

        /// <summary>
        /// Cập nhật vai trò của user
        /// </summary>
        /// <param name="user"> đối tượng user cần cập nhật </param>
        /// <returns> Trả về 1 nếu cập nhật thành công </returns>
        /// Author: Dương Minh Việt (8/8/2022)
        public int Update(User user)
        {
            using (_sqlConnection = new MySqlConnector.MySqlConnection(_connectionString))
            {
                //Định nghĩa lại đối tượng user cân cập nhật 
                var param = new
                {
                    UserID = user.UserID,
                    UserCode = user.UserCode,
                    UserName = user.UserName,
                    DepartmentID = user.DepartmentID,
                    PositionID = user.PositionID,
                    Email = user.Email,
                    UserStatus = user.UserStatus,
                    RoleName = user.RoleName,
                };
                //Câu truy vấn
                var query = "Proc_UpdateUser";
                //Gọi thủ tục cập nhật User
                var res = _sqlConnection.Execute(sql: query, param: param, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
        }

        /// <summary>
        /// Kiểm tra xem mã có trùng hay không 
        /// </summary>
        /// <param name="code"> Mã cần kiểm tra </param>
        /// <returns> True - nếu mã hợp lệ, false - nếu mã không hợp lệ </returns>
        public bool CheckCode(string code)
        {
            using (_sqlConnection = new MySqlConnector.MySqlConnection(_connectionString))
            {
                //Câu truy vấn
                var query = "select * from user where UserCode = @Code";
                //Khai bao param cho câu truy vẫn
                var param = new DynamicParameters();
                param.Add("@Code", code);
                var res = _sqlConnection.QueryFirstOrDefault(sql: query, param: param);
                if (res == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Kiểm tra xem người dùng đã có vai trò hay chưa 
        /// </summary>
        /// <param name="UserID">ID user cần kiểm tra </param>
        /// <param name="RoleID">ID role cần kiểm tra </param>
        /// <returns>Trả về true - nếu user chưa có vai trò, ngược lại</returns>
        public bool CheckRole(Guid UserID, Guid RoleID)
        {
            using (_sqlConnection = new MySqlConnector.MySqlConnection(_connectionString))
            {
                //Câu truy vấn z
                var query = "Select * from userrole where UserID = @UserID and RoleID = @RoleID";
                //Khai báo param cho câu truy vấn 
                var param = new DynamicParameters();
                param.Add("@UserID", UserID);
                param.Add("@RoleID", RoleID);
                var res = _sqlConnection.QueryFirstOrDefault(sql: query, param: param);
                if (res == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        /// <summary>
        /// phân trang người dùng
        /// </summary>
        /// <param name="pageSize"> Số bản ghi 1 trang  </param> 
        /// <param name="pageNumber"> Số trang </param>
        /// <param name="userFilter"> Thông tin tìm kiếm </param>
        /// <returns> Danh sách thông tin người dùng </returns>
        /// CreateBy: Dương Minh Việt (14/8/2022)
        public object GetPaging(int pageSize, int pageNumber, string? userFilter = " ", Guid? roleID=null)
        {
            using (_sqlConnection = new MySqlConnector.MySqlConnection(_connectionString))
            {
                var sqlQuery = "Proc_GetFilterUser";
                var dynamicParam = new DynamicParameters();
                dynamicParam.Add("@TotalPage", direction: System.Data.ParameterDirection.Output);
                dynamicParam.Add("@TotalRecord", direction: System.Data.ParameterDirection.Output);
                dynamicParam.Add("@PageStart", direction: System.Data.ParameterDirection.Output);
                dynamicParam.Add("@PageEnd", direction: System.Data.ParameterDirection.Output);
                dynamicParam.Add("@pageSize", pageSize);
                dynamicParam.Add("@pageNumber", pageNumber);
                dynamicParam.Add("@keyword", userFilter);
                dynamicParam.Add("@RoleID", roleID);
                var res = _sqlConnection.Query<User>(sql: sqlQuery, param: dynamicParam, commandType: System.Data.CommandType.StoredProcedure);
                int TotalPage = dynamicParam.Get<int>("@TotalPage");
                int TotalRecord = dynamicParam.Get<int>("@TotalRecord");
                int PageStart = dynamicParam.Get<int>("@PageStart");
                int PageEnd = dynamicParam.Get<int>("@PageEnd");
                return new
                {
                    TotalPage = TotalPage,
                    TotalRecord = TotalRecord,
                    PageStart = PageStart,
                    PageEnd = PageEnd,
                    Data = res,
                };
            }
        }

        /// <summary>
        /// Lấy ra mã người dùng mới 
        /// </summary>
        /// <returns>Trả về mã người dùng mới </returns>
        public string newUserCode()
        {
            _sqlConnection = new MySqlConnector.MySqlConnection(_connectionString);
            _sqlConnection.Open();
            using var trans = _sqlConnection.BeginTransaction();
            try
            {
                var UserCode = "";
                //câu truy vấn 
                var query = "Proc_GetNewUserCode";
                var dynamicParam = new DynamicParameters();
                dynamicParam.Add("@UserCode", UserCode);
                //lấy ra kết quả
                var res = _sqlConnection.QueryFirstOrDefault<string>(sql: query,param: dynamicParam, commandType: System.Data.CommandType.StoredProcedure, transaction:trans);
                //trả về dữ liệu
                return res;
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
            }
            finally
            {
                trans.Dispose();
                _sqlConnection.Close();
            }
            return "";
        }

        /// <summary>
        /// Phương thức khởi tạo kết nối đến database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public MySqlConnection GetNewConnect()
        {
            return new MySqlConnection(_connectionString);
        }
    }

}
