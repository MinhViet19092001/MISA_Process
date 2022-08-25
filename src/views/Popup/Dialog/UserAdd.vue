<template>
  <div class="con-ms-popup popup-insert-many-user">
    <div class="ms-popup--background"></div>
    <div class="ms-popup">
      <div>
        <header class="ms-popup--header">
          <div class="ms-popup--title">
            <h2>
              <span>Thêm người dùng</span>
            </h2>
          </div>
          <div class="ms-popup--close-root flex justify-between items-center">
            <div class="flex justify-center btn-icon-1">
              <div class="tooltip-container">
                <div class="con-ms-tooltip">
                  <i
                    class="icon-close"
                    @mouseenter="toggleBtnClose"
                    @mouseleave="toggleBtnClose"
                    @click="closePopup"
                  >
                    <DxTooltip
                      :visible="tooltipBtnClose"
                      :hide-on-outside-click="false"
                      target=".ms-popup--close-root .icon-close"
                      position="bottom"
                    >
                      Đóng
                    </DxTooltip>
                  </i>
                </div>
              </div>
            </div>
          </div>
        </header>
      </div>
      <div class="ms-popup--content">
        <div class="grid-list-user">
          <table>
            <thead>
              <tr>
                <th>STT</th>
                <th>
                  <div>Mã nhân viên<span class="required">*</span></div>
                </th>
                <th>
                  <div>Họ và tên<span class="required">*</span></div>
                </th>
                <th>
                  <div>Phòng ban<span class="required">*</span></div>
                </th>
                <th>
                  <div>Vị trí công việc<span class="required">*</span></div>
                </th>
                <th>
                  <div>Email<span class="required">*</span></div>
                </th>
                <th>
                  <div>Vai trò<span class="required">*</span></div>
                </th>
                <th>
                  <div>Trạng thái<span class="required">*</span></div>
                </th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(userRole, index) in listUserRole" :key="index">
                <td>{{ index + 1 }}</td>
                <td>
                  <input
                    type="text"
                    name="UserCode"
                    class="ms-input"
                    ref="txtUserCodeRef"
                    maxlength="20"
                    v-model="userRole.userCode"
                    id="txtUserCode"
                    tabindex="1"
                    @blur="onblur($event, 'Mã')"
                  />
                </td>
                <td>
                  <input
                    type="text"
                    id="txtUserName"
                    name="UserName"
                    ref="txtUserNameRef"
                    class="ms-input"
                    maxlength="100"
                    v-model="userRole.userName"
                    tabindex="1"
                    @blur="onblur($event, 'Tên')"
                  />
                </td>
                <td>
                  <DxSelectBox
                    :search-enabled="true"
                    :data-source="departments"
                    placeholder="Chọn phòng ban"
                    value-expr="departmentID"
                    display-expr="departmentName"
                    v-model="userRole.departmentID"
                    ref="txtDepartmentIDRef"
                    :tabIndex="1"
                    @focusOut="onblur($event, 'Phòng ban')"
                  >
                  </DxSelectBox>
                </td>
                <td>
                  <DxSelectBox
                    :search-enabled="true"
                    :data-source="positions"
                    placeholder="Chọn vị trí"
                    value-expr="positionID"
                    display-expr="positionName"
                    v-model="userRole.positionID"
                    :tabIndex="1"
                    ref="txtPositionIDRef"
                    @focusOut="onblur($event, 'Vị trí')"
                  >
                  </DxSelectBox>
                </td>
                <td>
                  <input
                    type="text"
                    id="txtEmail"
                    name="Email"
                    class="ms-input"
                    maxlength="100"
                    ref="txtEmailRef"
                    v-model="userRole.email"
                    tabindex="1"
                    @blur="onblur($event, 'Email')"
                  />
                </td>
                <td>
                  <DxTagBox
                    :data-source="roles"
                    display-expr="roleDescription"
                    ref="txtRolesRef"
                    :search-enabled="true"
                    placeholder="Chọn vai trò"
                    :multiline="false"
                    v-model="userRole.roles"
                    :tabIndex="1"
                  ></DxTagBox>
                </td>
                <td>
                  <DxSelectBox
                    :data-source="status"
                    placeholder="Chọn trạng thái"
                    value-expr="UserStatus"
                    display-expr="UserStatusName"
                    v-model="userRole.userStatus"
                    :tabIndex="1"
                    ref="txtUserStatusRef"
                  >
                  </DxSelectBox>
                </td>
                <td>
                  <div
                    class="icon-delete icon-hidden"
                    @click="deleteRow(index)"
                  ></div>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
        <div class="add-row" @click="addNewUser"><span>+ </span>Thêm dòng</div>
      </div>
      <footer class="ms-popup--footer borderFooter">
        <div class="flex">
          <button
            type="button"
            name="button"
            tabindex="2"
            class="ms-button ms-button-secondary ms-button-close"
            @click="closePopup"
            v-on:keydown.tab.stop.prevent="resetTabindex"
          >
            <span class="ms-button-text ms-button--text">Huỷ</span>
          </button>
          <button
            id="btnSaveUser"
            type="button"
            name="button"
            class="ms-button ms-button-primary ms-button-save"
            tabindex="1"
            @click="btnSaveOnClick()"
          >
            <span class="ms-button-text ms-button--text">Lưu</span>
          </button>
        </div>
      </footer>
    </div>
  </div>
  <DxToast
    v-model:visible="isShowToast"
    v-model:message="messageToast"
    v-model:type="typeToast"
    :width="auto"
    :minWidth="300"
  >
  </DxToast>
