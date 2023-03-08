import 'dart:async';
import 'package:front_end/src/blocs/validators.dart';

import './bloc.dart';

class Bloc with Validators {
  final _usernameController = StreamController<String>();
  final _passwordController = StreamController<String>();

  Stream<String> get username =>
      _usernameController.stream.transform(validateUsername);
  Stream<String> get password =>
      _passwordController.stream.transform(validatePassword);

  Function(String) get changeUsername => _usernameController.sink.add;
  Function(String) get changePassword => _passwordController.sink.add;

  dispose() {
    _usernameController.close();
    _passwordController.close();
  }
}
