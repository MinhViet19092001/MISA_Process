using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enum
{
    /// <summary>
    /// Trang thái thao tác với vai trò
    /// </summary>
    public enum RoleState
    {
        /// <summary>
        /// Không làm gì cả
        /// </summary>
        Nothing= 0,

        /// <summary>
        /// Thêm vai trò
        /// </summary>
        Insert = 1,

        /// <summary>
        /// Xoá vai trò
        /// </summary>
        Remove = 2, 
    }
}
