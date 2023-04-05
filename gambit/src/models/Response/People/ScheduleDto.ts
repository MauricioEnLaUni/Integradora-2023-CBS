class ScheduleDto
{
  Id: string;
  Period: string;
  Location?: Address;

  constructor(id: string, period: string, location?: Address)
  {
    this.Id = id;
    this.Period = period;
    this.Location = location;
  }
}

export default ScheduleDto;