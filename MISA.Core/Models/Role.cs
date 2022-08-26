using MISA.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Models
{
    public class Role:BaseEntity
    {
        #region Constructor
        public Role()
        {
            RoleID = new Guid();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Khoá chính
        /// </summary>
        public Guid RoleID { get; set; }
        /// <summary>
        /// Chi tiết vai trò
        /// </summary>
        public string RoleDescription { get; set; }
        /// <summary>
        /// Trạng thái đối với vai trò 
        /// </summary>
        public RoleState RoleState { get; set; } = RoleState.Nothing;
        #endregion
    }
}
