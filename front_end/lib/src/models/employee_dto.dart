import 'package:front_end/src/models/schedule_history_dto.dart';
import 'package:intl/intl.dart';
import 'package:json_annotation/json_annotation.dart';

import 'package:front_end/src/models/job_dto.dart';

@JsonSerializable()
class EmployeeDto {
  EmployeeDto(
      {required this.id,
      required this.name,
      required this.dob,
      required this.curp,
      required this.rfc,
      required this.charges,
      required this.scheduleHistory});

  String id;
  String name;
  DateTime dob;
  String curp;
  String rfc;
  List<JobDto> charges;
  List<ScheduleHistoryDto> scheduleHistory;

  factory EmployeeDto.fromJson(Map<String, dynamic> jsonString) {
    const String format = 'yyyy-MM-dd\'T\'HH:mm:ss\'';
    var jobsRaw = jsonString['charges'] as List<dynamic>;
    List<JobDto> jobs = jobsRaw.map((i) => JobDto.fromJson(i)).toList();
    var inner = jsonString['scheduleHistory'] as List<dynamic>;
    List<ScheduleHistoryDto> schedule =
        inner.map((i) => ScheduleHistoryDto.fromJson(i)).toList();

    return EmployeeDto(
        id: jsonString['id'],
        name: jsonString['name'],
        dob: DateFormat(format).parse(jsonString['dob']),
        curp: jsonString['curp'],
        rfc: jsonString['rfc'],
        charges: jobs,
        scheduleHistory: schedule);
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'name': name,
        'dob': dob,
        'curp': curp,
        'rfc': rfc,
        'charges': charges,
        'scheduleHistory': scheduleHistory
      };
}
