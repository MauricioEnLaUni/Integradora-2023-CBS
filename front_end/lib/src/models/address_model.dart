import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class AddressDto {
  AddressDto(this.street, this.number, this.colony, this.postalCode, this.city, this.state, this.country);

  String street;
  String number;
  String colony;
  String postalCode;
  String city;
  String state;
  String country;

  AddressDto.fromJson(Map<String, dynamic> json)
    : street = json['street'],
      number = json['number'],
      colony = json['colony'],
      postalCode = json['postalCode'],
      city = json['city'],
      state = json['state'],
      country = json['country'];

  Map<String, dynamic> toJson() => {
    'street' : street,
    'number' : number,
    'colony' : colony,
    'postalCode' : postalCode,
    'city' : city,
    'state' : state,
    'country' : country
  };
}