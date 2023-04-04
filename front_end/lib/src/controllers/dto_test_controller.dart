import 'dart:convert';

import 'package:front_end/src/models/dto_test.dart';
import 'package:http/http.dart' as http;

class DtoTestController {
  static getEverything() async {
    const String url = 'localhost:5236';

    var res = await http.get(Uri.http(url, 'Test'),
        headers: {"Content-type": "application/json"});
    if (res.statusCode == 200) return fromJson(res.body);
    return res.statusCode;
  }

  static TestDto fromJson(String jsonString) {
    final dynamic data = json.decode(jsonString);
    return TestDto.fromJson(data);
  }
}
