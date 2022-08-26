using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Interface;
using MISA.Infrastructure.Repository;

namespace MISA.Process.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<MISAEntity> : ControllerBase
    {
        #region Properties
        /// <summary>
        /// Biến lưu trữ các model tương ứng với database
        /// </summary>
        protected string tableName;
        IBaseRepository<MISAEntity> _repository;
        #endregion

        #region Constructor
        public BasesController(IBaseRepository<MISAEntity> repository)
        {
            _repository = repository;

            //Lấy ra tên các model tương ứng
            this.tableName = typeof(MISAEntity).Name;
        }
        #endregion

        #region Methods

        /// <summary>
        /// API lấy toàn bộ thông tin dữ liệu
        /// </summary>
        /// <returns> Thành công trả về mã code: 200 và danh sách thông tin dữ liệu </returns>
        /// <exception cref="Exception"></exception>
        /// Author: Dương Minh Việt (7/8/2022)
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                //Trả về danh sách thông tin dữ liệu
                var res =_repository.GetAll();
                return StatusCode(200, res);
            }
            catch
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// API lấy ra thông tin dữ liệu thông qua ID
        /// </summary>
        /// <param name="id"> ID đối tượng cần lấy ra thông tin dữ liệu </param>
        /// <returns> Thành công trả về mã 200 và thông tin dữ liệu tương ứng </returns>
        /// <exception cref="Exception"></exception>
        /// Author: Dương Minh Việt (7/8/2022)
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                //trả về thông tin dữ liệu cần lấy ra
                var res = _repository.GetById(id);
                return StatusCode(200, res);
            }
            catch
            {
                throw new Exception();

            }
        }

        /// <summary>
        /// API xoá thông tin dữ liệu 
        /// </summary>
        /// <param name="id"> ID đối tượng dữ liệu cần xoá thông tin </param>
        /// <returns> Trả về 1 - nếu xoá thành công </returns>
        /// <exception cref="Exception"></exception>
        /// Author: Dương Minh Việt (7/8/2022)
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            try
            {
                //trả về kết quả
                var res = _repository.Delete(id);
                return StatusCode(200, res);
            }
            catch
            {
                throw new Exception();

            }
        }
        #endregion
    }
}
