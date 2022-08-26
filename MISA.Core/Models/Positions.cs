using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Models
{
    public class Positions:BaseEntity
    {
        #region Constructor
        public Positions()
        {
            PositionID = new Guid();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Khoá chính
        /// </summary>
        public Guid PositionID { get; set; }
        /// <summary>
        /// Mã vị trí
        /// </summary>
        public string PositionCode { get; set; }
        /// <summary>
        /// Tên vị trí
        /// </summary>
        public string PositionName { get; set; }
        #endregion
    }
}
