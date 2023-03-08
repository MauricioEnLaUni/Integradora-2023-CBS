import 'package:flutter/material.dart';
import '../Pages/test.dart';

import '../constants.dart';

class Login extends StatelessWidget {
  Login({super.key});

  @override
  Widget build(BuildContext context) {
    return cuerpo(context);
  }
}

Widget texto() {
  return const Text("One test");
}

Widget fictichosTextButton(BuildContext context) {
  return TextButton(
    onPressed: () {
      print("Ir a la siguiente página");
      Navigator.push(
        context,
        MaterialPageRoute(builder: (context) => const Test())
      );
    }, child: const Text("a"),
  );
}

Widget cuerpo(BuildContext context) {
  return Scaffold(
      appBar: AppBar(
        title: const Text(Constants.appTitle),
      ),
      body: Column(
        children: [
          texto(),
          fictichosTextButton(context),
        ],
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () => null,
        tooltip: "Empty",
        child: const Icon(Icons.add),
      ),
    );
}

Widget fields(BuildContext context) {
  return Container(
    decoration: const BoxDecoration(
      
    ),
  );
}