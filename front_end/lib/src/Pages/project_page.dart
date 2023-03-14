import 'package:flutter/material.dart';

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
      title: "a",
      home: Scaffold(
          appBar: AppBar(
            title: const Text("login"),
          ),
          body: Row(
            children: [const Text("Proyectos"), Container()],
          )),
    );
  }
}
