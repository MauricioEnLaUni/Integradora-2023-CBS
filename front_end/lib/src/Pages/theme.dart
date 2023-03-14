import 'package:flutter/material.dart';

void main() {
  runApp(
    MyApp(),
  );
}

class MyApp extends StatefulWidget {
  @override
  _MyAppState createState() => _MyAppState();
}

class _MyAppState extends State<MyApp> {
  bool _isDarkMode = false;
  String my_text = "Click the button below to toggle dark mode";
  Icon my_icon = Icon(Icons.light_mode);

  void _toggleDarkMode() {
    setState(() {
      _isDarkMode = !_isDarkMode;
      if (_isDarkMode == false) {
        my_text = "Click the button below to toggle dark mode";
        my_icon = Icon(Icons.light_mode);
      } else {
        my_text = "Click the button below to toggle light mode";

        my_icon = Icon(Icons.dark_mode);
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      theme: _isDarkMode ? ThemeData.dark() : ThemeData.light(),
      home: Scaffold(
        appBar: AppBar(
          title: Text('Dark Mode Example'),
        ),
        body: Center(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Icon(my_icon.icon),
              Text(my_text),
              ElevatedButton(
                onPressed: _toggleDarkMode,
                child: Text('Toggle Dark Mode'),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
