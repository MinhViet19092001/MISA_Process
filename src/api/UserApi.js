import { HTTP } from "./BaseApi.js";

class UserApi {
  controller = "Users";

  /**
   * Tìm kiếm, phân trang
   * @param {*} pageSize   //số bản ghi trên 1 trang hiện tại
   * @param {*} pageNumber  //số trang hiện tại
   * @param {*} keyWord     //từ khoá tìm kiếm
   * @returns danh sách tìm kiếm, tổng số trang , tổng số bản ghi
   */
  filter(pageSize, pageNumber, keyword, roleId) {
    return HTTP.get(
      `${
        this.controller
      }/Filter?pageSize=${pageSize}&pageNumber=${pageNumber}&userFilter=${keyword.trim()}&roleID=${roleId}`
    );
  }

  /**
   * Xoá dữ liệu
   * @param {*} id id người dùng cần xoá
   * @returns trả về 1 nếu xoá thành công
   */
  deleteAll(id) {
    return HTTP.delete(`${this.controller}?id=${id}`);
  }

  /**
   * Sửa dữ liệu vai trò
   * @param {} user người dùng cần sửa dữ liệu vai trò
   */
  updateRole(user) {
    return HTTP.put(`${this.controller}`, user);
  }

  /**
   * Lấy ra mã người dùng mới
   * @returns trả về mã user code mới
   */
  getNewCode() {
    return HTTP.get(`${this.controller}/NewUserCode`);
  }

  /**
   * Thêm mới một người dùng
   * @param {*} users danh sách đối tượng cần thêm mới
   * @returns số user được thêm mới
   */
  insert(users) {
    return HTTP.post(`${this.controller}`, users);
  }

  /**
   * Kiểm tra trùng mã
   * @param {*} code mã đối tượng cần kiểm tra
   * @returns true - nếu mã không trùng, false là mã bị trùng
   */
  checkCode(code) {
    return HTTP.post(`${this.controller}/CheckCode?code=${code}`);
  }
}

export default new UserApi();
