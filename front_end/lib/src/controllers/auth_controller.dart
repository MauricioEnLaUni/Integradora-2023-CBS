import 'dart:convert';
import 'package:http/browser_client.dart';
import 'package:http/http.dart' as http;

import '../models/user_dto.dart';

class AuthController {
  Future? loginUser(String username, String password) async {
    try {
      LoginDto data = LoginDto(username, password);
      String payload = json.encode(data);
      var client = BrowserClient();
      client.withCredentials = true;
      const url = "localhost:8008/u/login";

      var response = await client.post(Uri.parse(url),
          headers: {"Content-type": "application/json"}, body: payload);
      if (response.statusCode == 200) {
        var loginArr = json.decode(response.body);
      }
    } catch (e) {
      return null;
    }
  }
}
