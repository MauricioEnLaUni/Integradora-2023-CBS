class ProjectDto {
  id: string;
  name: string;
  starts: Date;
  ends: Date;
  lastTask?: FTasksDto;

  constructor(id: string, name: string, starts: Date, ends: Date, lastTask?: FTasksDto)
  {
    this.id = id;
    this.name = name;
    this.starts = starts;
    this.ends = ends;
    this.lastTask = lastTask;
  }
}