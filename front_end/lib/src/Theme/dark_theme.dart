import 'package:flutter/material.dart';

class CommonMethod {
  ThemeData darkTheme = ThemeData(
    appBarTheme: AppBarTheme(
      color: Colors.grey[800]
    ),
    textButtonTheme: TextButtonThemeData(
      style: TextButton.styleFrom(
        foregroundColor: Colors.grey[200]
      ),
    ),
    scaffoldBackgroundColor: Colors.grey[100],
    textTheme: const TextTheme(
      bodyLarge: TextStyle(),
      bodyMedium: TextStyle(),
      bodySmall: TextStyle(),
    ).apply(
      bodyColor: Colors.teal
    )
  );
}