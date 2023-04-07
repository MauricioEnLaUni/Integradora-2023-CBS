import EmployeeDto from "./EmployeeDto";
import ContactDto from "../Contact/ContactDto";

class PersonDto
{
  Id: string;
  Name: string;
  Contact: ContactDto;
  Relation: string;
  Employee?: EmployeeDto;

  constructor(id: string, name: string, contact: ContactDto, relation: string, employee?: EmployeeDto)
  {
    this.Id = id;
    this.Name = name;
    this.Contact = contact;
    this.Relation = relation;
    this.Employee = employee;
  }
}

export default PersonDto;