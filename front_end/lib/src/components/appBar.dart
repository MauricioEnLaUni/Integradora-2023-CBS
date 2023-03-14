import 'package:flutter/material.dart';

class FictichosAppBar extends AppBar {
  FictichosAppBar({super.key});
}

class AppBarWrapper extends StatelessWidget {
  const AppBarWrapper({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      child: AppBar(),
    );
  }
}
