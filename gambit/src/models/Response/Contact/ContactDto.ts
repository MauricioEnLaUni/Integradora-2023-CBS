import AddressDto from "./AddressDto";

class ContactDto {
  Addresses: Array<AddressDto> ;
  Emails: Array<string>;
  Phones: Array<string> ;

  constructor(addresses: Array<AddressDto>, email: Array<string>, phones: Array<string>)
  {
    this.Addresses = addresses;
    this.Emails = email;
    this.Phones = phones;
  }
}

export default ContactDto;