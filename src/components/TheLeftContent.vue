<template>
  <div
    class="flex-column"
    style="
      padding-top: 16px;
      height: 100%;
      min-width: 200px;
      background-color: #fff;
    "
  >
    <router-link
      :to="{ path: '/' + tab.route }"
      v-for="(tab, index) in tabs"
      :key="index"
    >
      <div class="setting-tab-item flex-m">
        <div class="icon-tab" :class="tab.icon"></div>
        <div class="name-tab">{{ tab.name }}</div>
      </div>
    </router-link>
  </div>
</template>
<script>
export default {
  name: "TheLeftContent",
  data() {
    return {
      /**
       * Data component TheLeftContent
       * Author: Dương Minh Việt (29/7/2022)
       */

      //Danh sách chứa các tab item
      tabs: [
        {
          index: 1,
          name: "Người dùng",
          icon: "icon-user",
          route: "process/setting/user",
        },
        {
          index: 2,
          name: "Vai trò",
          icon: "icon-protect",
          route: "process/setting/role",
        },
        {
          index: 3,
          name: "Nhóm người dùng",
          icon: "icon-multiple-user",
          route: "process/setting/multiuser",
        },
        {
          index: 4,
          name: "Kết nối",
          icon: "icon-share-link",
          route: "process/setting/sharelink",
        },
      ],
      //Biến xác định xem tab nào được chọn
      tabActive: {
        typeof: Number,
        default: 1,
      },
    };
  },
  methods: {},
};
</script>
<style>
.setting-tab-item {
  height: 44px;
  cursor: pointer;
  padding-left: 24px;
}
.setting-tab-item .name-tab {
  line-height: 44px;
}
.tab-actived {
  background-color: #f3fbfd;
}
.tab-actived .name-tab {
  color: #02a3ec !important;
}
.tab-actived .icon-tab {
  background-color: #02a3ec !important;
}
</style>
