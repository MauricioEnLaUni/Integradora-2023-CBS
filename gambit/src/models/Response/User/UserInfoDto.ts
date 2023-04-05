class UserInfoDto {
  Name: string;
  CreatedAt: Date;
  Owner: string;
  Roles: Array<string>;
  Email: Array<string>;
  Avatar: ArrayBuffer;

  constructor(name: string, createdAt: Date, owner: string, roles: Array<string>, email: Array<string>, avatar: ArrayBuffer)
  {
    this.Name = name;
    this.CreatedAt = createdAt;
    this.Owner = owner;
    this.Roles = roles;
    this.Email = email;
    this.Avatar = avatar;
  }
}