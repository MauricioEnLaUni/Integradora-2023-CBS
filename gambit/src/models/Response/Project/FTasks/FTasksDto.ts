class FTasksDto
{
  Id: string;
  Name: string;
  StartDate: Date;
  Complete: boolean;
  Ends: Date;
  Parent: string;
  Subtasks: FTasksDto;
  EmployeesAssigned: Array<string>;
  Material: Array<string>;
  Address: Address;

  constructor(id: string, name: string, start: Date, complete: boolean, ends: Date, parent: string, subtask: FTasksDto, emp: Array<string>, mat: Array<string>, add: Address)
  {
    this.Id = id;
    this.Name = name;
    this.Complete = complete;
    this.StartDate = start;
    this.Ends = ends;
    this.Parent = parent;
    this.Subtasks = subtask;
    this.EmployeesAssigned = emp;
    this.Material = mat;
    this.Address = add;
  }
}