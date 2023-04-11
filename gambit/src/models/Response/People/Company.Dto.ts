import ContactDto from "../Contact/ContactDto";

class CompanyDto {
  Id: string;
  Name: string;
  Activity: string;
  Relation: string;
  Contact: ContactDto;

  constructor(id: string, name: string, act: string, rel: string, contact: ContactDto)
  {
    this.Id = id;
    this.Name = name
    this.Activity = act;
    this.Relation = rel;
    this.Contact = contact
  }
}

export default CompanyDto;