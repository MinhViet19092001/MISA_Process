<template>
  <div class="con-ms-popup popup-update-user">
    <div class="ms-popup--background"></div>
    <div class="ms-popup flex">
      <div class="popup-update-header">
        <header class="ms-popup--header">
          <div class="ms-popup--title">
            <h2>
              <span>Sửa người dùng</span>
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
      <div class="ms-popup--content" style="height: calc(100% - 60px)">
        <div>
          <div class="item-wrap">
            <div class="flex items-center">
              <div
                class="avatar m-r-8 flex items-center justify-center"
                v-bind:style="
                  'background-color: ' + bgAvatar(userUpdate.userName)
                "
              >
                <span class="bold" style="color: #fff; font-size: 38px">
                  {{ avatar(userUpdate.userName) }}
                </span>
              </div>
              <div class="user-information">
                <div>
                  <span
                    ><b>{{ userUpdate.userName }}</b></span
                  >
                  <span style="margin-left: 4px"
                    >({{ userUpdate.userCode }})</span
                  >
                </div>
                <div style="margin: 4px 0px">
                  <span>{{ userUpdate.email }}</span>
                </div>
                <div>
                  <span>{{ userUpdate.positionName }}</span>
                  <span> - </span>
                  <span>{{ userUpdate.departmentName }}</span>
                </div>
              </div>
            </div>
          </div>
          <br />
          <label class="bold" style="font-size: 20px !important">
            <p
              style="margin: 0 px !important; color: rgb(27, 28, 30) !important"
            >
              Vai trò <span class="required" style="float: inherit">*</span>
            </p>
          </label>
          <br />
          <div
            class="ms-row"
            style="
              display: flex;
              width: 100%;
              line-height: 35px;
              text-indent: 0px;
              padding: 0px;
              margin-bottom: 16px;
            "
          >
            <div
              style="float: left; margin-left: 0%; width: 50%"
              v-for="(role, index) in roles"
              :key="role.roleID"
            >
              <div class="flex items-center">
                <label class="container ms-checkbox" style="color: #212121">
                  <input
                    type="checkbox"
                    value="false"
                    class="ms-checkbox--input"
                  />
                  <span
                    class="checkmark"
                    v-bind:class="
                      check[index] == true
                        ? 'icon-checkbox-primary'
                        : 'icon-checkbox-uncheck'
                    "
                    @click="checkItem(index)"
                  ></span>
                  <span class="con-slot-label" @click="checkItem(index)">
                    <div :title="role.roleDescription" style="margin-top: -5px">
                      {{ role.roleDescription }}
                    </div>
                  </span>
                </label>
              </div>
            </div>
          </div>
        </div>
      </div>
      <footer class="ms-popup--footer borderFooter" style="height: 60px">
        <div class="flex" style="margin-right: 12px">
          <button
            type="button"
            name="button"
            class="ms-button ms-button-secondary"
            style="
              width: auto !important;
              height: 36px !important;
              border-radius: 4px;
              padding: 0px 16px;
              min-width: 80px !important;
              margin-right: 12px !important;
            "
            @click="closePopup"
          >
            <span class="ms-button-text ms-button--text">Huỷ</span>
          </button>
          <button
            type="button"
            name="button"
            class="ms-button ms-button-primary"
            style="
              width: 70px !important;
              height: 36px !important;
              border-radius: 4px;
              padding: 0px 16px;
              min-width: 80px !important;
            "
            @click="btnSaveOnCLick"
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
    minWidth="300px"
  >
  </DxToast>
</template>
<script>
import { DxTooltip } from "devextreme-vue/tooltip";
import { createAvatar, createBgAvatar } from "../../../common/avatar";
import RoleApi from "../../../api/RoleApi.js";
import UserApi from "../../../api/UserApi.js";
import { DxToast } from "devextreme-vue/toast";

