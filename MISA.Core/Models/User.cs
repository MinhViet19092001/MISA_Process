using MISA.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Models
{
    public class User : BaseEntity
    {

        #region Contructor
        public User()
        {
            UserID = new Guid();
            STT = 0;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Khoá chính
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// Mã người dùng
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// Tên người dùng
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// ID phòng ban
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// ID vị trí
        /// </summary>
        public Guid PositionID { get; set; }

        /// <summary>
        /// Tên vị trí công việc
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Vai trò
        /// </summary>
        public string? RoleName { get; set; }

        /// <summary>
        /// Số thứ tự từng dòng
        /// </summary>
        public int STT { get; set; }

        /// <summary>
        /// Trạng thái người dùng
        /// </summary>
        public Status UserStatus { get; set; }

        /// <summary>
        /// Tên trạng thái
        /// </summary>
        public string UserStatusName
        {
            get
            {
                switch (UserStatus)
                {
                    case Enum.Status.NotActive:
                        return Resources.Status.ResourceManager.GetString("NotActive");
                    case Enum.Status.Wait:
                        return Resources.Status.ResourceManager.GetString("Wait");
                    case Enum.Status.Active:
                        return Resources.Status.ResourceManager.GetString("Active");
                    case Enum.Status.Inactive:
                        return Resources.Status.ResourceManager.GetString("Inactive");
                    default:
                        return Resources.Status.ResourceManager.GetString("NotActive");
                }
            }
        }

        /// <summary>
        /// Danh sách vai trò 
        /// </summary>
        public List<Role> Roles { get; set; }
        #endregion
    }
}
