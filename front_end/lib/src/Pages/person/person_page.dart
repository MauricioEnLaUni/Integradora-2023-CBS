import 'package:flutter/src/widgets/container.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:front_end/src/Pages/menu/menu_large_screen.dart';
import 'package:front_end/src/Pages/menu/menu_small_screen.dart';
import 'package:front_end/src/Pages/person/person_large_screen.dart';
import 'package:front_end/src/helpers/responsiveness.dart';
import 'package:front_end/src/wigets/top_nav.dart';
import 'package:flutter/material.dart';


class person_page extends StatelessWidget {
final GlobalKey<ScaffoldState> scaffoldKey = GlobalKey();

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: topNavigationBar(context, scaffoldKey),
        drawer: Drawer(),
        body: person_large_Screen(),
      ),
    );
  }
}