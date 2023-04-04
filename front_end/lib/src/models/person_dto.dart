import 'package:json_annotation/json_annotation.dart';

import 'package:front_end/src/models/contact_dto.dart';
import 'package:front_end/src/models/employee_dto.dart';

@JsonSerializable()
class PersonDto {
  PersonDto(
      {required this.id,
      required this.name,
      required this.contacts,
      this.employed});

  String id;
  String name;
  ContactDto contacts;
  EmployeeDto? employed;

  factory PersonDto.fromJson(Map<String, dynamic> jsonString) {
    Map<String, dynamic>? employed = jsonString['employee'];

    return PersonDto(
        id: jsonString['id'],
        name: jsonString['name'],
        contacts: ContactDto.fromJson(jsonString['contact']),
        employed: employed != null
            ? EmployeeDto.fromJson(jsonString['employee'])
            : null);
  }

  Map<String, dynamic> toJson() =>
      {'id': id, 'name': name, 'contact': contacts, 'employee': employed};
}
