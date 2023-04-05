class LoginDto
{
  Name: string;
  Password: string;
  Email: string;
  Owner: string;

  constructor(name: string, password: string, email: string, owner: string)
  {
    this.Name = name;
    this.Password = password;
    this.Email = email;
    this.Owner = owner;
  }
}