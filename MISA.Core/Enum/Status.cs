using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enum
{
    /// <summary>
    /// Trạng thái người dùng
    /// </summary>
    public enum Status
    {

        #region Properties

        /// <summary>
        /// Chưa kích hoạt
        /// </summary>
        NotActive = 0,


        /// <summary>
        /// Chờ xác nhận
        /// </summary>
        Wait = 1,

        /// <summary>
        /// Đang hoạt động
        /// </summary>
        Active = 2,

        /// <summary>
        /// Ngừng kích hoạt 
        /// </summary>
        Inactive = 3,
        #endregion
    }
}
