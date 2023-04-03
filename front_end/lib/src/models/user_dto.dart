import 'dart:typed_data';

class NewUserDto {
  String name;
  String password;
  String email;

  NewUserDto(this.name, this.password, this.email);
}

class LoginDto {
  String name;
  String password;

  LoginDto(this.name, this.password);
}

class LoginSuccessDto {
  String name;
  DateTime createdAt;
  List roles;
  List email;
  Uint8List avatar;

  LoginSuccessDto(
      this.name, this.createdAt, this.roles, this.email, this.avatar);
}
