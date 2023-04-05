import SalaryDto from "./SalaryDto";

class JobDto
{
  Name: string;
  InternalKey: string;
  SalaryHistory: Array<SalaryDto>;
  Role: string;
  Active: boolean;

  constructor(name: string, internal: string, salary: Array<SalaryDto>, role: string, active: boolean)
  {
    this.Name = name;
    this.InternalKey = internal;
    this.SalaryHistory = salary;
    this.Role = role;
    this.Active = active;
  }
}

export default JobDto;