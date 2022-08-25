//Biểu thức chính quy UserCode
export const regexUserCode = "MS-[0-9]*";

//Biểu thức chính quy Email
export const regexEmail = "[a-zA-Z0-9_.+]@[a-zA-Z0-9.]+";

//Thông báo khi validate dữ liệu các trường có giá trị null
export function valueNull(fieldName, index) {
  return (
    fieldName + " người dùng thứ " + (index + 1) + " không được phép để trống."
  );
}

//Thông báo khi validate dữ liệu các trường có giá trị bị trùng
export function valueDuplicate(fieldName, index) {
  return (
    fieldName + " người dùng thứ " + (index + 1) + " đã tồn tại trong hệ thống."
  );
}
//Thông báo khi validate dữ liệu các trường có giá trị không đúng với biểu thức chính quy
export function valueRegex(fieldName, index) {
  if (fieldName == "Mã") {
    return (
      fieldName +
      " người dùng thứ " +
      (index + 1) +
      " không đúng với định dạng MS-"
    );
  }
  if (fieldName == "Email") {
    return fieldName + " người dùng thứ " + (index + 1) + " không hợp lệ.";
  }
}

//giá trị title của input có giá trị null
export function titleNull(fieldName) {
  if (fieldName == "Phòng ban" || fieldName == "Vị trí") {
    return fieldName + " không được để trống.";
  }
  return fieldName + " người dùng không được để trống.";
}

export function titleCheckRegex(fieldName) {
  if (fieldName == "Mã") {
    return fieldName + " người dùng không đúng với định dạng MS-";
  }
  if (fieldName == "Email") {
    return fieldName + " người dùng không hợp lệ.";
  }
}

export function titleCheckCode(fieldName, index) {
  return fieldName + " người dùng thứ " + index + " đã tồn tại trong hệ thống.";
}
