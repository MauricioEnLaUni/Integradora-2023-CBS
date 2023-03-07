import 'package:flutter/material.dart';

import 'constants.dart';

void main() => runApp(const FictichosBuilderCRM());

class FictichosBuilderCRM extends StatelessWidget {
  const FictichosBuilderCRM({super.key});

  @override
  Widget build(BuildContext context) {
    return const MaterialApp(
      title: Constants.appTitle,
      home: Scaffold(),
    );
  }
}
