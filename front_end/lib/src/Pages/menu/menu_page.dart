import 'package:flutter/material.dart';
import 'package:front_end/src/helpers/responsiveness.dart';
import 'package:front_end/src/Pages/menu/menu_large_screen.dart';
import 'package:front_end/src/Pages/menu/menu_small_screen.dart';
import 'package:front_end/src/wigets/top_nav.dart';
import '../../../constants.dart';

class project_page extends StatelessWidget {
  const project_page({super.key});

  @override
  Widget build(BuildContext context) {
    return Container();
  }
}

class projectLayout extends StatelessWidget {
  final GlobalKey<ScaffoldState> scaffoldKey = GlobalKey();
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: topNavigationBar(context, scaffoldKey),
        drawer: Drawer(),
        body: ResponsiveWidget(largeScreen: LargeScreen(), smallScreen: SmallScreen()),
      ),
    );
  }
}
