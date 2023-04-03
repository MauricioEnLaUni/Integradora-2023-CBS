import 'package:flutter/material.dart';
import './src/app.dart';

void main() async {
  runApp(const FictichosBuilderCRM());
}

class FictichosBuilderCRM extends StatelessWidget {
  const FictichosBuilderCRM({super.key});

  @override
  Widget build(BuildContext context) {
    return const App();
  }
}
