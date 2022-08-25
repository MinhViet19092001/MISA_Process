import { createApp } from "vue";
import App from "./App.vue";
import User from "./views/Grid/GridUser.vue";
import { createRouter, createWebHistory } from "vue-router";

const role = {
  template: "<div>Vai trò</div>",
};
const mutilUser = {
  template: "<div>Nhóm người dùng</div>",
};
const shareLink = {
  template: "<div>Kết nối</div>",
};

const misaroutes = [
  { path: "/", redirect: "/process/setting/user" },
  { path: "/process", redirect: "/process/setting/user" },
  { path: "/process/setting/user", component: User },
  { path: "/process/setting/role", component: role },
  { path: "/process/setting/multiuser", component: mutilUser },
  { path: "/process/setting/sharelink", component: shareLink },
];

const router = createRouter({
  history: createWebHistory(),
  routes: misaroutes,
  linkActiveClass: "tab-actived",
});

createApp(App).use(router).mount("#app");
