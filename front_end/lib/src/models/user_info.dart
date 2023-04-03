import 'dart:typed_data';
import 'package:intl/intl.dart';
import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class UserInfoDto {
  UserInfoDto({ required this.name, required this.createdAt, required this.roles, required this.email, this.avatar });

  String name;
  DateTime createdAt;
  List<String> roles;
  List<String> email;
  Uint8List? avatar;

  factory UserInfoDto.fromJson(Map<String, dynamic> jsonString) {
    return UserInfoDto(
      name: jsonString['name'],
      createdAt: DateFormat('yyyy-MM-dd').parse(jsonString['createdAt']),
      roles: List<String>.from(jsonString['roles']),
      email: List<String>.from(jsonString['email']),
      avatar: jsonString['avatar'] != null ? Uint8List.fromList(jsonString['avatar']) : null
    );
  }
}