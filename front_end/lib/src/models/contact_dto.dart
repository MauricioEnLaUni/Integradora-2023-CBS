import 'package:json_annotation/json_annotation.dart';

import 'package:front_end/src/models/address_model.dart';

@JsonSerializable()
class ContactDto {
  ContactDto(
      {required this.addresses, required this.phones, required this.emails});

  List<AddressDto> addresses;
  List<String> phones;
  List<String> emails;

  factory ContactDto.fromJson(Map<String, dynamic> jsonString) {
    List<dynamic> phones = jsonString['phones'] ?? [];
    List<dynamic> emails = jsonString['emails'] ?? [];
    var inner = jsonString['addresses'] as List<dynamic>;
    List<AddressDto> addresses =
        inner.map((i) => AddressDto.fromJson(i)).toList();

    return ContactDto(
      addresses: addresses,
      phones: List<String>.from(phones.map((dynamic item) => item.toString())),
      emails: List<String>.from(emails.map((dynamic item) => item.toString())),
    );
  }

  Map<String, dynamic> toJson() =>
      {'addresses': addresses, 'phones': phones, 'emails': emails};
}
