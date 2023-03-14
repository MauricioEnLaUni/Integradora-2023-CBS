import 'package:flutter/material.dart';

class MovingDisplay extends StatefulWidget {
  MovingDisplay({
    super.key,
    this.child,
  });

  Widget? child;

  @override
  State<MovingDisplay> createState() => _MovingDisplay();
}

class _MovingDisplay extends State<MovingDisplay> {
  @override
  Widget build(BuildContext context) {
    return MovingDisplay.child!();
  }
}