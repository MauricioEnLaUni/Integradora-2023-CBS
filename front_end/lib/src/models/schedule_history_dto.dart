import 'package:front_end/src/models/address_model.dart';
import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class ScheduleHistoryDto {
  ScheduleHistoryDto({required this.id, required this.period, this.location});

  String id;
  String period;
  AddressDto? location;

  factory ScheduleHistoryDto.fromJson(Map<String, dynamic> jsonString) {
    Map<String, dynamic>? loc = jsonString['location'];

    return ScheduleHistoryDto(
        id: jsonString['id'],
        period: jsonString['period'],
        location:
            loc != null ? AddressDto.fromJson(jsonString['location']) : null);
  }

  Map<String, dynamic> toJson() =>
      {'id': id, 'period': period, 'location': location};
}
