class ContactDto {
  Addresses: Array<Address> ;
  Emails: Array<string>;
  Phones: Array<string> ;

  constructor(addresses: Array<Address>, email: Array<string>, phones: Array<string>)
  {
    this.Addresses = addresses;
    this.Emails = email;
    this.Phones = phones;
  }
}