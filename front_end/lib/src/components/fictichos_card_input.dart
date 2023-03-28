import 'package:flutter/material.dart';

class FCardInput extends StatelessWidget {
  const FCardInput({super.key});

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Card(
        elevation: 0,
        shape: RoundedRectangleBorder(
          side: BorderSide(
            color: Theme.of(context).colorScheme.outline,
          ),
          borderRadius: const BorderRadius.all(Radius.circular(12)),
        ),
        child: SizedBox(
          width: 300,
          height: 100,
          child: Center(
            child: Row(
              children: [
                const Text("Project"),
                Column(
                  children: const [
                    Text("New Project"),
                  ],
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
