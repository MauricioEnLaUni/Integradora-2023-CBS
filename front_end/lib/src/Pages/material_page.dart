import 'package:flutter/material.dart';

import '../../constants.dart';

class FMaterialPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: "Equipment Screen",
      home: Scaffold(
        appBar: AppBar(
          title: const Text(Constants.appTitle),
          actions: <Widget>[
            IconButton(
                icon: const Icon(Icons.manage_search),
                tooltip: 'Show searchbar',
                onPressed: () {}),
            TextFormField(
              decoration: const InputDecoration(
                hintText: 'Type your query here...',
              ),
            ),
          ],
        ),
      ),
    );
  }
}
