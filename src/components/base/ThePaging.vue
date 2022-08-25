<template>
  <div class="paging" id="paging">
    <div class="grid-navigation">
      <div class="page-total flex">
        Tổng số bản ghi: <b style="padding: 0px 6px">{{ totalRecord }}</b>
      </div>
      <div class="page-size-selector flex items-center">
        <div style="margin-right: 8px !important">Số bản ghi/trang</div>
        <div style="position: relative">
          <button
            type="button"
            name="button"
            class="ms-component includeIcon ms-button ms-button-secondary items-center flex justify-between flex"
            style="
              width: 80px;
              height: 36px !important;
              border-radius: 4px;
              padding: 0px 8px 0px 12px;
              min-width: 80px !important;
              margin-right: 0 !important;
              position: relative;
            "
            @click="showMenuPageSize"
          >
            <div
              class="ms-button-icon items-center justify-center flex"
              style="order: 2; margin-right: 0px; margin-left: 4px"
            >
              <i size="16px" class="icon-chevron-down"></i>
            </div>
            <span class="ms-button-text ms-button--text">{{
              curPageSize
            }}</span>
          </button>
          <div class="ms-dropdown-list-pagesize" v-if="isShowMenuPageSize">
            <ul class="ms-dropdown-menu">
              <li
                class="ms-dropdown-item-pagesize"
                v-for="(pageSize, index) in pageSizes"
                :key="index"
                v-bind:class="{ selected: pageSize === curPageSize }"
                @click="changeCurPageSize(pageSize)"
              >
                <div class="flex justify-between items-center w-full">
                  <div>{{ pageSize }}</div>
                  <div
                    v-bind:class="{ 'icon-check': pageSize === curPageSize }"
                  ></div>
                </div>
              </li>
            </ul>
          </div>
        </div>

        <div class="page-info">
          <b>{{ pageStart }}</b> - <b>{{ pageEnd }}</b> bản ghi
        </div>
      </div>
      <div class="page-next-preview">
        <div
          class="ms-row"
          style="
            justify-content: center;
            display: flex;
            width: 100%;
            line-height: 35px;
            text-indent: 0px;
            padding: 0px;
            margin: 0 !important;
          "
        >
          <div
            style="
              justify-content: flex-end;
              display: flex;
              align-items: center;
              margin-left: 0%;
              width: 100%;
              margin: 0;
              float: left;
            "
          >
            <div style="height: 40px" class="con-ms-pagination w-full">
              <nav class="flex items-center justify-between">
                <div
                  class="ms-bottom-nav"
                  v-bind:class="[curPageNumber === 1 ? 'disable' : '']"
                  @click="prevPage"
                >
                  <div class="flex items-center justify-center btn-icon-1">
                    <div class="tooltip-container">
                      <div class="con-ms-tooltip">
                        <i
                          class="icon-chevron-left"
                          @mouseenter="toggleBtnPrev"
                          @mouseleave="toggleBtnPrev"
                        ></i>
                      </div>
                      <DxTooltip
                        :visible="tooltipBtnPrev"
                        :hide-on-outside-click="false"
                        target=".icon-chevron-left"
                        position="top"
                      >
                        <div>Quay lại</div>
                      </DxTooltip>
                    </div>
                  </div>
                </div>
                <div
                  class="ms-bottom-nav"
                  v-bind:class="[curPageNumber === totalPage ? 'disable' : '']"
                  @click="nextPage"
                >
                  <div class="flex items-center justify-center btn-icon-1">
                    <div class="con-ms-tooltip">
                      <i
                        class="icon-chevron-right"
                        @mouseenter="toggleBtnNext"
                        @mouseleave="toggleBtnNext"
                      ></i>
                    </div>
                    <DxTooltip
                      :visible="tooltipBtnNext"
                      :hide-on-outside-click="false"
                      target=".icon-chevron-right"
                      position="top"
                    >
                      <div>Tiếp tục</div>
                    </DxTooltip>
                  </div>
                </div>
              </nav>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { DxTooltip } from "devextreme-vue/tooltip";
import { pageDefault } from "../../enum/enum.js";
export default {
  name: "ThePaging",
  components: { DxTooltip },
  props: ["totalRecord", "pageStart", "pageEnd", "totalPage"],
  watch: {
    //Khi tổng số trang nhận từ component GridUser thay đổi
    totalPage() {
      //gán giá trị cho số trang lớn nhất
      this.pageMax = this.totalPage;
    },
    //Bắt sự kiện khi thay đổi số bản ghi trên trang => gán lại số trang = 1
    curPageSize() {
      this.curPageNumber = this.pageMin;
    },
  },
  data() {
    return {
      //Danh sách chứa giá trị số bản ghi trên trang
      pageSizes: [15, 25, 50, 100],
      //Biến ẩn hiện dropdown dan sách bản ghi trên trang
      isShowMenuPageSize: false,
      //Biến ẩn hiện tool tip cho nút quay lại trong paging
      tooltipBtnPrev: false,
      //Biển ẩn hiện tool tip cho nút tiếp tục trong paging
      tooltipBtnNext: false,
      //Số bản ghi trên 1 trang hiện tại - ban đầu được gán giá trị mặc định
      curPageSize: pageDefault.pageSize,
      //Số trang hiện tại - ban đầu được gán bằng giá trị mặc định
      curPageNumber: pageDefault.pageNumber,
      //số trang lớn nhất
      pageMax: {
        typeof: Number,
        default: 1,
      },
      //số trang nhỏ nhất
      pageMin: 1,
    };
  },
  methods: {
    //Phương thức ẩn hiện tooltip cho nút quay lại trong paging
    toggleBtnPrev() {
      this.tooltipBtnPrev = !this.tooltipBtnPrev;
    },
    //Phương thức ẩn hiện tooltip cho nút tiếp tục trong paging
    toggleBtnNext() {
      this.tooltipBtnNext = !this.tooltipBtnNext;
    },
    //Phương thức click vào nút next trang
    nextPage() {
      if (this.curPageNumber < this.pageMax) {
        //Tăng số trang lên 1
        this.curPageNumber = this.curPageNumber + 1;
        //Emit trang hiện tại cho component cha GridUser
        this.$emit("changePageNumber", this.curPageNumber);
      }
    },
    //Phương thức click vào nút prev trang
    prevPage() {
      if (this.curPageNumber > this.pageMin) {
        //Giảm số trang đi 1
        this.curPageNumber = this.curPageNumber - 1;
        //Emit trang hiện tại cho component cha GridUser
        this.$emit("changePageNumber", this.curPageNumber);
      }
    },
    //Phương thức ẩn hiện drop down menu pagesize
    showMenuPageSize() {
      this.isShowMenuPageSize = !this.isShowMenuPageSize;
    },
    //Phương thức lựa chọn pageSize
    changeCurPageSize(pageSize) {
      //Gán giá trị cho biến page size hiện tại
      this.curPageSize = pageSize;
      //Ẩn đi dropdown menu
      this.isShowMenuPageSize = false;
      //Emit số bản ghi trên trang hiện tại cho component cha GridUser
      this.$emit("changePageSize", pageSize);
    },
  },
};
</script>
<style>
@import url("../../css/base/pagination.css");
</style>
