import { HTTP } from "./BaseApi.js";
class DepartmentApi {
  controller = "Departments";
  getAll() {
    return HTTP.get(`${this.controller}`);
  }
}
export default new DepartmentApi();
