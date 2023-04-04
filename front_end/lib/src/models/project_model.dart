import 'dart:convert';
import 'package:intl/intl.dart';
import 'package:json_annotation/json_annotation.dart';

import 'ftasks_model.dart';

@JsonSerializable()
class ProjectDto {
  ProjectDto(this.id, this.name, this.starts, this.ends, this.lastTask);

  @JsonKey(name: 'Id')
  String id;
  String name;
  DateTime starts;
  DateTime ends;
  FTasksDto lastTask;

  ProjectDto.fromJson(Map<String, dynamic> jsonString)
    : id = jsonString['id'],
      name = jsonString['name'],
      starts = DateFormat('yyyy-MM-dd').parse(jsonString['starts']),
      ends = DateFormat('yyyy-MM-dd').parse(jsonString['ends']),
      lastTask = FTasksDto.fromJson(jsonString['lastTask']);

  Map<String, dynamic> toJson() => {
    'id' : id,
    'name' : name,
    'starts' : starts,
    'ends' : ends,
    'lastTask' : lastTask
  };
}