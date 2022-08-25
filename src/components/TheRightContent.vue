<template>
  <div>
    <router-view></router-view>
  </div>
</template>
<script>
export default {
  name: "TheRightContent",
  components: {},
  data() {
    return {
      //Biến xác định ẩn hiện Popup InsertUser
      isShowInsertUser: false,
    };
  },
  methods: {},
};
</script>
<style>
.body-custom {
  width: calc(100vw - 194px);
  overflow: auto;
}
.content-header {
  padding-bottom: 16px !important;
}
</style>
