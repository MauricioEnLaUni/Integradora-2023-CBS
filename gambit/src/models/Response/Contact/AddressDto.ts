class Address
{
  Street: string;
  Number: string;
  Colony: string;
  PostalCode: string;
  City: string;
  State: string;
  Country: string;
  Coordinates: string;

  constructor(street: string, number: string, colony: string, postalCode: string, city: string, state: string, country: string, coordinates: string)
  {
    this.Street = street;
    this.Number = number;
    this.Colony = colony;
    this.PostalCode = postalCode;
    this.City = city;
    this.State = state;
    this.Country = country;
    this.Coordinates = coordinates;
  }
}