import 'package:http/http.dart' as http;
import 'dart:convert';

import 'package:front_end/src/models/user_dto.dart';

class ServerController {
  static loginWithServer(LoginDto data) async {
    const url = "localhost:5236";

    var res = await http.post(Uri.http(url, 'User/login'),
        body: jsonEncode({'name': data.name, 'password': data.password}),
        headers: {"Content-type": "application/json"});
    if (res.statusCode == 200) return true;
    return false;
  }
}
