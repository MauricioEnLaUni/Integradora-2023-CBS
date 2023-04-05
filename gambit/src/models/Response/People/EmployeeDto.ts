import JobDto from "./JobDto";
import ScheduleDto from "./ScheduleDto";

class EmployeeDto
{
  Id: string;
  Name: string;
  DOB: string;
  CURP: string;
  RFC: string;
  Charges: Array<JobDto>;
  ScheduleHistory: Array<ScheduleDto>;

  constructor(id: string, name: string, dob: string, curp: string, rfc: string, charges: Array<JobDto>, scheduleHistory: Array<ScheduleDto>)
  {
    this.Id = id;
    this.Name = name;
    this.DOB = dob;
    this.CURP = curp;
    this.RFC = rfc;
    this.Charges = charges;
    this.ScheduleHistory = scheduleHistory;
  }
}

export default EmployeeDto;