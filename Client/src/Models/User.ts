export interface Entity {
  id: string;
}

export interface Credentials extends Entity {
  usr: string;
  pwd: string;
}