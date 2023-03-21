import 'package:flutter/material.dart';
import 'package:front_end/src/wigets/large_screen.dart';
import '../../constants.dart';

class project_page extends StatelessWidget {
  const project_page({super.key});

  @override
  Widget build(BuildContext context) {
    return Container();
  }
}

class projectLayout extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(
          elevation: 0,
          backgroundColor: Colors.orange,
          title: const Text(
            "login",
            style: TextStyle(
                fontSize: 32,
                color: Colors.black,
                backgroundColor: Colors.green),
          ),
        ),
        body: LargeScreen(),
      ),
    );
  }
}
