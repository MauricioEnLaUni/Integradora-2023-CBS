import 'package:intl/intl.dart';
import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class SalaryDto {
  SalaryDto(
      {required this.id,
      required this.period,
      required this.due,
      required this.reductions,
      required this.hourlyRate,
      required this.closed,
      this.hoursWeeklyCap});

  String id;
  String period;
  String due;
  Map<String, double> reductions;
  double hourlyRate;
  DateTime closed;
  int? hoursWeeklyCap;

  factory SalaryDto.fromJson(Map<String, dynamic> jsonString) {
    const String format = 'yyyy-MM-dd\'T\'HH:mm:ss\'';
    Map<String, dynamic> d = jsonString['reductions'];
    Map<String, double> reductionsLocal = {};
    d.forEach((key, value) => {reductionsLocal[key] = value.toDouble()});

    return SalaryDto(
        id: jsonString['id'],
        period: jsonString['period'],
        due: jsonString['due'],
        reductions: reductionsLocal,
        hourlyRate: jsonString['hourlyRate'],
        closed: DateFormat(format).parse(jsonString['closed']),
        hoursWeeklyCap: jsonString['hoursWeeklyCap']);
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'period': period,
        'due': due,
        'reductions': reductions,
        'hourlyRate': hourlyRate,
        'closed': closed,
        'hoursWeeklyCap': hoursWeeklyCap
      };
}
