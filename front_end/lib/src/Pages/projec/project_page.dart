import 'package:flutter/material.dart';
import 'package:front_end/src/Pages/material/meterial_layout.dart';
import 'package:front_end/src/wigets/top_nav.dart';
import '../projec/project_layout.dart';

class projectsPage extends StatelessWidget {
  final GlobalKey<ScaffoldState> scaffoldKey = GlobalKey();

  projectsPage({super.key});
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: topNavigationBar(context, scaffoldKey),
        drawer: const Drawer(),
        body: projectsPage(),
      ),
    );
  }
}
