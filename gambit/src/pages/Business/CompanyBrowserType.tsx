export default class CompanyBrowserDto {
  id: string;
  name: string;
  activity: string;
  relation: string;

  constructor(id: string, name: string, activity: string, relation: string)
  {
    this.id = id;
    this.name = name;
    this.activity = activity;
    this.relation = relation;
  }
}