﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Models
{
    public class Department : BaseEntity
    {
        #region Constructor
        public Department()
        {
            DepartmentID = Guid.NewGuid();
        }
        #endregion
        #region Propeties
        /// <summary>
        /// Khoá chính
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
        #endregion
    }
}
