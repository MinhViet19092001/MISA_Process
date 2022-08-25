import { HTTP } from "./BaseApi.js";
class RoleApi {
  controller = "Roles";
  getAll() {
    return HTTP.get(`${this.controller}`);
  }
}
export default new RoleApi();
