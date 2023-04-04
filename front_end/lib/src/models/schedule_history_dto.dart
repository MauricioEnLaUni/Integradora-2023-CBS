import 'package:front_end/src/models/address_model.dart';
import 'package:front_end/src/models/salary_dto.dart';
import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class ScheduleHistoryDto {
  ScheduleHistoryDto(
      {required this.id,
      required this.period,
      required this.hours,
      this.location});

  String id;
  String period;
  Map<Duration, int> hours;
  AddressDto? location;

  factory ScheduleHistoryDto.fromJson(Map<String, dynamic> jsonString) {
    Map<String, dynamic> temp = jsonString['hours'];
    Map<Duration, int> duration = {};
    Map<String, dynamic>? loc = jsonString['location'];

    temp.forEach((key, value) {
      int ticks = int.parse(key);
      Duration dur = Duration(microseconds: ticks ~/ 10);
      duration[dur] = value;
    });

    return ScheduleHistoryDto(
        id: jsonString['id'],
        period: jsonString['period'],
        hours: duration,
        location:
            loc != null ? AddressDto.fromJson(jsonString['location']) : null);
  }

  Map<String, dynamic> toJson() =>
      {'id': id, 'period': period, 'hours': hours, 'location': location};
}
