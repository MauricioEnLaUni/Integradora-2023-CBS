import 'package:front_end/src/models/payment_dto.dart';
import 'package:intl/intl.dart';
import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class AccountDto {
  AccountDto(
      {required this.id,
      required this.createdAt,
      required this.payments,
      required this.owner});

  String id;
  DateTime createdAt;
  List<PaymentDto> payments;
  String owner;

  factory AccountDto.fromJson(Map<String, dynamic> jsonString) {
    const String format = 'yyyy-MM-dd\'T\'HH:mm:ss\'';
    var temp = jsonString['payments'] as List<dynamic>;
    List<PaymentDto> pay = temp.map((i) => PaymentDto.fromJson(i)).toList();

    return AccountDto(
        id: jsonString['id'],
        createdAt: DateFormat(format).parse(jsonString['createdAt']),
        payments: pay,
        owner: jsonString['owner']);
  }

  Map<String, dynamic> toJson() =>
      {'id': id, 'createdAt': createdAt, 'payments': payments, 'owner': owner};
}
