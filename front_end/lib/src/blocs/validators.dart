import 'dart:async';

class Validators {
  final validateUsername = StreamTransformer<String, String>.fromHandlers(
      handleData: (username, sink) {
    RegExp exp = RegExp(r'^[a-zA-Z][a-zA-Z0-9-_]{4,27}$');
    if (exp.firstMatch(username).toString() == username) {
      sink.add(username);
    } else {
      sink.addError('Invalid Username.');
    }
  });

  final validatePassword = StreamTransformer<String, String>.fromHandlers(
      handleData: (password, sink) {
    RegExp exp =
        RegExp(r'^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,64}$');
    if (exp.firstMatch(password).toString() == password) {
      sink.add(password);
    } else {
      sink.addError('Invalid Password.');
    }
  });
}
