<template>
  <div class="main-container">
    <TheHeader />
    <TheContent />
  </div>
</template>

<style>
@import url("./css/base/style.css");
</style>
<script>
import TheHeader from "./components/TheHeader.vue";
import TheContent from "./components/TheContent.vue";
export default {
  name: "App",
  components: {
    TheHeader,
    TheContent,
  },
};
</script>
<style></style>
