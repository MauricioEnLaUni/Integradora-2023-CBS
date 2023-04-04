import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class AddressDto {
  AddressDto(
      {this.street,
      this.number,
      this.colony,
      this.postalCode,
      this.city,
      this.state,
      this.country});

  String? street;
  String? number;
  String? colony;
  String? postalCode;
  String? city;
  String? state;
  String? country;

  factory AddressDto.fromJson(Map<String, dynamic> jsonString) {
    return AddressDto(
        street: jsonString['street'],
        number: jsonString['number'],
        colony: jsonString['colony'],
        postalCode: jsonString['postalCode'],
        city: jsonString['city'],
        state: jsonString['state'],
        country: jsonString['country']);
  }

  Map<String, dynamic> toJson() => {
        'street': street,
        'number': number,
        'colony': colony,
        'postalCode': postalCode,
        'city': city,
        'state': state,
        'country': country
      };
}
