import { ObjectId } from "mongodb";
import mongoose, { Types } from "mongoose";

export class Entity {
  private Id: Types.ObjectId = new mongoose.Types.ObjectId();
  private Name: string;
  private Created: Date = new Date();
  private Closed?: Date;

  constructor(name: string, closes: Date | null) {
    this.Name = name;
    if (closes) this.Closed = closes;
  }

  protected setName(newName: string): void {
    this.Name = newName;
  }
};

class Address {
  private Street?: string;
  private Number?: string;
  private Colony?: string;
  private PostalCode?: string;
  private City?: string;
  private State?: string;
  private Country?: string;
  private Latitude?: string;
  private Longitude?: string;

  constructor(
    street?: string,
    number?: string,
    colony?: string,
    pc?: string,
    city?: string,
    state?: string,
    country?: string,
    lat?: string,
    log?: string
  ) {
    this.Street = street;
    this.Number = number;
    this.Colony = colony;
    this.PostalCode = pc;
    this.City = city;
    this.State = state;
    this.Country = country;
    this.Latitude = lat;
    this.Longitude = log;
  }
}

class Contact {
  private Address: Array<Address> = new Array<Address>();
  private Phones: Array<string> = new Array<string>();
  private Email: Array<string> = new Array<string>();
}

class Salary {
  private Id: Types.ObjectId = new mongoose.Types.ObjectId();
  private Created: Date = new Date();
  private Reductions: Array<Array<string>> = new Array<Array<string>>();
  private Rate: number;
  private Hours: number;
  private Closed?: Date;

  constructor(rate: number, hours: number, closes?: Date) {
    this.Rate = rate;
    this.Hours = hours;
    this.Closed = closes;
  }
}

class Job extends Entity {
  private SalaryHistory: Array<Salary> = new Array<Salary>();
  private Role: string;
  private Area: string;
  private Responsibilities: Array<string> = new Array<string>();

  constructor(role: string, area: string, name: string, created: Date | null) {
    super(name, created);
    this.Role = role;
    this.Area = area;
  }
}


export class Person extends Entity {
  private lastName: string;
  private Contact: Array<Contact> = new Array<Contact>();
  private Job: Array<Job> = new Array<Job>();

  constructor(name: string, closes: Date | null, last: string) {
    super(name, closes);
    this.lastName = last;
  }
};