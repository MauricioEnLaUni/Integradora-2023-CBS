import AddressDto from "./AddressDto";

class ContactDto {
  addresses: Array<AddressDto> ;
  emails: Array<string>;
  phones: Array<string> ;

  constructor(addresses: Array<AddressDto>, email: Array<string>, phones: Array<string>)
  {
    this.addresses = addresses;
    this.emails = email;
    this.phones = phones;
  }
}

export default ContactDto;