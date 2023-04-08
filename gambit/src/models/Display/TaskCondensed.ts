export default class TaskCondensed
{
  id: string;
  name: string;
  ends: Date;
  current: string;
  overseer: string;

  constructor(id: string, name: string, ends: Date, current: string, overseer: string)
  {
    this.id = id;
    this.name = name;
    this.ends = ends;
    this.current = current;
    this.overseer = overseer;
  }
}