import 'package:flutter/material.dart';
import 'package:front_end/src/controllers/project_controller.dart';
import 'package:front_end/src/models/project_model.dart';
import './src/app.dart';

void main() async {
  List<ProjectDto> data = await ProjectController.getProjects();
  print(data[0].starts);
  runApp(const FictichosBuilderCRM());
}

class FictichosBuilderCRM extends StatelessWidget {
  const FictichosBuilderCRM({super.key});

  @override
  Widget build(BuildContext context) {
    return const App();
  }
}
