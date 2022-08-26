using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interface
{
    public interface IBaseRepository<MISAEntity>
    {
        /// <summary>
        /// Lấy ra danh sách dữ liệu
        /// </summary>
        /// <returns> danh sách dữ liệu </returns>
        /// Author: Dương Minh Việt (5/8/2022)
        IEnumerable<MISAEntity> GetAll();

        /// <summary>
        /// Lấy ra thông tin dữ liệu qua ID
        /// </summary>
        /// <param name="id"> ID đối tượng cần truy xuất thông tin </param>
        /// <returns> Thông tin dữ liệu </returns>
        /// Author: Dương Minh Việt (5/8/2022)
        MISAEntity GetById(Guid id);

        /// <summary>
        /// Xoá dữ liệu
        /// </summary>
        /// <param name="id"> ID đối tượng cần xoá dữ liệu </param>
        /// <returns> Trả về 1 - nếu xoá thành công </returns>
        /// Author: Dương Minh Việt (5/8/2022)
        int Delete(Guid id);
    }
}
