<template>
  <div class="con-ms-popup popup-notification">
    <div class="ms-popup--background"></div>
    <div
      class="ms-popup flex"
      style="
        background: rgb(255, 255, 255);
        width: 415px;
        min-width: 500px;
        flex-direction: column;
      "
    >
      <div style="height: 53px">
        <header class="ms-popup--header" style="height: 53px; width: 500px">
          <div class="ms-popup--title">
            <h2>
              <span
                style="
                  letter-spacing: 0.576px;
                  font-size: 24px;
                  color: rgb(33, 33, 33);
                  font-weight: 500;
                "
                >Xoá người dùng</span
              >
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
      <div
        class="ms-popup--content"
        style="overflow: auto; height: calc(100% - 60px)"
      >
        <div>
          <span>Bạn có chắc chắn muốn xoá </span
          ><span class="bold"> {{ userDelete.userName }} </span
          ><span> khỏi ứng dụng AMIS quy trình không?</span>
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
            class="ms-button ms-button-danger"
            style="
              width: 70px !important;
              height: 36px !important;
              border-radius: 4px;
              padding: 0px 16px;
              min-width: 80px !important;
            "
            @click="btnDeleteOnClick"
          >
            <span class="ms-button-text ms-button--text">Xoá</span>
          </button>
        </div>
      </footer>
    </div>
  </div>
</template>
<script>
import { DxTooltip } from "devextreme-vue/tooltip";
import UserApi from "../../../api/UserApi.js";
export default {
  name: "DeleteUser",
  components: { DxTooltip },
  props: ["userDelete"],
  data() {
    return {
      //Biển ẩn hiện tooltip nút x (close)
      tooltipBtnClose: false,
    };
  },
  methods: {
    //Phương thức ẩn hiển tooltip nút x (close)
    toggleBtnClose() {
      this.tooltipBtnClose = !this.tooltipBtnClose;
    },
    //Sự kiện click vào nút x (close) để đóng popup
    closePopup() {
      this.$emit("showDeleteUser", false);
    },
    //Sự kiện click vào nút xoá
    async btnDeleteOnClick() {
      var me = this;
      //Gọi API xoá dữ liệu
      await UserApi.deleteAll(me.userDelete.userID)
        .then((res) => {
          //nếu xoá thành công
          if (res.data > 0) {
            //Emit cho component cha GridUser || PreviewUser = > Toast lên cho người dùng biết là đã xoá thành công
            me.$emit("showToast", "success", "Xoá thành công", true);
            //Emit cho component cha PreviewUser => Ẩn đi component
            me.$emit(
              "showToastPreview",
              "success",
              "Xoá thành công",
              true,
              false,
              true
            );
            //Emit cho component cha GridUser => để xác định load lại dữ liệu
            me.$emit("reloadData", true);
            //Ẩn đi popup
            me.$emit("showDeleteUser", false);
          } else {
            me.handleException();
          }
        })
        .catch(() => {
          me.handleException();
        });
    },

    //Phương thức toast lên khi có thông báo lỗi 500
    handleException() {
      //Emit cho component cha GridUsser = > Toast lên cho người dùng biết là đã xoá không thành công
      this.$emit(
        "showToast",
        "warning",
        "Có lỗi xảy ra vui lòng liên hệ MISA",
        true
      );
      //Emit cho component cha PreviewUser => Ẩn đi component
      this.$emit(
        "showToastPreview",
        "warning",
        "Có lỗi xảy ra vui lòng liên hệ MISA",
        true,
        false,
        true
      );
      //Ẩn đi popup
      this.$emit("showDeleteUser", false);
    },
  },
};
</script>
