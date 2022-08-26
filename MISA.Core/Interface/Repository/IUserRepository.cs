using MISA.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace MISA.Core.Interface.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        /// <summary>
        /// Phương thức khởi tạo chuỗi kết nối 
        /// CreateBy: Dương Minh Việt (19/8/2022)
        /// </summary>
        MySqlConnection GetNewConnect();

        /// <summary>
        /// Phân trang danh sách người dùng
        /// </summary>
        /// <param name="pageNumber"> Số trang truyền vào </param>
        /// <param name="pageSize"> Số bản ghi trên 1 trang </param>
        /// <param name="userFilter"> Từ khoá để tìm kiếm </param>
        /// <param name="roleID"> ID vai trò để lọc dữ liệu </param>
        /// <returns> Danh sách user </returns>
        /// CreateBy: DƯơng Minh Việt(14/8/2022)
        object GetPaging(int pageSize, int pageNumber, string? userFilter = " ", Guid? roleID = null);

        /// <summary>
        /// Thêm mới 1 người dùng 
        /// </summary>
        /// <param name="user"> Thông tin đối tượng cần thêm mới </param>
        /// <returns> Trả về 1 nếu thêm mới thành công </returns>
        /// Author: Dương Minh Việt (7/8/2022)
        int Insert(User user);

        /// <summary>
        /// Thêm mới 1 vai trò vào bảng UserRole
        /// </summary>
        /// <param name="UserID"> ID user cần thêm vai trò </param>
        /// <param name="RoleID"> ID role được thêm mới </param>
        /// <returns>Trả về 1 nếu thêm mới thành công </returns>
        /// Author: Dương Minh Việt (7/8/2022)
        int InsertUserRole(Guid UserID, Guid RoleID);


        /// <summary>
        /// Cập nhật người dùng
        /// </summary>
        /// <param name="user"> Thông tin đối tượng cần cập nhật </param>
        /// <returns> Trả về 1 nếu update thành công </returns>
        /// Author: Dương Minh Việt (7/8/2022)
        int Update(User user);

        /// <summary>
        /// Xoá vai trò người dùng 
        /// </summary>
        /// <param name="UserID"> ID user cần xoá vai trò </param>
        /// <param name="RoleID"> ID role cần xoá </param>
        /// <returns> Trả về 1 - nếu xoá thành công </returns>
        /// Author: Dương Minh Việt (7/8/2022)
        int DeleteUserRole(Guid UserID, Guid RoleID);

        /// <summary>
        /// Kiểm tra trùng mã 
        /// </summary>
        /// <param name="code"> Mã cần kiểm tra </param>
        /// <returns> True - là mã hợp lệ, False - là không hợp lệ</returns>
        /// Author: Dương Minh Việt (9/8/2022)
        bool CheckCode(string code);

        /// <summary>
        /// Kiểm tra xem user đã có vai trò này chưa 
        /// </summary>
        /// <param name="UserID"> ID user cần kiểm tra </param>
        /// <param name="RoleID"> ID role cần kiểm tra </param>
        /// <returns> Trả về true - nếu user chưa có vai trò, ngược lại</returns>
        /// Author: Dương Minh Việt (7/8/2022)
        bool CheckRole(Guid UserID, Guid RoleID);

        /// <summary>
        /// Lưu vai trò cùa người dùng
        /// </summary>
        /// <param name="user"> Đối tượng người dùng cần lưu vai trò</param>
        /// <returns> Trả về số vai trò được thêm mới thành công </returns>
        /// Author: Dương Minh Việt (7/8/2022)
        int SaveUserRole(User user);

        /// <summary>
        /// Lấy ra mã người dùng mới 
        /// </summary>
        /// <returns> Trả về mã mới cho người dùng </returns>
        /// Author: Dương Minh Việt (7/8/2022)
        string newUserCode();
    }
}
