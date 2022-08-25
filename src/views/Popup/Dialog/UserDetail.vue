<template>
  <div id="previewUser">
    <div class="detail-preview-user">
      <div class="container">
        <div class="content-user">
          <div class="icon-close" @click="closePopup"></div>
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
            <div style="margin-left: 0px; float: left; width: 16.6667%">
              <div
                class="avatar m-r-8 flex items-center justify-center"
                style="margin-top: 10px"
                v-bind:style="
                  'background-color: ' + bgAvatar(userSelected.userName)
                "
              >
                <div class="bold" style="color: #fff; font-size: 38px">
                  {{ avatar(userSelected.userName) }}
                </div>
              </div>
            </div>
            <div style="margin-left: 0px; float: left; width: 75%">
              <div class="fullName bold" style="font-size: 20px !important">
                {{ userSelected.userName }}
              </div>
              <div class="email">{{ userSelected.email }}</div>
              <div class="status">
                <div style="padding-left: 16px !important; position: relative">
                  <span
                    class="dot"
                    v-bind:style="
                      'background-color:' + userStatus(userSelected.userStatus)
                    "
                  ></span>
                  <span
                    v-bind:style="
                      'color: ' + userStatus(userSelected.userStatus)
                    "
                    >{{ userSelected.userStatusName }}</span
                  >
                </div>
              </div>
              <div class="flex">
                <button
                  type="button"
                  name="button"
                  class="ms-button ms-button-waiting"
                  style="
                    width: auto !important;
                    height: 36px !important;
                    border-radius: 4px;
                    padding: 0px 16px;
                    min-width: 80px !important;
                  "
                  @click="showPopupUpdate"
                >
                  <span class="ms-button-text ms-button--text">Cập nhật</span>
                </button>
                <button
                  type="button"
                  name="button"
                  style="
                    width: auto !important;
                    height: 36px !important;
                    border-radius: 4px;
                    padding: 0px 16px;
                    min-width: 80px !important;
                  "
                  class="ms-button ms-button-waiting"
                  @click="showPopupDelete"
                >
                  <span class="ms-button-text ms-button--text">Xoá</span>
                </button>
              </div>
            </div>
          </div>
          <div class="body-preview">
            <div
              class="bold"
              style="font-size: 20px !important; margin-top: 20px"
            >
              THÔNG TIN CÔNG VIỆC
            </div>
            <div
              class="ms-row flex"
              style="
                width: 100%;
                line-height: 35px;
                text-indent: 0px;
                padding: 0px;
                margin-bottom: 16px;
                margin-top: 20px;
              "
            >
              <div style="margin-left: 0%; width: 29.1667%; float: left">
                <div class="bold">Mã nhân viên</div>
              </div>
              <div style="margin-left: 0%; width: 50%; float: left">
                <div
                  class="UserCode"
                  style="border-bottom: 1px solid rgb(231, 232, 233)"
                >
                  {{ userSelected.userCode }}
                </div>
              </div>
            </div>
            <div
              class="ms-row flex"
              style="
                width: 100%;
                line-height: 35px;
                text-indent: 0px;
                padding: 0px;
                margin-bottom: 16px;
              "
            >
              <div style="margin-left: 0%; width: 29.1667%; float: left">
                <div class="bold">Phòng ban</div>
              </div>
              <div style="margin-left: 0%; width: 50%; float: left">
                <div
                  class="DepartmentName"
                  style="border-bottom: 1px solid rgb(231, 232, 233)"
                >
                  {{ userSelected.departmentName }}
                </div>
              </div>
            </div>
            <div
              class="ms-row flex"
              style="
                width: 100%;
                line-height: 35px;
                text-indent: 0px;
                padding: 0px;
                margin-bottom: 16px;
              "
            >
              <div style="margin-left: 0%; width: 29.1667%; float: left">
                <div class="bold">Vị trí công việc</div>
              </div>
              <div style="margin-left: 0%; width: 50%; float: left">
                <div
                  class="PositionName"
                  style="border-bottom: 1px solid rgb(231, 232, 233)"
                >
                  {{ userSelected.positionName }}
                </div>
              </div>
            </div>
            <div
              class="bold"
              style="font-size: 20px !important; margin-top: 20px"
            >
              THÔNG TIN PHÂN QUYỀN
            </div>
            <div class="w-full grid-paging" style="margin-top: 20px">
              <div class="grid-container">
                <DxDataGrid
                  :data-source="userSelected.userRoles"
                  :column-auto-width="true"
                  :show-borders="false"
                  :show-row-lines="true"
                  :allow-sorting="false"
                  >>
                  <DxSorting mode="none" />
                  <DxColumn data-field="roleDescription" caption="Vai trò" />
                  <DxScrolling column-rendering-mode="virtual" />
                </DxDataGrid>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <UpdateUser
    v-if="isShowUpdateUser"
    @showUpdateUser="showPopupUpdateUser"
    :userUpdate="userSelected"
    @showToastPreview="showToastPreview"
  />
  <DeleteUser
    v-if="isShowDeleteUser"
    @showDeleteUser="showPopupDeleteUser"
    :userDelete="userSelected"
    @showToastPreview="showToastPreview"
  />
  <DxToast
    v-model:visible="isShowToast"
    v-model:message="messageToast"
    v-model:type="typeToast"
    :width="300"
  >
  </DxToast>
