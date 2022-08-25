import { HTTP } from "./BaseApi.js";
class PositionApi {
  controller = "Positions";
  getAll() {
    return HTTP.get(`${this.controller}`);
  }
}
export default new PositionApi();