export default {
  name: "UpdateUser",
  components: { DxTooltip, DxToast },
  props: ["userUpdate"],
  data() {
    return {
      //Biến xác định ẩn hiện tooltip nút x (close)
      tooltipBtnClose: false,
      //Danh sách các vai trò
      roles: [],
      //Mảng để xác định xem vai trò nào được checked
      check: [],
      //Đối tượng cần update:
      user: {},
      //Biến xác định ẩn hiện Toast
      isShowToast: false,
      //Nội dung thông báo
      messageToast: "",
      //Kiểu thông báo
      typeToast: "",
      //Biến xác định có xem có cập nhật dữ liệu hay không
      isValid: true,
    };
  },
  async created() {
    //Load dữ liệu vai trò
    await this.loadRole();
  },
  methods: {
    //Phương thức ẩn hiển tooltip nút x (close)
    toggleBtnClose() {
      this.tooltipBtnClose = !this.tooltipBtnClose;
    },
    //Sự kiện click vào nút x (close) để đóng popup
    closePopup() {
      this.$emit("showUpdateUser", false);
    },
    //tạo tên avatar
    avatar(name) {
      return createAvatar(name);
    },
    //tạo bg cho avatar
    bgAvatar(name) {
      return createBgAvatar(this.avatar(name));
    },
    //Phương thức gọi dữ liệu Role
    async loadRole() {
      var me = this;
      //Gọi API lấy về dữ liệu
      await RoleApi.getAll().then((res) => {
        //gán giá trị cho mảng roles
        me.roles = res.data;
        //Mảng chứa danh sách chi tiết vai trò
        const arrRoleDes = me.roles.map((x) => x.roleDescription);
        const arrRoleName = me.userUpdate.roleName.split(",");
        //binding dữ liệu
        for (let index = 0; index < arrRoleDes.length; index++) {
          //Định nghĩa lại trạng thái vai trò các roles
          me.roles[index].roleState = 0;
          if (arrRoleName.includes(arrRoleDes[index])) {
            me.check.push(true);
          } else {
            me.check.push(false);
          }
        }
      });
    },
    //Phương thức click vào các vai trò trong popup sửa
    checkItem(index) {
      if (this.check[index] == true) {
        this.check[index] = false;
        this.roles[index].roleState = 2;
      } else {
        this.check[index] = true;
        this.roles[index].roleState = 1;
      }
    },
    //Phương thức click vào nút lưu
    async btnSaveOnCLick() {
      debugger;
      //Biến kiểm tra xem có bao nhiêu phần tử vai trò không được chọn
      var dem = 0;
      var me = this;
      me.user = me.userUpdate;
      me.user.roles = me.roles;
      //Kiểm tra xem có cập nhật dữ liệu hay không
      //Kiểm tra mảng check,nếu phần tử nào không được chọn sẽ tăng biến dem
      for (let index = 0; index < this.check.length; index++) {
        if (this.check[index] == false) {
          dem++;
        }
      }
      //Nếu biến dem có giá trị bằng với độ dài của phần tử mảng => không cho cập nhật
      if (dem == this.check.length) {
        this.isValid = false;
      } else {
        this.isValid = true;
      }
      //Nếu biến isValid == true, có thể cập nhật được
      if (this.isValid) {
        //Gọi API sửa dữ liệu vai trò
        await UserApi.updateRole(me.user)
          .then((res) => {
            console.log(res);
            if (res.data >= 0) {
              //Emit cho component cha GridUser || PreviewUser = > Toast lên cho người dùng biết là đã xoá thành công
              me.$emit("showToast", "success", "Sửa thành công", true);
              //Load lại dữ liệu
              //Emit cho component cha GridUser => để xác định load lại dữ liệu
              me.$emit("reloadData", true);
              //Emit cho component cha PreviewUser => Ẩn đi component
              me.$emit(
                "showToastPreview",
                "success",
                "Sửa thành công",
                true,
                false,
                true
              );
              //Ẩn đi popup
              me.$emit("showUpdateUser", false);
            } else {
              //Emit cho component cha GridUser || PreviewUser = > Toast lên cho người dùng biết là đã xoá thành công
              me.$emit(
                "showToast",
                "warning",
                "Có lỗi xảy ra vui lòng liên hệ MISA",
                true
              );
            }
          })
          .catch(() => {
            //Emit cho component cha GridUser || PreviewUser = > Toast lên cho người dùng biết là đã xoá thành công
            me.$emit(
              "showToast",
              "warning",
              "Có lỗi xảy ra vui lòng liên hệ MISA",
              true
            );
          });
      } else {
        //Toast lên thông báo cho người dùng
        this.showToast("warning", "Bạn cần chọn vai trò cho người dùng.", true);
      }
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
<style>
@import url("../../../css/user-update.css");
</style>
