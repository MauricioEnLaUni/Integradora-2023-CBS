import 'package:flutter/material.dart';
import 'package:front_end/src/controllers/dto_test_controller.dart';
import 'package:front_end/src/models/dto_test.dart';
import './src/app.dart';

void main() async {
  TestDto data = await DtoTestController.getEverything();
  runApp(const FictichosBuilderCRM());
}

class FictichosBuilderCRM extends StatelessWidget {
  const FictichosBuilderCRM({super.key});

  @override
  Widget build(BuildContext context) {
    return const App();
  }
}
