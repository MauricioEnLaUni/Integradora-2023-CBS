import 'package:flutter/material.dart';

import 'material/material_page.dart';

class Dashboard extends StatelessWidget {
  const Dashboard({super.key});

  @override
  Widget build(BuildContext context) {
    return Container();
  }
}

class DashBoardLayout extends StatelessWidget {
  const DashBoardLayout({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
        home: GridView.count(
      crossAxisCount: 5,
      children: List.generate(99, (index) {
        return Center(
          child: Card(
              child: IconButton(
                  icon: const Icon(Icons.agriculture),
                  tooltip: 'Equipment Manager',
                  onPressed: () {
                    Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (context) => FMaterialPage()));
                  })),
        );
      }),
    ));
  }
}
