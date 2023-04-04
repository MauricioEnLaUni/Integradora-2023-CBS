import 'package:front_end/src/models/salary_dto.dart';
import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class MaterialCategoryDto {
  MaterialCategoryDto(
      {required this.id,
      required this.name,
      required this.parent,
      required this.subcategory,
      required this.children});

  String id;
  String name;
  String parent;
  List<String> subcategory;
  List<String> children;

  factory MaterialCategoryDto.fromJson(Map<String, dynamic> jsonString) {
    List<dynamic> sub = jsonString['subcategory'] ?? [];
    List<dynamic> child = jsonString['subcategory'] ?? [];

    return MaterialCategoryDto(
        id: jsonString['id'],
        name: jsonString['name'],
        parent: jsonString['parent'],
        subcategory:
            List<String>.from(sub.map((dynamic item) => item.toString())),
        children:
            List<String>.from(child.map((dynamic item) => item.toString())));
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'name': name,
        'parent': parent,
        'subcategory': subcategory,
        'children': children
      };
}
