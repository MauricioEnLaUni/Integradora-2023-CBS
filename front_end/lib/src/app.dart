import 'package:flutter/material.dart';

import './Pages/login_page.dart';

class App extends StatelessWidget {
  const App({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: "a",
      home: Scaffold(
        appBar: AppBar(
          title: const Text("a"),
        ),
        body: const LogInPage(),
      ),
    );
  }
}