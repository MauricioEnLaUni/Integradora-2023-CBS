class AddressDto
{
  Street?: string;
  Number?: string;
  Colony?: string;
  PostalCode?: string;
  City?: string;
  State?: string;
  Country?: string;
  Coordinates?: Coordinates;

  constructor(street?: string, number?: string, colony?: string, postalCode?: string, city?: string, state?: string, country?: string, coordinates?: Coordinates)
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

class Coordinates
{
  Latitude: string;
  Longitude: string;

  constructor(lat: string, long: string)
  {
    this.Latitude = lat;
    this.Longitude = long;
  }
}

export default AddressDto;