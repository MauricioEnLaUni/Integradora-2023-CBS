import 'package:intl/intl.dart';
import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class PaymentDto {
  PaymentDto(
      {required this.id,
      required this.amount,
      required this.concept,
      required this.createdAt,
      required this.due,
      required this.complete});

  String id;
  double amount;
  String concept;
  DateTime createdAt;
  DateTime due;
  bool complete;

  factory PaymentDto.fromJson(Map<String, dynamic> jsonString) {
    const String format = 'yyyy-MM-dd\'T\'HH:mm:ss\'';

    return PaymentDto(
        id: jsonString['id'],
        amount: jsonString['amount'],
        concept: jsonString['concept'],
        createdAt: DateFormat(format).parse(jsonString['createdAt']),
        due: DateFormat(format).parse(jsonString['due']),
        complete: jsonString['complete']);
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'amount': amount,
        'concept': concept,
        'createdAt': createdAt,
        'due': due,
        'complete': complete
      };
}
