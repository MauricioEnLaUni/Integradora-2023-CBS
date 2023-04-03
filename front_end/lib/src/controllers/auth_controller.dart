import 'package:http/http.dart' as http;
import 'dart:convert';

import 'package:front_end/src/models/user_dto.dart';
import 'package:mongo_dart/mongo_dart.dart';

class ServerController {
  static login(LoginDto data) async {
    const connectionString =
        "mongodb+srv://210804utags:1EOsEwLtPGAYxkNT@cluster0.st6dfjy.mongodb.net/?retryWrites=true&w=majority";
    var db = await Db.create(connectionString);
    await db.open();
    var userCollection = db.collection("users");
    User usr = userCollection.find({"name", data.name}) as User;
    if (usr.password != data.password) return false;
    return true;
  }

  static loginWithServer(LoginDto data) async {
    const url = "localhost:5236";

    var res = await http.post(Uri.http(url, 'u/login'),
        body: jsonEncode({'name': data.name, 'password': data.password}),
        headers: {"Content-type": "application/json"});
    print(res.statusCode);
    if (res.statusCode == 200) return true;
    return false;
  }
}

class User {
  ObjectId id;
  String name;
  String password;

  User(this.id, this.name, this.password) {}
}
