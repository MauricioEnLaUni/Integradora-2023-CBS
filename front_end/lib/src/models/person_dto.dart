import 'package:json_annotation/json_annotation.dart';

import 'package:front_end/src/models/contact_dto.dart';
import 'package:front_end/src/models/employee_dto.dart';

@JsonSerializable()
class PersonDto {
  PersonDto(
      {required this.id,
      required this.name,
      required this.lastName,
      required this.relation,
      required this.contacts,
      this.employed});

  String id;
  String name;
  String lastName;
  String relation;
  ContactDto contacts;
  EmployeeDto? employed;

  factory PersonDto.fromJson(Map<String, dynamic> jsonString) {
    Map<String, dynamic>? employed = jsonString['employed'];

    return PersonDto(
        id: jsonString['id'],
        name: jsonString['name'],
        lastName: jsonString['lastName'],
        relation: jsonString['relation'],
        contacts: ContactDto.fromJson(jsonString['contacts']),
        employed: employed != null
            ? EmployeeDto.fromJson(jsonString['employed'])
            : null);
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'name': name,
        'lastName': lastName,
        'relation': relation,
        'contacts': contacts,
        'employed': employed
      };
}
