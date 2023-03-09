import 'dart:async';
import 'package:flutter/material.dart';
import 'package:front_end/src/Pages/dashboard.dart';
import 'package:front_end/src/controllers/auth_controller.dart';

class LogInPage extends StatelessWidget {
  const LogInPage({super.key});

  @override
  Widget build(BuildContext context) {
    UserManagement manager = UserManagement();
    AuthController authController = AuthController();

    return Column(
      children: [
        const SizedBox(
          height: 20,
        ),
        Padding(
          padding: const EdgeInsets.symmetric(horizontal: 20),
          child: SizedBox(
            height: 50,
            child: usernameField(manager),
          ),
        ),
        const SizedBox(
          height: 20,
        ),
        Padding(
          padding: const EdgeInsets.symmetric(horizontal: 20),
          child: SizedBox(
            height: 50,
            child: passwordField(manager),
          ),
        ),
        const SizedBox(
          height: 20,
        ),
        ElevatedButton(
          onPressed: () {
            authController.loginUser(
                manager.username.toString(), manager.password.toString());
          },
          child: const Text('Login'),
        ),
        ElevatedButton(
          onPressed: () {
            Navigator.push(context,
                MaterialPageRoute(builder: (context) => DashBoardLayout()));
          },
          child: const Text('Go to Dashboard'),
        ),
      ],
    );
  }
}

class UserManagement {
  final _usernameController = StreamController<String>();
  final _passwordController = StreamController<String>();

  Stream<String> get username =>
      _usernameController.stream.transform(validateUsername);
  Stream<String> get password =>
      _passwordController.stream.transform(validatePassword);

  Function(String) get changeUsername => _usernameController.sink.add;
  Function(String) get changePassword => _passwordController.sink.add;

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

  dispose() {
    _usernameController.close();
    _passwordController.close();
  }
}

Widget usernameField(UserManagement mng) {
  return StreamBuilder(
    stream: mng.username,
    builder: (context, snapshot) {
      return TextField(
        decoration: const InputDecoration(
          hintText: '',
          enabledBorder: OutlineInputBorder(),
          focusedBorder: OutlineInputBorder(),
          labelText: 'Username',
        ),
        onChanged: mng.changeUsername,
      );
    },
  );
}

Widget passwordField(UserManagement mng) {
  return StreamBuilder(
    stream: mng.password,
    builder: (context, snapshot) {
      return TextField(
        decoration: const InputDecoration(
          hintText: 'Password',
          enabledBorder: OutlineInputBorder(),
          focusedBorder: OutlineInputBorder(),
        ),
        obscureText: true,
        onChanged: (newValue) {
          mng.changePassword(newValue);
        },
      );
    },
  );
}