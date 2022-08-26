using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Exceptions
{
    public class MSException:Exception
    {
        #region Properties

        /// <summary>
        /// Chuỗi thông báo lỗi
        /// </summary>
        public string? errorMsg { get; set; } 
        /// <summary>
        /// Từ điển để lưu trữ danh sách các Exception
        /// </summary>
        public IDictionary? errors { get; set; }
        #endregion

        #region Constructor
        public MSException(List<string> error)
        {
            this.errorMsg = error[0];
            this.errors = new Dictionary<string, object>();
            errors.Add("Error", error);
        }

        public override string Message => this.errorMsg;
        public override IDictionary Data => this.errors; 
        #endregion


    }
}
