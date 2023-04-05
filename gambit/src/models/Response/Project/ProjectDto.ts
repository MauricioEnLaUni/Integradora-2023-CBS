class ProjectDto {
  Id: string;
  Name: string;
  Starts: Date;
  Ends: Date;
  LastTask: FTasksDto;

  constructor(id: string, name: string, starts: Date, ends: Date, lastTask: FTasksDto)
  {
    this.Id = id;
    this.Name = name;
    this.Starts = starts;
    this.Ends = ends;
    this.LastTask = lastTask;
  }
}