<template>
  <div class="main-nav">
    <div class="nav-container flex justify-between">
      <div class="flex flex-1">
        <div class="box-app-menu"></div>
        <a href="/process" class="nav-logo flex-c-m" style="margin-right: 34px"
          >Quy trình</a
        >
        <div class="top-menu-container flex-1">
          <div class="flex-m">
            <div
              v-for="(item, index) in items"
              :key="index"
              class="menu-item"
              style="height: 100%"
              @mouseover="onOver(item)"
              @mouseleave="onLeave(item)"
              v-bind:class="item.active == true ? 'active' : ''"
            >
              <div class="menu-item-name">{{ item.name }}</div>
              <div
                :class="{
                  'menu-item-active': item.hover == true || item.active == true,
                }"
              ></div>
            </div>

            <div class="btn-run-process">
              <button
                class="ms-button ms-button-primary includeIcon"
                style="
                  height: 36px !important;
                  width: auto !important;
                  padding: 0px 16px 0px 12px;
                  min-width: 80px !important;
                "
              >
                <div
                  class="ms-button--icon flex-c-m"
                  style="order: 0; margin-right: 4px; margin-left: 0px"
                >
                  <i class="icon-run-process"></i>
                </div>
                <span class="ms-button-text">Chạy quy trình</span>
              </button>
            </div>
          </div>
        </div>
      </div>
      <div class="flex-m">
        <div class="flex-m" style="height: 100%">
          <div class="flex-m global-search">
            <div class="mi-search"></div>
            <input
              type="text"
              placeholder="Nhập ID, tiêu đề lượt chạy"
              class="flex-1"
            />
          </div>
          <div class="ms-notification">
            <div class="notification-count">12</div>
          </div>
          <div class="right-action user">
            <div style="margin: auto; position: relative">
              <div
                class="user-avatar"
                v-bind:style="
                  'background-color:' + bgAvatar(avatar('Dương Minh Việt'))
                "
              >
                <span> {{ avatar("Dương Minh Việt") }} </span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { createAvatar, createBgAvatar } from "../common/avatar";
export default {
  name: "TheHeader",
  data() {
    return {
      /**
       * Data của component The Header
       * Author: Dương Minh Việt (29/7/2022)
       */

      //Danh sách menu item
      items: [
        {
          name: "Lượt chạy",
          hover: false,
          route: "process/excute",
          active: false,
        },
        {
          name: "Thiết kế quy trình",
          hover: false,
          route: "process/definition",
          active: false,
        },
        {
          name: "Báo cáo",
          hover: false,
          route: "process/report",
          active: false,
        },
        {
          name: "Thiết lập",
          hover: false,
          route: "process/setting",
          active: true,
        },
      ],

      //Biến xác định xem css khi hover qua menu item
      isItemHover: false,
    };
  },
  methods: {
    //Phương thức khi hover qua item
    onOver(item) {
      item.hover = true;
    },

    //Phương thức khi di chuột qua item
    onLeave(item) {
      item.hover = false;
    },
    //Tạo tên avatar
    avatar(name) {
      return createAvatar(name);
    },
    //Tạo background cho avatar
    bgAvatar(name) {
      return createBgAvatar(name);
    },
  },
};
</script>
<style>
.main-nav {
  position: relative;
  height: 56px;
  background: #fff;
  padding: 0 16px 0 0;
  border-bottom: 1px solid #e7e8e9;
}
.nav-container {
  height: 100%;
}
.box-app-menu {
  margin: auto 16px;
  height: 36px;
  width: 36px;
  background: url(https://testcdnamisapp.misa.vn/apps/process/img/icon-app-gray.eec2ff5c.svg)
    no-repeat center;
  cursor: pointer;
}
.box-app-menu:hover {
  border-radius: 50%;
  background-color: #f2f2f2;
}
.nav-logo {
  font-size: 18px;
  color: #000;
  padding: 0 10px 0 44px;
  position: relative;
  font-weight: 600;
}
.nav-logo::before {
  content: "";
  height: 32px;
  width: 32px;
  background: url(https://testcdnamisapp.misa.vn/apps/process/img/logo.fe78f7ca.svg);
  position: absolute;
  top: 12px;
  left: 0;
}
.top-menu-container {
  height: 100%;
  display: flex;
  justify-content: space-between;
}
.menu-item {
  margin-right: 36px;
  display: flex;
  flex-direction: column;
  cursor: pointer;
  justify-content: space-between;
}
.menu-item:hover {
  color: #02a3ec;
}

.menu-item-name {
  font-weight: 500;
  margin-top: 20px;
}
.menu-item-name:hover {
  color: #02a3ec;
}
.menu-item-active {
  background-color: #02a3ec;
  height: 3px;
  width: 100%;
}
.active {
  color: #02a3ec;
}
.btn-run-process {
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}
.btn-run-process button {
  cursor: pointer;
}
.global-search {
  padding: 4px 8px;
  border: 1px solid var(--boder-color);
  margin-right: 8px;
  height: 36px;
  border-radius: 4px;
  width: 240px;
}
.mi-search {
  width: 24px;
  height: 24px;
  -webkit-mask-image: url("../assets/Icon.de5bb0db.svg");
  mask-image: url("../assets/Icon.de5bb0db.svg");
  -webkit-mask-position: -192px 0;
  mask-position: -192px 0;
  background-color: #9fa4b4 !important;
}
.global-search input {
  border: none;
  outline: none;
  margin-right: 24px;
  margin-left: 4px;
}
.ms-notification {
  border-radius: 50%;
  cursor: pointer;
  width: 32px;
  margin-right: 24px;
  height: 32px;
  margin-top: 2px;
  background: url("../assets/icon-notification-dark-thick.svg") no-repeat center;
  position: relative;
}
.right-action {
  height: 100%;
  width: 48px;
}
.right-action:hover {
  cursor: pointer;
  background-color: #f5f5f5;
}
.right-action:focus {
  background-color: #e8f5f7;
}
.right-action.user {
  text-align: center;
  align-items: center;
  display: flex;
}
.right-action .user-avatar {
  min-width: 32px;
  height: 32px;
  text-align: center;
  line-height: 32px;
  border-radius: 50%;
  margin: 0;
  padding: 0;
  font-size: 11px;
  color: #fff;
  overflow: hidden;
}
.right-action .user-avatar img {
  border-radius: 50%;
  height: 32px;
  width: 32px;
  display: block;
}
.notification-count {
  width: 20px;
  height: 20px;
  border-radius: 50%;
  position: absolute;
  background-color: #f44336;
  top: -4px;
  right: -4px;
  color: #fff;
  font-size: 10px;
  line-height: 20px;
  text-align: center;
}
</style>
