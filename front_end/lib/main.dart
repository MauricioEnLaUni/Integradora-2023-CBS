import 'package:flutter/material.dart';
import 'package:front_end/src/components/fictichos_card_input.dart';

void main() => runApp(const FictichosBuilderCRM());

class FictichosBuilderCRM extends StatelessWidget {
  const FictichosBuilderCRM({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      theme: ThemeData(
          colorSchemeSeed: const Color(0xff6750a4), useMaterial3: true),
      home: const Scaffold(
        body: FCardInput(),
      ),
    );
  }
}