</template>
<script>
import UpdateUser from "./UserUpdate.vue";
import DeleteUser from "../Message/UserRemove.vue";
import { DxToast } from "devextreme-vue/toast";
import { createAvatar, createBgAvatar } from "../../../common/avatar";
import {
  DxDataGrid,
  DxColumn,
  DxScrolling,
  DxSorting,
} from "devextreme-vue/data-grid";
export default {
  name: "PreviewUser",
  components: {
    DxDataGrid,
    DxColumn,
    DxScrolling,
    DxSorting,
    UpdateUser,
    DeleteUser,
    DxToast,
  },
  props: {
    //Đối tượng được chọn
    userSelected: {},
    //Chức năng xử lý trạng thái của user
    userStatus: {
      type: Function,
    },
  },
  data() {
    return {
      //Biến xác định ẩn hiện popup update user
      isShowUpdateUser: false,
      //Biến xác định ẩn hiện popup delete user
      isShowDeleteUser: false,
      //Kiểu thông báo
      typeToast: "",
      //Nội dung thông báo
      messageToast: "",
      //Biến xác định ẩn hiện Toast
      isShowToast: false,
      //Đối tượng user được chọn
      userSelect: this.userSelected,
      //Danh sách vai trò của user
      userRoles: [],
    };
  },
  methods: {
    ///Sự kiện click vào nút x sẽ đóng popup
    closePopup() {
      this.$emit("showPreviewUser", false);
    },
    //Sự kiện click vào nút sửa hiện lên popup update user
    showPopupUpdate() {
      this.showPopupUpdateUser(true);
    },
    //Sự kiện click vào nút sửa hiện lên popup update user
    showPopupDelete() {
      this.showPopupDeleteUser(true);
    },
    //Phương thức ẩn hiện popup update user
    showPopupUpdateUser(isShow) {
      this.isShowUpdateUser = isShow;
    },
    //Phương thức ẩn hiện popup delete user
    showPopupDeleteUser(isShow) {
      this.isShowDeleteUser = isShow;
    },
    //tạo tên avatar
    avatar(name) {
      return createAvatar(name);
    },
    //tạo bg cho avatar
    bgAvatar(name) {
      return createBgAvatar(this.avatar(name));
    },
    //Phương thức xác định thông báo toast
    showToastPreview(type, message, isShowToast, isShowPopup, isReloadData) {
      //Show toast
      this.typeToast = type;
      this.messageToast = message;
      this.isShowToast = isShowToast;
      //đóng popup
      setTimeout(() => {
        this.$emit("showPreviewUser", isShowPopup);
        this.$emit("reloadData", isReloadData);
      }, 1000);
    },
  },
};
</script>
<style>
@import url("../../../css/user-detail.css");
</style>
