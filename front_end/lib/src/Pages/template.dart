import 'package:flutter/material.dart';

import 'package:front_end/constants.dart';
import 'package:front_end/src/Theme/dark_theme.dart';

class Template extends StatelessWidget{
  const Template({ super.key });

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: Constants.appTitle,
      theme: CommonMethod().darkTheme,
      home: const Text('Is it broken?'),
      locale: null,
    );
  }
}