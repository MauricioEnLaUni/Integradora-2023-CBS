import 'package:front_end/src/models/salary_dto.dart';
import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class MaterialDto {
  MaterialDto(
      {required this.id,
      required this.name,
      required this.owner,
      required this.brand,
      required this.quantity});

  String id;
  String name;
  String owner;
  String brand;
  int quantity;

  factory MaterialDto.fromJson(Map<String, dynamic> jsonString) {
    return MaterialDto(
        id: jsonString['id'],
        name: jsonString['name'],
        owner: jsonString['owner'],
        brand: jsonString['brand'],
        quantity: jsonString['quantity']);
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'name': name,
        'owner': owner,
        'brand': brand,
        'quantity': quantity
      };
}
