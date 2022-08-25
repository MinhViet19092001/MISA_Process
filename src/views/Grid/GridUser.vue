<template>
  <div id="setting-user" class="h-full">
    <div class="w-full">
      <div class="h-full flex-column body-custom" style="padding: 16px 24px">
        <div class="content-header">
          <div class="flex justify-between items-center">
            <div class="bold" style="font-size: 20px">Người dùng</div>
            <button
              class="ms-button ms-button-primary includeIcon cursor-pointer"
              style="
                height: 36px !important;
                width: auto !important;
                padding: 0px 16px 0px 12px;
                min-width: 80px !important;
              "
              @click="showPopupInsert"
            >
              <div
                class="ms-button--icon flex-c-m"
                style="order: 0; margin-right: 4px; margin-left: 0px"
              >
                <i class="icon-plus"></i>
              </div>
              <span class="ms-button-text">Thêm mới</span>
            </button>
          </div>
        </div>
        <div class="content-main flex justify-between">
          <div class="w-full h-full">
            <div class="flex items-center w-full" style="height: 60px">
              <span class="input-search-user">
                <div style="margin-left: 12px">
                  <div style="height: 36px; width: 240px">
                    <div class="h-full w-full flex items-center">
                      <div class="ms-input flex w-full">
                        <div style="padding-left: 8px">
                          <i class="mi-search"></i>
                        </div>
                        <input
                          type="text"
                          maxlength="255"
                          placeholder="Tìm kiếm người dùng"
                          class="ms-input-item"
                          ref="txtSearch"
                          @keyup="filterUser"
                        />
                      </div>
                    </div>
                  </div>
                </div>
              </span>
              <span class="select-box-role">
                <div style="margin-left: 12px !important">
                  <DxSelectBox
                    :data-source="roles"
                    placeholder="Chọn vai trò"
                    value-expr="roleID"
                    display-expr="roleDescription"
                    @value-changed="valueChangedRole"
                  >
                  </DxSelectBox>
                </div>
              </span>
              <div
                class="su-file"
                style="margin-right: 16px !important; margin-left: auto"
              >
                <div class="adjust-column" @click="showPopupAdjustcolumn">
                  <div class="cursor-pointer icon-setting"></div>
                </div>
              </div>
            </div>
            <div class="custom-paging-grid">
              <div class="grid-container">
                <div id="datagrid" class="h-full">
                  <DxDataGrid
                    style="opacity: 1; height: calc(100vh - 267px)"
                    :data-source="users"
                    key-expr="userID"
                    :allow-column-resizing="true"
                    :show-borders="false"
                    :show-row-lines="true"
                    :column-auto-width="true"
                    :column-min-width="80"
                    :hover-state-enabled="true"
                    :allow-sorting="false"
                    :onRowClick="shopPopupPreview"
                  >
                    <DxSorting mode="none" />
                    <DxPager :visible="false" />
                    <DxPaging :enabled="false" />
                    <DxColumn
                      data-field="userCode"
                      caption="Mã nhân viên"
                      alignment="left"
                      max-width="200"
                      width="200"
                      css-class="custom-column"
                    />
                    <DxColumn
                      data-field="userName"
                      caption="Họ và tên"
                      text-align="left"
                      max-width="20"
                      width="200"
                      css-class="custom-column"
                      cell-template="cellTemplateName"
                    />
                    <DxColumn
                      data-field="departmentName"
                      caption="Phòng ban"
                      text-align="left"
                      max-width="200"
                      width="200"
                      css-class="custom-column"
                    />
                    <DxColumn
                      data-field="positionName"
                      caption="Vị trí công việc"
                      text-align="left"
                      max-width="250"
                      width="250"
                      css-class="custom-column"
                    />
                    <DxColumn
                      data-field="email"
                      caption="Email"
                      alignment="left"
                      text-align="left"
                      max-width="250"
                      width="250"
                      css-class="custom-column"
                    />
                    <DxColumn
                      data-field="roleName"
                      caption="Vai trò"
                      alignment="left"
                      text-align="left"
                      max-width="200"
                      width="200"
                      css-class="custom-column"
                    />
                    <DxColumn
                      data-field="userStatusName"
                      caption="Trạng thái"
                      alignment="left"
                      text-align="left"
                      min-width="150"
                      max-width="200"
                      width="200"
                      css-class="custom-column"
                      cell-template="cellTemplateStatus"
                    />
                    <DxColumn
                      :visible="true"
                      cell-template="cellTemplateFunction"
                      text-align="left"
                    />
                    <DxScrolling
                      column-rendering-mode="virtual"
                      :use-native="false"
                      show-scrollbar="always"
                    />
                    <template #cellTemplateName="{ data }">
                      <div>
                        <div class="flex items-center">
                          <div
                            class="user-avatar flex items-center justify-center m-r-8"
                            v-bind:style="
                              'background-color:' + bgAvatar(data.data.userName)
                            "
                          >
                            <span
                              style="
                                color: #fff;
                                font-size: 16px !important;
                                font-weight: 700;
                              "
                            >
                              {{ avatar(data.data.userName) }}
                            </span>
                          </div>
                          <div>{{ data.data.userName }}</div>
                        </div>
                      </div>
                    </template>
                    <template #cellTemplateStatus="{ data }">
                      <div>
                        <div
                          style="padding-left: 16px; position: relative"
                          v-bind:style="
                            'color: ' + userStatus(data.data.userStatus)
                          "
                        >
                          {{ data.data.userStatusName }}
                          <span
                            class="dot"
                            v-bind:style="
                              'background-color:' +
                              userStatus(data.data.userStatus)
                            "
                          ></span>
                        </div>
                      </div>
                    </template>
                    <template #cellTemplateFunction="{ data }">
                      <div
                        class="flex"
                        style="
                          margin-left: 8px !important;
                          height: 100%;
                          justify-content: flex-end !important;
                        "
                      >
                        <div class="style-button">
                          <div
                            title="Sửa"
                            class="button-command-wrap btn-more"
                            @click.stop="showPopupUpdate(data)"
                          >
                            <div class="icon-pencil icon-hidden"></div>
                          </div>
                        </div>
                        <div class="style-button">
                          <div
                            title="Xoá"
                            class="button-command-wrap btn-more"
                            @click.stop="showPopupDelete(data)"
                          >
                            <div class="icon-delete icon-hidden"></div>
                          </div>
                        </div>
                      </div>
                    </template>
                    <DxLoadPanel :enabled="true" />
                  </DxDataGrid>
                  <ThePaging
                    :totalRecord="totalRecord"
                    :pageStart="pageStart"
                    :pageEnd="pageEnd"
                    :totalPage="totalPage"
                    @changePageNumber="changePageNumber"
                    @changePageSize="changePageSize"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="adjust-column-main" v-if="isShowAdjustColumn">
          <header>
            <div class="header p-b-8">
              <div class="flex justify-between items-center p-b-12">
                <div class="title">Tùy chỉnh cột</div>
                <div class="flex justify-center btn-icon-1">
                  <div class="tooltip-container">
                    <div class="con-ms-tooltip">
                      <i
                        class="icon-close"
                        @mouseenter="toggleTooltipClose"
                        @mouseleave="toggleTooltipClose"
                        @click="closePopupAdjustColumn"
                      >
                        <DxTooltip
                          :visible="tooltipClose"
                          :hide-on-outside-click="false"
                          target=".adjust-column-main .icon-close"
                          position="bottom"
                        >
                          Đóng
                        </DxTooltip>
                      </i>
                    </div>
                  </div>
                </div>
              </div>
              <div>
                <div class="w-full search-column">
                  <div class="flex items-center w-full h-full">
                    <div class="ms-input flex w-full">
                      <div class="flex m-l-8 items-center justify-center">
                        <div class="mi-search"></div>
                      </div>
                      <input
                        type="text"
                        class="ms-input-item"
                        maxlength="255"
                        placeholder="Tìm kiếm"
                      />
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </header>
          <main>
            <div class="flex-column list-group-column">
              <div
                class="list-group-item"
                v-for="(column, index) in columns"
                :key="index"
              >
                <div class="flex items-center justify-between">
                  <label class="container ms-checkbox flex cursor-pointer">
                    <input type="checkbox" class="ms-checkbox--input" />
                    <span
                      class="checkmark"
                      v-bind:class="
                        column.show == true
                          ? 'icon-checkbox-primary'
                          : 'icon-checkbox-uncheck'
                      "
                    ></span>
                    <span class="con-slot-label"
                      ><div>{{ column.Name }}</div>
                    </span>
                  </label>
                  <div class="icon-drag icon-hidden"></div>
                </div>
              </div>
            </div>
          </main>
          <footer>
            <div class="command-wrap">
              <button class="ms-button ms-button-secondary btn-get-default">
                <span class="ms-button-text ms-button--text"
                  >Lấy lại mặc định</span
                >
              </button>
              <button class="ms-button ms-button-primary btn-apply">
                <span class="ms-button-text ms-button--text">Áp dụng</span>
              </button>
            </div>
          </footer>
        </div>
      </div>
    </div>
  </div>
  <InsertUser
    v-if="isShowInsertUser"
    @showInsertUser="showPopupInsertUser"
    @reloadData="reloadData"
    @showToast="showToast"
  />
  <UpdateUser
    v-if="isShowUpdateUser"
    @showUpdateUser="showPopupUpdateUser"
    :userUpdate="userSelected"
    @reloadData="reloadData"
    @showToast="showToast"
  />
  <DeleteUser
    v-if="isShowDeleteUser"
    @showDeleteUser="showPopupDeleteUser"
    :userDelete="userSelected"
    @reloadData="reloadData"
    @showToast="showToast"
  />
  <PreviewUser
    v-if="isShowPreviewUser"
    @showPreviewUser="showPopupPreviewUer"
    :userSelected="userSelected"
    @reloadData="reloadData"
    :userStatus="userStatus"
  />
  <DxToast
    :minWidth="500"
    v-model:visible="isShowToast"
    v-model:message="messageToast"
    v-model:type="typeToast"
    :width="auto"
  >
  </DxToast>
