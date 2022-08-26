using MISA.Core.Exceptions;
using MISA.Core.Interface.Repository;
using MISA.Core.Interface.Service;
using MISA.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace MISA.Core.Service
{
    public class UserService : IUserService
    {
        #region Properties
        IUserRepository _repository;
        //Danh sách chứa các lỗi thông báo khi validate dữ liệu
        protected List<string> errorValidateMsg;
        protected string errorMsg;
        //Biến xác định xem Validate có hợp lệ hay không
        protected bool isValid = true;
        #endregion

        #region Constructor
        public UserService(IUserRepository repository)
        {
            _repository = repository;
            errorValidateMsg = new List<string>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Xử lí khi thêm mới 1 user 
        /// </summary>
        /// <param name="user"> đối tượng cần thêm mới </param>
        /// <returns> Trả về số vai trò của người dùng được thêm mới </returns>
        /// <exception cref="MSException"> Bắt Exception </exception>
        /// Author: Dương Minh Việt (19/8/2022)
        public int Insert(User user)
        {
            try
            {
                //Lấy ra danh sách các vai trò 
                foreach (var role in user.Roles)
                {
                    user.RoleName += role.RoleDescription + ",";
                }
                //Cắt bỏ dấu phẩy ở cuối danh sách vai trò
                user.RoleName = user.RoleName.TrimEnd(',');
                //Nếu validate hợp lệ
                if (Validate(user))
                {
                    //Thêm mới người dùng 
                    var res = _repository.Insert(user);
                    if (res > 0)
                    {
                        //Lưu các vai trò của user vào bảng UserRole
                        var result = _repository.SaveUserRole(user);
                        if (result > 0)
                        {
                            //Trả về kết quả 
                            return res;
                        }
                    }
                }
            }
            catch
            {
                throw new MSException(errorValidateMsg);

            }
            return 0;

        }

        /// <summary>
        /// Xử lí cập nhật vai trò của user
        /// </summary>
        /// <param name="user"> Dối tượng user cần cập nhật vai trò </param>
        /// <returns> Trả về 1 nếu cập nhật thành công </returns>
        /// Author: Dương Minh Việt (19/8/2022)
        public int UpdateUserRole(User user)
        {
            try
            {
                //Kiểm tra xem danh sách vai trò có hay không => nếu không có vai trò nào => báo lỗi
                if (user.Roles.Count > 0)
                {
                    //Duyệt qua các danh sách vao trò của user
                    foreach (var role in user.Roles)
                    {
                        switch (role.RoleState)
                        {
                            case Enum.RoleState.Insert:
                                //Kiểm tra xem vai trò này đã có đối với người dùng này hay chưa
                                var check = _repository.CheckRole(user.UserID, role.RoleID);
                                if (check == false)
                                {
                                    break;
                                }
                                else
                                {
                                    //Thêm mới 1 vai trò vào bảng UserRole
                                    var res = _repository.InsertUserRole(user.UserID, role.RoleID);
                                    if (res == 0)
                                    {
                                        //Xử lí lỗi khi insert vai trò không thành công vào bảng UserRole
                                        break;
                                    }
                                    else
                                    {
                                        if(user.RoleName != null)
                                        {
                                            //Cập nhật RoleName trong User
                                            user.RoleName += "," + role.RoleDescription;
                                        }
                                        else
                                        {
                                            //Cập nhật RoleName trong User
                                            user.RoleName += role.RoleDescription;
                                        }
                                        //Cập nhật lại User
                                        var responseInsert = _repository.Update(user);
                                    }
                                }
                                break;
                            case Enum.RoleState.Remove:
                                //Xoá vai trò của user trong bảng UserRole
                                var res1 = _repository.DeleteUserRole(user.UserID, role.RoleID);

                                //Cập nhật lại RoleName trong user 
                                //Tạo mảng chứa danh sách các RoleName
                                List<string> RoleNames = user.RoleName.Split(",").ToList();
                                //Xoá đi vai trò trong mảng
                                RoleNames.Remove(role.RoleDescription);
                                //Tạo lại RoleName
                                user.RoleName = null;
                                foreach (var roleName in RoleNames)
                                {
                                    user.RoleName += roleName + ",";
                                }
                                //Cắt bỏ dấu , cuối chuỗi RoleName
                                if(user.RoleName != null)
                                {
                                    user.RoleName = user.RoleName.TrimEnd(',');

                                }
                                //Cập nhật lại User
                                var responseDelete = _repository.Update(user);
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    errorValidateMsg.Add(Resources.ValidateUser.ResourceManager.GetString("RolesNull"));
                    throw new MSException(errorValidateMsg);
                }
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
            return 0;
        }

        /// <summary>
        /// Kiểm tra xem chuỗi truyền vào có đúng với Regex hay không 
        /// </summary>
        /// <param name="s"> Chuỗi cần kiểm tra </param>
        /// <param name="regex"> chuỗi regex để so sánh </param>
        /// <returns> True - nếu chuỗi đúng với regex , False - nếu chuỗi không đúng với regex</returns>
        private bool CheckRegex(string s, Regex regex)
        {
            if (regex.Match(s).Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Validate dữ liệu đầu vào user
        /// </summary>
        /// <param name="user"> đối tượng cần validate dữ liệu </param>
        /// <returns></returns>
        public bool Validate(User user)
        {
            //Validate dữ liệu 
            //Kiếm tra trùng mã, nếu mã đã tồn tại => báo lỗi
            if (!_repository.CheckCode(user.UserCode))
            {
                HandelException(Resources.ValidateUser.ResourceManager.GetString("CodeDuplicate"));
            }

            //Nếu mã trống => báo lỗi
            if (string.IsNullOrEmpty(user.UserCode))
            {
                HandelException(Resources.ValidateUser.ResourceManager.GetString("CodeNull"));
            }
            else
            {
                //Kiểm tra mã người dùng xem có đúng định dạng hay không, nếu không đúng => báo lỗi
                var regexUserCode = new Regex("MS-[0-9]*");
                if (!CheckRegex(user.UserCode, regexUserCode))
                {
                    HandelException(Resources.ValidateUser.ResourceManager.GetString("CodeNotValid"));
                }

            }
            //Nếu tên trống => báo lỗi
            if (string.IsNullOrEmpty(user.UserName))
            {
                HandelException(Resources.ValidateUser.ResourceManager.GetString("NameNull"));
            }
            //nếu phòng ban trống => báo lỗi
            if (string.IsNullOrEmpty(user.DepartmentID.ToString()))
            {
                HandelException(Resources.ValidateUser.ResourceManager.GetString("DepartmentNull"));
            }
            //Nếu vị trí trống => báo lỗi
            if (string.IsNullOrEmpty(user.PositionID.ToString()))
            {
                HandelException(Resources.ValidateUser.ResourceManager.GetString("PositionNull"));
            }
            //Nếu email trống => báo lỗi
            if (string.IsNullOrEmpty(user.Email))
            {
                HandelException(Resources.ValidateUser.ResourceManager.GetString("EmailNull"));
            }
            //Kiểm tra Email xem có đúng định dạng hay không, nếu không đúng => báo lỗi
            var regexEmail = new Regex(@"[a-zA-Z0-9_\.+]@[a-zA-Z0-9\.]+");
            if (!CheckRegex(user.Email, regexEmail))
            {
                HandelException(Resources.ValidateUser.ResourceManager.GetString("EmailNotValid"));
            }
            //Nếu trạng thái trống => báo lỗi
            if (string.IsNullOrEmpty(user.UserStatus.ToString()))
            {
                HandelException(Resources.ValidateUser.ResourceManager.GetString("UserStatusNull"));
            }
            //Nếu danh sách vai trò rỗng => báo lỗi
            if (user.Roles.Count <= 0)
            {
                HandelException(Resources.ValidateUser.ResourceManager.GetString("RolesNull"));
            }
            //Trả về giá trị xem validate có hợp lệ hay không
            return isValid;
        }

        /// <summary>
        /// Xử lí exception khi validate dữ liệu
        /// </summary>
        /// <param name="error"> Lỗi xảy ra </param>
        /// Author: Dương Minh Việt (19/8/2022)
        public void HandelException(string error)
        {
            isValid = false;
            errorValidateMsg.Add(error);
        }
        #endregion
    }

}
