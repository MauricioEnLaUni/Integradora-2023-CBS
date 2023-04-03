import 'package:flutter/material.dart';

abstract class Constants {
  static const String appUrl = String.fromEnvironment(
    'appUrl',
    defaultValue: '',
  );

  static const String appTitle = String.fromEnvironment(
    'appTitle',
    defaultValue: '',
  );

  static const String appTheme = String.fromEnvironment(
    'appTheme',
    defaultValue: '',
  );
}

Color blueBar = const Color.fromARGB(0, 99, 250, 248);
Color shadow = const Color.fromARGB(0, 24, 24, 24);
Color pk = const Color.fromARGB(255, 255, 191, 191);
