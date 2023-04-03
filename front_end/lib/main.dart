import 'package:flutter/material.dart';
import 'package:front_end/src/controllers/project_controller.dart';
import './src/app.dart';

void main() async {
  print(await ProjectController.getProjects());
  runApp(const FictichosBuilderCRM());
}

class FictichosBuilderCRM extends StatelessWidget {
  const FictichosBuilderCRM({super.key});

  @override
  Widget build(BuildContext context) {
    return const App();
  }
}
