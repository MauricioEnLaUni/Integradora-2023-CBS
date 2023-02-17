export class Entity {
  constructor(
    readonly id: string,
    protected name: string,
    readonly created: Date,
  ) { }

  private setName(newName: string) {
    this.name = newName;
  }
  private getName() {
    return this.name;
  }
}

export class LocatedEntity extends Entity {
  constructor(
    ,
    readonly address: Array<Location>
  ) {
    super(args);
  }
}
/**
 * Sets a template for physical sites.
 */
export class Site {
  constructor(
    readonly address: Location,
    private departments: Array<Department>,
    private owner?: string,
    private rent?: number,
  ) { }
}

export class Department {
  constructor(
    private name: string,
    readonly address: Location,
    private head: Employee,
    readonly created: Date,
    private projects: Array<Project>,
    private employees: Array<Employee>,
    readonly disbanded?: Date,
  ) { }
}

export class Employee {

}

export class Project {

}