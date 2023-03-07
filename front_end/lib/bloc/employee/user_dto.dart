class NewUserDto {
  String name;
  String password;
  String email;

  NewUserDto(
    this.name,
    this.password,
    this.email
  );
}

class LoginDto {
  String name;
  String password;

  LoginDto(
    this.name,
    this.password
  );
}