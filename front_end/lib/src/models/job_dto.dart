import 'package:front_end/src/models/salary_dto.dart';
import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class JobDto {
  JobDto(
      {required this.id,
      required this.name,
      required this.internalKey,
      required this.salaryHistory,
      required this.role});

  String id;
  String name;
  List<SalaryDto> salaryHistory;
  String internalKey;
  String role;

  factory JobDto.fromJson(Map<String, dynamic> jsonString) {
    var inner = jsonString['salaryHistory'] as List<dynamic>;
    List<SalaryDto> salaries = inner.map((i) => SalaryDto.fromJson(i)).toList();

    return JobDto(
        id: jsonString['id'],
        name: jsonString['name'],
        salaryHistory: salaries,
        internalKey: jsonString['internalKey'],
        role: jsonString['role']);
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'name': name,
        'salaryHistory': salaryHistory,
        'internalKey': internalKey,
        'role': role
      };
}
