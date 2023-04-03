import 'dart:convert';

import 'package:intl/intl.dart';
import 'package:json_annotation/json_annotation.dart';

import 'address_model.dart';

@JsonSerializable()
class FTasksDto {
  String id;
  DateTime startDate;
  DateTime ends;
  String parent;
  List<String> subtasks;
  List<String> employeesAssigned;
  List<String> material;
  AddressDto? address;

  FTasksDto({
    required this.id,
    required this.startDate,
    required this.ends,
    required this.parent,
    required this.subtasks,
    required this.employeesAssigned,
    required this.material,
    this.address,
  });

  factory FTasksDto.fromJson(Map<String, dynamic> jsonString) {
    List<dynamic> subtasks = jsonString['subtasks'] ?? [];
    List<dynamic> employeesAssigned= jsonString['employeesAssigned'] ?? [];
    List<dynamic> material= jsonString['material'] ?? [];
    Map<String, dynamic>? addressJson = jsonString['address'];

    return FTasksDto(
      id: jsonString['id'] ?? '',
      startDate: DateFormat('yyyy-MM-dd').parse(jsonString['startDate']),
      ends: DateFormat('yyyy-MM-dd').parse(jsonString['ends']),
      parent: jsonString['parent'] ?? '',
      subtasks: List<String>.from(subtasks.map((dynamic item) => item.toString())),
      employeesAssigned: List<String>.from(employeesAssigned.map((dynamic item) => item.toString())),
      material: List<String>.from(material.map((dynamic item) => item.toString())),
      address: addressJson != null ? AddressDto.fromJson(jsonString['address']) : null,
    );
  }

  Map<String, dynamic> toJson() => {
    'id' : id,
    'startDate' : startDate,
    'ends' : ends,
    'parent' : parent,
    'subtasks' : subtasks,
    'employeesAssigned' : employeesAssigned,
    'material' : material,
    'address' : address
  };
}