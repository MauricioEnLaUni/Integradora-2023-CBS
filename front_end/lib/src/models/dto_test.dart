import 'package:front_end/src/models/account_dto.dart';
import 'package:front_end/src/models/material_category_dto.dart';
import 'package:front_end/src/models/material_dto.dart';
import 'package:front_end/src/models/person_dto.dart';
import 'package:front_end/src/models/project_model.dart';
import 'package:front_end/src/models/user_info.dart';
import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class TestDto {
  TestDto(
      {required this.project,
      required this.material,
      required this.category,
      required this.account,
      required this.person,
      required this.userinfo});

  ProjectDto project;
  MaterialDto material;
  MaterialCategoryDto category;
  AccountDto account;
  PersonDto person;
  UserInfoDto userinfo;

  factory TestDto.fromJson(Map<String, dynamic> jsonString) {
    return TestDto(
      project: ProjectDto.fromJson(jsonString['projectDto']),
      material: MaterialDto.fromJson(jsonString['material']),
      category: MaterialCategoryDto.fromJson(jsonString['category']),
      account: AccountDto.fromJson(jsonString['account']),
      person: PersonDto.fromJson(jsonString['person']),
      userinfo: UserInfoDto.fromJson(jsonString['userInfo']),
    );
  }
}
