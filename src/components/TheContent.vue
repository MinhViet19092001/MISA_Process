<template>
  <div class="main-content flex-column">
    <div class="main-view flex-column">
      <div class="flex" style="height: 100%; margin-left: -6px">
        <TheLeftContent />
        <TheRightContent />
      </div>
    </div>
  </div>
</template>
<script>
import TheLeftContent from "./TheLeftContent";
import TheRightContent from "./TheRightContent.vue";

export default {
  name: "TheContent",
  components: { TheLeftContent, TheRightContent },
};
</script>
<style>
.main-content {
  transition: padding 0.2s;
  height: calc(100vh - 56px);
  background-color: #f5f8f9;
}
.main-view {
  height: 100%;
  min-width: 500px;
  min-height: 500px;
}
</style>
