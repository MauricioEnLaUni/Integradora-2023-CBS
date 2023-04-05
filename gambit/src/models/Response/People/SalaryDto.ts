class SalaryDto
{
  Reductions: Record<string, number>;
  HourlyRate: number;
  Period: string;
  Due: string;
  HoursWeeklyCap?: number;
  Closed: Date;

  constructor(red: Record<string, number>, hourlyRate: number, period: string, due: string, hoursWeeklyCap: number, closed: Date)
  {
    this.Reductions = red;
    this.HourlyRate = hourlyRate;
    this.Period = period;
    this.Due = due;
    this.HoursWeeklyCap = hoursWeeklyCap;
    this.Closed = closed;
  }
}

export default SalaryDto;