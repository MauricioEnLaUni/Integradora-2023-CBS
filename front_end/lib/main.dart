import 'package:flutter/material.dart';

import 'constants.dart';

void main() {
  return runApp(
    MaterialApp(
      title: Constants.appTitle,
      home: Scaffold(
        appBar: AppBar(
          title: const Text('Dashboard'),
        ),
        body: const Center(
          child: Text(
            'Hello World',
            textDirection: TextDirection.ltr,
          ),
        ),
      ),
    ),
  );
}