</template>
<script>
import { inputOnblur } from "../../../common/inputBlur";
import DxTagBox from "devextreme-vue/tag-box";
import { DxSelectBox } from "devextreme-vue/select-box";
import { DxTooltip } from "devextreme-vue/tooltip";
import RoleApi from "../../../api/RoleApi";
import DepartmentApi from "../../../api/DepartmentApi";
import PositionApi from "../../../api/PositionApi";
import { status } from "../../../common/data.js";
import UserRole from "../../../model/UserRole.js";
import UserApi from "@/api/UserApi";
import { DxToast } from "devextreme-vue/toast";

import {
  regexUserCode,
  regexEmail,
  valueNull,
  valueRegex,
  titleNull,
  titleCheckRegex,
  titleCheckCode,
  valueDuplicate,
} from "../../../common/resources";

export default {
  name: "InsertUser",
  components: { DxTooltip, DxSelectBox, DxTagBox, DxToast },
  data() {
    return {
      //Danh sách user thêm mới
      listUserRole: [],
      //Biến ẩn hiện tooltip nút x (close)
      tooltipBtnClose: false,
      //Danh sách phòng ban
      departments: [],
      //Danh sách vị trí công việc
      positions: [],
      //Danh sách vai trò
      roles: [],
      //Danh sách status
      status: status,
      //Đối tượng user từng dòng
      userRole: {},
      //Biến xác định mã code mới
      newUserCode: "",
      //Danh sách các input focus của 1 dòng user
      inputFocus: [],
      //Danh sách chưa các input focus của list user
      listInputFocus: [],
      //Danh sách chứa các lỗi khi validate 1 user
      errorMsgs: [],
      //danh sách lỗi khi nhập của list use
      listErrorMsg: [],
      //Biến xác định validate có hợp lệ hay không
      isValid: true,
      //Biến xác định ẩn hiện Toast
      isShowToast: false,
      //Nội dung thông báo
      messageToast: "",
      //Kiểu thông báo
      typeToast: "",
    };
  },
  async created() {
    //load dữ liệu role
    await this.loadRole();
    //load dữ liệu department
    await this.loadDepartment();
    //load dữ liệu positions
    await this.loadPosition();
    //lấy ra mã code mới
    await this.getNewUserCode();
    //load dòng đầu tiên của dữ liệu
    this.userRole = new UserRole();
    //Gọi API lấy ra mã code mới
    this.userRole.userCode = this.newUserCode;
    //thêm vào danh sách user cần thêm mới
    this.listUserRole.push(this.userRole);
  },
  mounted() {
    //Focus vào ô nhập liệu đầu tiên
    setTimeout(() => {
      const target = document.querySelector("table tbody tr td #txtUserCode");
      target.focus();
    }, 100);
  },
  methods: {
    //Phương thức ẩn hiện tooltip nút x (close)
    toggleBtnClose() {
      this.tooltipBtnClose = !this.tooltipBtnClose;
    },
    //Đóng popup
    closePopup() {
      this.$emit("showInsertUser", false);
    },
    // click vào nút thêm dòng = >Thêm mới 1 dòng user trong form
    addNewUser() {
      //lấy ra mã người dùng cuối cùng
      const lastUserCode =
        this.listUserRole[this.listUserRole.length - 1].userCode;
      //Cắt chuỗi mã người dùng cuối cùng để thành ra các thành phần prefix và postfix
      const arrUserCode = lastUserCode.split("-");
      //Lấy ra thành phần prefix của UserCode
      const prefixUserCode = arrUserCode[0];
      //lấy ra thành phần postfix của UserCode
      const postfixUserCode = +arrUserCode[1] + 1;
      //khởi tạo 1 đối tượng model  UserRole
      let newUser = new UserRole();
      if (postfixUserCode <= 9) {
        newUser.userCode = prefixUserCode + "-000" + postfixUserCode;
      } else if (postfixUserCode > 9 && postfixUserCode <= 99) {
        newUser.userCode = prefixUserCode + "-00" + postfixUserCode;
      } else if (postfixUserCode > 99 && postfixUserCode <= 999) {
        newUser.userCode = prefixUserCode + "-0" + postfixUserCode;
      } else {
        newUser.userCode = prefixUserCode + postfixUserCode;
      }
      //Thêm vào danh sách user thêm mới
      this.listUserRole.push(newUser);
      //focus vào ô mã nhân viên của đối tượng user mới
      this.$nextTick(() => {
        this.$refs.txtUserCodeRef[this.listUserRole.length - 1].focus();
      });
    },
    //CLick vào nút xoá dòng => xoá 1 dòng trong form
    deleteRow(index) {
      this.listUserRole.splice(index, 1);
    },
    //Phương thức gọi dữ liệu Role
    async loadRole() {
      var me = this;
      //Gọi API lấy về dữ liệu
      await RoleApi.getAll().then((res) => {
        //gán giá trị cho mảng roles
        me.roles = res.data;
      });
    },
    //Phương thức gọi dữ liệu Role
    async loadDepartment() {
      var me = this;
      //Gọi API lấy về dữ liệu
      await DepartmentApi.getAll().then((res) => {
        //gán giá trị cho mảng roles
        me.departments = res.data;
      });
    },
    //Phương thức gọi dữ liệu Role
    async loadPosition() {
      var me = this;
      //Gọi API lấy về dữ liệu
      await PositionApi.getAll().then((res) => {
        //gán giá trị cho mảng roles
        me.positions = res.data;
      });
    },
    //Phương thức lấy ra mã user mới
    async getNewUserCode() {
      var me = this;
      await UserApi.getNewCode().then((res) => {
        me.newUserCode = res.data;
      });
    },
    //Phương thức click vào nút lưu => insert dữ liệu
    async btnSaveOnClick() {
      this.listInputFocus = [];
      this.listErrorMsg = [];
      //Check validate dữ liệu
      for (let index = 0; index < this.listUserRole.length; index++) {
        await this.validate(this.listUserRole[index], index);
      }
      if (this.isValid) {
        var me = this;
        //Gọi API thêm mới danh sách dữ liệu
        await UserApi.insert(this.listUserRole)
          .then((res) => {
            if (res.data > 0) {
              //Thông báo cho người dùng thêm mới thành công
              me.$emit("showToast", "success", "Thêm mới thành công", true);
              // load lại dữ liệu
              me.$emit("reloadData", true);
              //Đóng popup
              me.$emit("showInsertUser", false);
            }
          })
          .catch((response) => {
            console.log(response);
            //Thông báo toast cho người dùng là có lỗi
            me.$emit(
              "showToast",
              "warning",
              "Có lỗi xảy ra vui lòng liên hệ MISA",
              true
            );
            //Ẩn đi popup insert
            me.$emit("showInsertUser", false);
          });
      } else {
        //focus vào ô input  và báo lỗi ở ô input sai đầu tiên
        for (let index = 0; index < this.listErrorMsg.length; index++) {
          if (this.listErrorMsg[index].length > 0) {
            let error = this.listErrorMsg[index];
            let focus = this.listInputFocus[index];
            //focus vào ô đầu tiên
            focus[0].focus();
            //show toast thông báo, báo lỗi cho người dùng
            this.showToast("warning", error[0], true);
            break;
          }
        }
      }
    },

    //Phương thức reset lại tabindex trên form
    resetTabindex() {
      this.$nextTick(() => {
        const inputFocusFirst = document.querySelector(
          ".grid-list-user table tbody td input#txtUserCode"
        );
        inputFocusFirst.focus();
      });
    },

    //Phương thức khi blur ra ô input
    onblur($event, fieldName) {
      inputOnblur($event, fieldName);
    },

    //Phương thức validate dữ liệu cho user
    async validate(user, index) {
      debugger;
      //Làm rỗng danh sách errorMsg
      this.errorMsgs = [];
      //làm rỗng danh sách input focus
      this.inputFocus = [];
      //#region Kiểm tra mã người dùng
      //Kiểm tra mã trống
      if (!user.userCode) {
        //Thêm thuộc tính báo đỏ ở input
        this.$refs.txtUserCodeRef[index].setAttribute("required", true);
        //Thêm thuộc tính title ở input
        this.$refs.txtUserCodeRef[index].setAttribute("title", titleNull("Mã"));
        //thêm vào danh sách báo lỗi
        this.errorMsgs.push(valueNull("Mã", index));
        //focus vào ô mã nhân viên
        this.inputFocus.push(this.$refs.txtUserCodeRef[index]);
      }
      //Khi có giá trị ở ô mã người dùng
      else {
        //Gọi API kiểm tra trùng mã
        //Biến kiểm tra xem mã có bị trùng hay không
        let CodeValid = true;
        await UserApi.checkCode(user.userCode).then((res) => {
          CodeValid = res.data;
        });
        //Kiểm tra xem có đúng định dạng MS- hay không
        var checkRegexUserCode = new RegExp(regexUserCode);
        if (!checkRegexUserCode.test(user.userCode)) {
          //Thêm thuộc tính báo dỏ ở input
          this.$refs.txtUserCodeRef[index].setAttribute("required", true);
          //Thêm thuộc tính title ở input
          this.$refs.txtUserCodeRef[index].setAttribute(
            "title",
            titleCheckRegex("Mã")
          );
          //Thêm vào danh sách báo lỗi
          this.errorMsgs.push(valueRegex("Mã", index));
          //thêm vào danh sách focus khi có lỗi
          this.inputFocus.push(this.$refs.txtUserCodeRef[index]);
        } else if (!CodeValid) {
          //Thêm thuộc tính báo dỏ ở input
          this.$refs.txtUserCodeRef[index].setAttribute("required", true);
          //Thêm thuộc tính title ở input
          this.$refs.txtUserCodeRef[index].setAttribute(
            "title",
            titleCheckCode("Mã", index)
          );
          //Thêm vào danh sách báo lỗi
          this.errorMsgs.push(valueDuplicate("Mã", index));
          //thêm vào danh sách focus khi có lỗi
          this.inputFocus.push(this.$refs.txtUserCodeRef[index]);
        } else {
          this.inputFocus = this.inputFocus.filter(
            (item) => item != this.$refs.txtUserCodeRef[index]
          );
        }
      }
      //#endregion

      //#region Kiểm tra tên người dùng
      //Kiểm tra tên trống
      if (!user.userName) {
        //Thêm thuộc tính báo đỏ ở input
        this.$refs.txtUserNameRef[index].setAttribute("required", true);
        //Thêm thuộc tính title ở input
        this.$refs.txtUserNameRef[index].setAttribute(
          "title",
          titleNull("Tên")
        );
        //Thêm vào danh sách báo lỗi
        this.errorMsgs.push(valueNull("Tên", index));
        //thêm vào danh sách focus khi có lỗi
        this.inputFocus.push(this.$refs.txtUserNameRef[index]);
      } else {
        this.inputFocus = this.inputFocus.filter(
          (item) => item != this.$refs.txtUserNameRef[index]
        );
      }
      //#endregion

      //#region Kiểm tra phòng ban
      if (!user.departmentID) {
        //Thêm thuộc tính báo đỏ ở input
        this.$refs.txtDepartmentIDRef[index].instance._$element[0].setAttribute(
          "required",
          true
        );
        this.$refs.txtDepartmentIDRef[
          index
        ].instance._labelContainerElement.setAttribute("required", true);

        //Thêm title vào ô input
        this.$refs.txtDepartmentIDRef[
          index
        ].instance._labelContainerElement.setAttribute(
          "title",
          titleNull("Phòng ban")
        );
        //Thêm vào danh sách báo lỗi
        this.errorMsgs.push(valueNull("Phòng ban", index));

        //thêm vào danh sách focus khi có lỗi
        this.inputFocus.push(
          this.$refs.txtDepartmentIDRef[index].instance._labelContainerElement
        );
        //Thêm vào danh sách báo lỗi
        this.errorMsgs.push(valueNull("Phòng ban", index));
      } else {
        this.inputFocus = this.inputFocus.filter(
          (item) =>
            item !=
            this.$refs.txtDepartmentIDRef[index].instance._labelContainerElement
        );
      }
      //#endregion

      //#region Kiểm tra vị trí
      if (!user.positionID) {
        //Thêm thuộc tính báo đỏ ở input
        this.$refs.txtPositionIDRef[index].instance._$element[0].setAttribute(
          "required",
          true
        );
        //Thêm title vào ô input
        this.$refs.txtPositionIDRef[
          index
        ].instance._labelContainerElement.setAttribute(
          "title",
          titleNull("Vị trí")
        );
        //Thêm vào danh sách báo lỗi
        this.errorMsgs.push(valueNull("Vị trí", index));
        //thêm vào danh sách focus khi có lỗi
        this.inputFocus.push(
          this.$refs.txtPositionIDRef[index].instance._labelContainerElement
        );
      } else {
        this.inputFocus = this.inputFocus.filter(
          (item) =>
            item !=
            this.$refs.txtPositionIDRef[index].instance._labelContainerElement
        );
      }
      //#endregion

      //#region Kiểm tra email
      //Kiểm tra Email
      if (!user.email) {
        //Thêm thuộc tính báo đỏ ở input
        this.$refs.txtEmailRef[index].setAttribute("required", true);
        //Thêm thuộc tính title ở input
        this.$refs.txtEmailRef[index].setAttribute("title", titleNull("Email"));
        //thêm vào danh sách báo lỗi
        this.errorMsgs.push(valueNull("Email", index));
        //focus vào ô mã nhân viên
        this.inputFocus.push(this.$refs.txtEmailRef[index]);
      }
      //Khi có giá trị ở ô mã người dùng
      else {
        //Kiểm tra xem có đúng định dạng MS- hay không
        var checkRegexEmail = new RegExp(regexEmail);
        if (!checkRegexEmail.test(user.email)) {
          //Thêm thuộc tính báo dỏ ở input
          this.$refs.txtEmailRef[index].setAttribute("required", true);
          //Thêm thuộc tính title ở input
          this.$refs.txtEmailRef[index].setAttribute(
            "title",
            titleCheckRegex("Email")
          );
          //Thêm vào danh sách báo lỗi
          this.errorMsgs.push(valueRegex("Email", index));
          //thêm vào danh sách focus khi có lỗi
          this.inputFocus.push(this.$refs.txtEmailRef[index]);
        } else {
          this.inputFocus = this.inputFocus.filter(
            (item) => item != this.$refs.txtEmailRef[index]
          );
        }
      }
      //#endregion

      //#region Kiểm tra danh sách vai trò
      if (!user.roles.length || user.roles.length < 0) {
        //Thêm thuộc tính báo đỏ ở input
        this.$refs.txtRolesRef[index].instance._$element[0].setAttribute(
          "required",
          true
        );
        //Thêm title vào ô input
        this.$refs.txtRolesRef[
          index
        ].instance._labelContainerElement.firstChild.setAttribute(
          "title",
          titleNull("Vai trò")
        );
        //Thêm vào danh sách báo lỗi
        this.errorMsgs.push(valueNull("Vai trò", index));
        //thêm vào danh sách focus khi có lỗi
        this.inputFocus.push(
          this.$refs.txtRolesRef[index].instance._labelContainerElement
            .firstChild
        );
      } else {
        this.inputFocus = this.inputFocus.filter(
          (item) =>
            item !=
            this.$refs.txtRolesRef[index].instance._labelContainerElement
              .firstChild
        );
      }
      //#endregion

      //Nếu có lỗi xảy ra => validate không hợp lệ
      if (this.errorMsgs.length > 0) {
        //valid không hợp lệ
        this.isValid = false;
      } else {
        this.isValid = true;
      }
      //push vào danh sách input focus cho list user
      this.listInputFocus.push(this.inputFocus);
      //push vào danh sách báo lỗi cho list user
      this.listErrorMsg.push(this.errorMsgs);
    },

    //Phương thức show toast thông báo
    showToast(type, message, isShow) {
      this.typeToast = type;
      this.messageToast = message;
      this.isShowToast = isShow;
    },
  },
};
</script>
<style scoped>
@import url("@/css/multiple-insert-user.css");
</style>
