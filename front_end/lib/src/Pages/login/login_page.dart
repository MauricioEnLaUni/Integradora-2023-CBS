import 'package:flutter/material.dart';

import '../../controllers/auth_controller.dart';
import '../../models/user_dto.dart';
import '../dashboard.dart';
import '../menu/menu_page.dart';

class LoginPage extends StatefulWidget {
  const LoginPage({super.key});

  @override
  _LoginPageState createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  GlobalKey<FormState> _formKey = GlobalKey<FormState>();
  late String userName;
  late String password;
  bool _loading = false;

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Form(
        key: _formKey,
        child: Stack(
          children: <Widget>[
            Transform.translate(
              offset: const Offset(0, -40),
              child: Center(
                child: SingleChildScrollView(
                  child: Card(
                    elevation: 2,
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(20),
                    ),
                    margin: const EdgeInsets.only(
                      left: 20,
                      right: 20,
                      top: 150,
                      bottom: 20,
                    ),
                    child: Padding(
                      padding: const EdgeInsets.symmetric(
                          horizontal: 35, vertical: 20),
                      child: Column(
                        mainAxisSize: MainAxisSize.min,
                        children: <Widget>[
                          TextFormField(
                            decoration: const InputDecoration(
                              labelText: 'Usuario',
                            ),
                            onChanged: (value) {
                              userName = value;
                            },
                            validator: (value) {
                              if (value!.isEmpty) 'Este campo es obligatorio';
                            },
                          ),
                          const SizedBox(height: 40),
                          TextFormField(
                            decoration: const InputDecoration(
                              labelText: 'Password',
                            ),
                            obscureText: true,
                            onChanged: (value) {
                              password = value;
                            },
                            validator: (value) {
                              if (value!.isEmpty) 'Este campo es obligatorio';
                            },
                          ),
                          const SizedBox(
                            height: 40,
                          ),
                          Padding(
                            padding: const EdgeInsets.symmetric(vertical: 15),
                            child: ElevatedButton(
                              onPressed: () async {
                                LoginDto data = LoginDto(userName, password);
                                if (await ServerController.loginWithServer(
                                    data)) {
                                  _login(context);
                                }
                              },
                              child: Row(
                                mainAxisAlignment: MainAxisAlignment.center,
                                children: <Widget>[
                                  const Text('Iniciar Sesión'),
                                  if (_loading)
                                    Container(
                                      height: 20,
                                      width: 20,
                                      margin: const EdgeInsets.only(left: 20),
                                      child: const CircularProgressIndicator(),
                                    ),
                                ],
                              ),
                            ),
                          ),
                          const SizedBox(
                            height: 20,
                          ),
                          Container(
                              margin: const EdgeInsets.only(top: 30.0),
                              child: ElevatedButton(
                                  child: const Text('Navigate to a new screen'),
                                  onPressed: () {
                                    Navigator.push(
                                      context,
                                      MaterialPageRoute(
                                          builder: (context) =>
                                              projectLayout()),
                                    );
                                  }))
                        ],
                      ),
                    ),
                  ),
                ),
              ),
            )
          ],
        ),
      ),
    );
  }

  void _login(BuildContext context) {
    if (!_loading) {
      setState(() {
        _loading = true;
      });
    }
    Navigator.push(
      context,
      MaterialPageRoute(builder: (context) => const DashBoardLayout()),
    );
  }
}
