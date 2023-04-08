import TaskCondensed from "./TaskCondensed";

export default class ProjectCondensed
{
  id: string;
  name: string;
  ends: Date;
  last: TaskCondensed;
  client: string;

  constructor(id: string, name: string, ends: Date, last: TaskCondensed, client: string)
  {
    this.id = id;
    this.name = name;
    this.ends = ends;
    this.last = last;
    this.client = client;
  }
}