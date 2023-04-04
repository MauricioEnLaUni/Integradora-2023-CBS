import 'dart:convert';
import 'package:intl/intl.dart';
import 'package:json_annotation/json_annotation.dart';

import 'ftasks_model.dart';

@JsonSerializable()
class ProjectDto {
  ProjectDto(
      {required this.id,
      required this.name,
      required this.starts,
      required this.ends,
      this.lastTask});

  @JsonKey(name: 'Id')
  String id;
  String name;
  DateTime starts;
  DateTime ends;
  FTasksDto? lastTask;

  factory ProjectDto.fromJson(Map<String, dynamic> jsonString) {
    const String format = 'yyyy-MM-dd\'T\'HH:mm:ss\'';
    Map<String, dynamic>? last = jsonString['lastTask'];

    return ProjectDto(
        id: jsonString['id'],
        name: jsonString['name'],
        starts: DateFormat(format).parse(jsonString['starts']),
        ends: DateFormat(format).parse(jsonString['ends']),
        lastTask:
            last != null ? FTasksDto.fromJson(jsonString['lastTask']) : null);
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'name': name,
        'starts': starts,
        'ends': ends,
        'lastTask': lastTask
      };
}
