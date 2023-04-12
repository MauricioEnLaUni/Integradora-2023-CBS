import ContactDto from "../Contact/ContactDto";

class CompanyDto {
  id: string;
  name: string;
  activity: string;
  relation: string;
  contact: ContactDto;

  constructor(id: string, name: string, act: string, rel: string, contact: ContactDto)
  {
    this.id = id;
    this.name = name
    this.activity = act;
    this.relation = rel;
    this.contact = contact
  }
}

export default CompanyDto;