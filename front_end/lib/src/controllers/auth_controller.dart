import 'dart:convert';
import 'package:http/http.dart' as http;

import '../models/user_dto.dart';

class AuthController {
  Future? loginUser(String username, String password) async {
    try {
      LoginDto data = LoginDto(username, password);
      const url = "localhost:8008/u/login";

      var response = await http.post(Uri.parse(url), body: {data});
      if (response.statusCode == 200) {
        var loginArr = json.decode(response.body);
      }
    } catch (e) {
      return null;
    }
  }
}