</template>
<script>
import { createAvatar, createBgAvatar } from "../../common/avatar";
import "devextreme/dist/css/dx.light.css";
import { DxSelectBox } from "devextreme-vue/select-box";
import { DxTooltip } from "devextreme-vue/tooltip";
import { DxToast } from "devextreme-vue/toast";
import UpdateUser from "../Popup/Dialog/UserUpdate";
import DeleteUser from "../Popup/Message/UserRemove";
import PreviewUser from "../Popup/Dialog/UserDetail";
import InsertUser from "../Popup/Dialog/UserAdd.vue";
import ThePaging from "../../components/base/ThePaging";
import {
  DxDataGrid,
  DxScrolling,
  DxColumn,
  DxSorting,
  DxPager,
  DxLoadPanel,
  DxPaging,
} from "devextreme-vue/data-grid";
import { status, pageDefault } from "../../enum/enum.js";
import UserApi from "../../api/UserApi.js";
import RoleApi from "../../api/RoleApi.js";
import { columns } from "../../common/data.js";
export default {
  name: "UserGrid",
  components: {
    DxSelectBox,
    DxDataGrid,
    DxScrolling,
    DxColumn,
    DxSorting,
    DxPager,
    DxTooltip,
    UpdateUser,
    DeleteUser,
    PreviewUser,
    InsertUser,
    ThePaging,
    DxLoadPanel,
    UserApi,
    DxPaging,
    DxToast,
  },
  async created() {
    /**
     * Gọi phương thức load dữ liệu
     */
    await this.loadData();
    /**
     * Gọi phương thức load dữ liệu Role
     */
    await this.loadRole();
  },
  watch: {
    //Khi thay đổi pageNumber => load lại dữ liệu
    pageNumber() {
      this.loadData();
    },
    //Khi thay đổi pageSize => số trang được gán bằng giá trị mặc định và load lại dữ liệu
    pageSize() {
      //gán lại giá trị cho pageNumber
      this.pageNumber = pageDefault.pageNumber;
      //load lại dữ liệu
      this.loadData();
    },
    //Khi biến reload Data thay đổi => biến bằng true => load lại data
    isReloadData() {
      if (this.isReloadData === true) {
        this.loadData();
        //set lại giá trị cho biến reload lại data là false
        if (this.timer) {
          clearTimeout(this.timer);
          this.timer = null;
        }
        this.timer = setTimeout(() => {
          this.isReloadData = false;
        }, 200);
      }
    },
    //Khi thay đổi biển tìm kiếm dữ liệu
    txtSearch() {
      this.loadData();
    },
    roleSearch() {
      this.loadData();
    },
  },
  data() {
    return {
      /**
       * Data component User Grid
       * Author: Dương Minh Việt (29/7/2022)
       */
      //Danh sách chứa các tên các cột
      columns: columns,
      //Biến ẩn hiện tooltip nút x (close)
      tooltipClose: false,
      //mảng check column được ẩn hiện
      checkColumn: [],
      //Đối tượng User được chọn để thực hiện các chức năng xem, sửa, xoá
      userSelected: {},
      //Danh sách các vai trò
      roles: [],
      //Danh sách người dùng
      users: [],
      //Đối tượng xác định trạng thái người dùng
      status: status,
      //Biến xác định ẩn hiện popup update user
      isShowUpdateUser: false,
      //Biến xác định ẩn hiện popup delete user
      isShowDeleteUser: false,
      //Biến xác định ẩn hiện popup preview user
      isShowPreviewUser: false,
      //Biến xác định ẩn hiện Popup InsertUser
      isShowInsertUser: false,
      //Biến xác định ẩn hiện popup custom column
      isShowAdjustColumn: false,
      //Biến xác định có load lại dữ liệu hay không
      isReloadData: false,
      //Số bản ghi trên trang - ban đầu được gán bằng giá trị mặc định
      pageSize: pageDefault.pageSize,
      //Số trang - ban đầu được gán bằng giá trị mặc định
      pageNumber: pageDefault.pageNumber,
      //Từ khoá tìm kiếm
      txtSearch: "",
      //ID role cần để tìm kiếm
      roleSearch: "",
      //Tổng số bản ghi
      totalRecord: 0,
      //Tổng số trang
      totalPage: 1,
      //Trang bắt đầu
      pageStart: 1,
      //Trang kết thúc
      pageEnd: 1,
      //Kiểu thông báo
      typeToast: "",
      //Nội dung thông báo
      messageToast: "",
      //Biến xác định ẩn hiện Toast
      isShowToast: false,
    };
  },
  methods: {
    //Phương thức ẩn hiện tooltip nút x (close)
    toggleTooltipClose() {
      this.tooltipClose = !this.tooltipClose;
    },
    //Xác định trạng thái người dùng
    userStatus(state) {
      if (state == status.notActive) return "#02a3ec;";
      if (state == status.wait) return "#f39619;";
      if (state === status.active) return "rgb(102, 209, 129);";
      if (state == status.inactive) return "rgb(97, 103, 103);";
    },
    //Tạo tên avatar
    avatar(name) {
      return createAvatar(name);
    },
    //Tạo màu cho background avatar
    bgAvatar(name) {
      return createBgAvatar(this.avatar(name));
    },
    //Phương thức ẩn hiện popup update user
    showPopupUpdateUser(isShow) {
      this.isShowUpdateUser = isShow;
    },
    //Phương thức ẩn hiện popup delete user
    showPopupDeleteUser(isShow) {
      this.isShowDeleteUser = isShow;
    },
    //Phương thức ẩn hiện popup preview user
    showPopupPreviewUer(isShow) {
      this.isShowPreviewUser = isShow;
    },
    //Sự kiện click vào nút sửa hiện lên popup update user
    showPopupUpdate(data) {
      //Gọi phương thức ẩn hiện popup update
      this.showPopupUpdateUser(true);
      //Gán đối tượng được chọn để sửa  = data
      this.userSelected = data.data;
    },
    //Sự kiện click vào nút sửa hiện lên popup update user
    showPopupDelete(data) {
      //Gọi phương thức ẩn hiện popup delete user
      this.showPopupDeleteUser(true);
      //Gán đối tượng được chọn để sửa = data
      this.userSelected = data.data;
    },
    //Sự kiệN click vào một dòng => xem dữ liệu user
    shopPopupPreview(data) {
      //Gọi phương thức ẩn hiện popup update user
      this.showPopupPreviewUer(true);
      //Gán đối tượng dòng được chọn để xem trước =  data
      this.userSelected = data.data;
      //Xử lí truyền danh sách vai trò của nhân viên
      //Lấy ra mảng danh sách tên vai trò
      let arrRoleName = this.userSelected.roleName.split(",");
      this.userSelected.userRoles = arrRoleName.map((item) => {
        return {
          roleDescription: item,
        };
      });
    },
    //Phương thức ẩn hiển popup Insert User
    showPopupInsertUser(isShow) {
      this.isShowInsertUser = isShow;
    },
    //Sự kiện click vào nút Thêm mới => mở lên form popup User
    showPopupInsert() {
      this.showPopupInsertUser(true);
    },
    //Phương thức load dữ liệu data
    async loadData() {
      var me = this;
      //Làm rỗng danh sách User
      me.users = [];
      //Gọi APi lấy ra dữ liệu danh sách User
      await UserApi.filter(
        me.pageSize,
        me.pageNumber,
        me.txtSearch,
        me.roleSearch
      )
        .then((res) => {
          if (res.data && res.data.data != null) {
            //gán danh sách user = res
            me.users = res.data.data;
            //gán giá trị cho tổng số bản ghi => để truyền xuống paging
            me.totalRecord = res.data.totalRecord;
            //gán giá trị cho trang bắt đầu => truyền xuống paging
            me.pageStart = res.data.pageStart;
            //gán giá trị cho trang kết thúc => truyền xuống paging
            me.pageEnd = res.data.pageEnd;
            //gán giá trị cho tổng số trang => truyền xuống paging
            me.totalPage = res.data.totalPage;
          }
        })
        .catch(() => {
          //Toast lên thông báo lỗi cho người dùng
          me.showToast("warning", "Có lỗi xảy ra vui lòng liên hệ MISA", true);
        });
    },
    //Phương thức xử lí khi thay đổi pageNumber
    changePageNumber(pageNumber) {
      //gán giá trị cho pageNumber bằng giá trị nhận được component con ThePaging
      this.pageNumber = pageNumber;
    },
    //Phương thức xử lí khi thay đổi pageSize
    changePageSize(pageSize) {
      //gán giá trị cho pageSize bằng giá trị nhận được component con ThePaging
      this.pageSize = pageSize;
    },
    //Phương thức tìm kiếm nhân viên
    filterUser() {
      if (this.timer) {
        clearTimeout(this.timer);
        this.timer = null;
      }
      this.timer = setTimeout(() => {
        this.txtSearch = this.$refs.txtSearch.value;
      }, 500);
    },
    //Phương thức gọi dữ liệu Role
    async loadRole() {
      var me = this;
      //Gọi API lấy về dữ liệu
      await RoleApi.getAll().then((res) => {
        //gán giá trị cho mảng roles
        me.roles = res.data;
        //thêm phần từ vai trò: tất cả vào mảng
        let item = {
          roleID: "",
          roleDescription: "Tất cả",
        };
        me.roles.unshift(item);
      });
    },
    //Phước thức xác định có load data hay không
    reloadData(isLoad) {
      this.isReloadData = isLoad;
    },
    //Phương thức xác định thông báo toast
    showToast(type, message, isShow) {
      this.typeToast = type;
      this.messageToast = message;
      this.isShowToast = isShow;
    },
    //Hiện popup chỉnh cột
    showPopupAdjustcolumn() {
      this.isShowAdjustColumn = !this.isShowAdjustColumn;
    },
    //Ẩn đi popup chỉnh cột
    closePopupAdjustColumn() {
      this.isShowAdjustColumn = false;
    },
    //Sự kiện khi chọn vai trò để lọc
    valueChangedRole(e) {
      this.roleSearch = e.value;
    },
  },
};
</script>
<style>
@import url("../../css/user-grid.css");
</style>
