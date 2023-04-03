import 'package:flutter/material.dart';
import 'package:mongo_dart/mongo_dart.dart' hide State;
import 'package:http/http.dart' as http;

class person_large_Screen extends StatelessWidget {
  const person_large_Screen({super.key});

  Widget build(BuildContext context) {
    return TextField(
      decoration: InputDecoration(
        hintText: 'Buscar...',
        prefixIcon: Icon(Icons.search),
        border: OutlineInputBorder(
          borderRadius: BorderRadius.circular(10),
          borderSide: BorderSide.none,
        ),
        filled: true,
        fillColor: Colors.grey[200],
      ),
    );
  }
}
