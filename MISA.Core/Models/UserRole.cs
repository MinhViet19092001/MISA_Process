using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Models
{
    public class UserRole:BaseEntity
    {
        #region Properties
        /// <summary>
        /// ID người dùng
        /// </summary>
        public Guid UserID { get; set; }
        /// <summary>
        /// ID vai trò
        /// </summary>
        public Guid RoleID { get; set; }

        #endregion
    }
}
