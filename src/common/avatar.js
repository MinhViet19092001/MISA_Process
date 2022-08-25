import { colorCode } from "../enum/enum";
//Tạo tên avatar
export function createAvatar(UserName) {
  //Cắt chuỗi tên
  let arrayName = UserName.split(" ");
  //Lấy ra họ
  let familyName = arrayName[0];
  //lấy ra tên người dùng
  let name = arrayName[arrayName.length - 1];
  return familyName[0] + name[0];
}

//Tạo màu cho background avatar
export function createBgAvatar(AvatarName) {
  //Chuyển tên avt thành dạng mã ascii
  let ascii = AvatarName.charCodeAt(0) + AvatarName.charCodeAt(1);
  //Chia lấy dư mã ascii cho 5 (có 5 màu)
  var color = ascii % 5;
  if (color == colorCode.lime) return "#00FF00";
  if (color == colorCode.aqua) return "#00FFFF";
  if (color == colorCode.yellow) return "#FFFF00";
  if (color == colorCode.fuchsia) return "#FF00FF";
  if (color == colorCode.orange) return "#FFCC00";
}
