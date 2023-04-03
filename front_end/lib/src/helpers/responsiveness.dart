import 'package:flutter/material.dart';

const int largeScreenSize = 800;
const int smallScreenSize = 360;

class ResponsiveWidget extends StatelessWidget {
  final Widget largeScreen;
  final Widget smallScreen;

  const ResponsiveWidget(
      {super.key, required this.largeScreen, required this.smallScreen});

  static bool isSmallScreen(BuildContext context) =>
      MediaQuery.of(context).size.width < largeScreenSize;

  static bool isLargeScreen(BuildContext context) =>
      MediaQuery.of(context).size.width >= largeScreenSize;

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(builder: (context, constraints) {
      double width = constraints.maxWidth;
      if (width >= largeScreenSize) {
        return largeScreen;
      } else {
        return smallScreen;
      }
    });
  }
}
