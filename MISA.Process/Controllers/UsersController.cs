using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Exceptions;
using MISA.Core.Interface;
using MISA.Core.Interface.Repository;
using MISA.Core.Interface.Service;
using MISA.Core.Models;
using MISA.Core.Resources;

namespace MISA.Process.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : BasesController<User>
    {
        #region Constructor
        IUserRepository _repository;
        IUserService _service;
        public UsersController(IUserRepository repository, IUserService service) : base(repository)
        {
            _repository = repository;
            _service = service;
        }
        #endregion

        /// <summary>
        /// API lấy ra danh sách người dùng
        /// </summary>
        /// <param name="pageNumber"> số trang </param>
        /// <param name="pageSize"> số bản ghi trên 1 trang </param>
        /// <param name="userFilter"> từ khoá để tìm kiếm </param>
        /// <returns> Danh sách người dùng </returns>
        /// CreateBy: Dương Minh Việt (12/7/2022)
        [HttpGet("Filter")]
        public IActionResult GetPaging(int pageSize, int pageNumber, string? userFilter, Guid? roleID)
        {
            try
            {
                //trả về kết quả
                var res = _repository.GetPaging(pageSize, pageNumber, userFilter, roleID);
                return StatusCode(200, res);

            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }
        }

        /// <summary>
        /// API thêm mới một người dùng
        /// </summary>
        /// <param name="users"> Đối tượng người dùng cần thêm mới </param>
        /// <returns> Trả về số người dùng thêm mới thành công </returns>
        /// <exception cref="Exception"></exception>
        /// Author: DƯơng Minh Việt (7/8/2022)
        #region Methods
        [HttpPost]
        public IActionResult InsertUser(List<User> users)
        {
            try
             { 
                var res = 0;
                foreach (var user in users)
                {
                    //Thêm mới ngươì dùng
                    res = _service.Insert(user);
                    if(res != 1)
                    {
                        res = 0;
                        break;
                    }
                }
                return StatusCode(201, res);
            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }
        }

        /// <summary>
        /// API kiểm tra trùng mã 
        /// </summary>
        /// <param name="code"> Mã cần kiểm tra </param>
        /// <returns> Trả về true - nếu mã không bị trùng, false là mã bị trùng </returns>
        /// Author: DƯơng Minh Việt (7/8/2022)

        [HttpPost("CheckCode")]
        public IActionResult CheckCode(string code)
        {
            try
            {
                //trả về kết quả
                var res = _repository.CheckCode(code);
                return StatusCode(200, res);
            }
            catch(Exception ex)
            {
                return HandelException(ex);

            }
        }

        /// <summary>
        /// API sửa vai trò của người dùng
        /// </summary>
        /// <param name="user"> Đối tượng người dùng cần sửa vai trò </param>
        /// <returns> Trả về 1 nếu sửa thành công </returns>
        /// Author: Dương Minh Việt (8/8/2022)
        [HttpPut]
        public IActionResult UpdateUserRole(User user)
        {
            try
            {
                //Lấy ra kết quả
                var res = _service.UpdateUserRole(user);
                //trả về kết quả
                return StatusCode(200, res);

            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }
        }

        /// <summary>
        /// Api lấy mã code mới cho user
        /// </summary>
        /// <returns> Trả về mã mới </returns>
        [HttpGet("NewUserCode")]
        public IActionResult GetNewCode()
        {
            try
            {
                //láy ra kết quả 
                var res = _repository.newUserCode();
                //trả về kết quả
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return HandelException(ex);

            }
        }

        /// <summary>
        /// Xử lí khi có Exception xảy ra
        /// </summary>
        /// <param name="ex"> Exception văng ra </param>
        /// <returns> Trả về tương ứng với kiểu ex được văng ra</returns>
        private IActionResult HandelException(Exception ex)
        {
            if (ex is MSException)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = ex.Message,
                    data = ex.Data
                };
                return StatusCode(400, res);
            }
            else
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = ResourcesException.ResourceManager.GetString("500"),
                    data = ex.Data
                };
                return StatusCode(500, res);
            }
        }
        #endregion

    }
}
