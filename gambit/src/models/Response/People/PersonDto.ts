import EmployeeDto from "./EmployeeDto";

class PersonDto
{
  Id: string;
  Name: string;
  Contact: ContactDto;
  Employee: EmployeeDto;

  constructor(id: string, name: string, contact: ContactDto, employee: EmployeeDto)
  {
    this.Id = id;
    this.Name = name;
    this.Contact = contact;
    this.Employee = employee;
  }
}

export default PersonDto;