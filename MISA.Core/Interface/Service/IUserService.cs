using MISA.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interface.Service
{
    public interface IUserService
    {
        /// <summary>
        /// Xử lí khi thêm mới 1 đối tượng user
        /// </summary>
        /// <param name="user"> đối tượng cần thêm mới </param>
        /// <returns> Trả về 1 nếu thêm mới thành công </returns>
        /// Author: Dương Minh Việt(9/8/2022)
        int Insert(User user);

        /// <summary>
        /// Xử lí khi cập nhật vai trò của User
        /// </summary>
        /// <param name="user"> đối tượng cần cập nhật </param>
        /// <returns></returns>
        /// Author: DƯơng Minh Việt (9/8/2022)
        int UpdateUserRole(User user);
    }
}
