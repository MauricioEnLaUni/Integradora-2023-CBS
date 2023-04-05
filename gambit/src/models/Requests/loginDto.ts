class LoginDto
{
  Name: string;
  Password: string;

  constructor(name: string, password: string)
  {
    this.Name = name;
    this.Password = password;
  }
}