//Sự kiện khi blur ra khỏi input
export function inputOnblur($event, fieldName) {
  //xỬ lí các ô input bình thường
  if ($event.element == null) {
    const target = $event.target;
    if (!target.value) {
      //Thêm thuộc tính báo đỏ vào các ô input
      target.setAttribute("required", true);
      //Thêm title vào các ô tương ứng
      target.setAttribute(
        "title",
        fieldName + " người dùng không được để trống."
      );
    } else {
      //Bỏ thuộc tính báo đỏ vào các ô input
      target.removeAttribute("required");
      target.removeAttribute("title");
    }
  } else {
    const target = $event.event.target;
    if (!target.value) {
      $event.element.setAttribute("required", true);
      $event.element.setAttribute("title", fieldName + " không được để trống.");
    } else {
      //Bỏ thuộc tính báo đỏ vào các ô input
      $event.element.removeAttribute("required");
      $event.element.removeAttribute("title");
    }
  }
}
