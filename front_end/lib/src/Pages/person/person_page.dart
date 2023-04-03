import 'package:front_end/src/Pages/person/person_large_screen.dart';
import 'package:front_end/src/wigets/top_nav.dart';
import 'package:flutter/material.dart';

class person_page extends StatelessWidget {
  final GlobalKey<ScaffoldState> scaffoldKey = GlobalKey();

  person_page({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: topNavigationBar(context, scaffoldKey),
        drawer: const Drawer(),
        body: const person_large_Screen(),
      ),
    );
  }
}
