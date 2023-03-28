import 'package:flutter/src/widgets/container.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:flutter/material.dart';

class SmallScreen extends StatelessWidget {
  const SmallScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Center(
      child: GridView.count(
        crossAxisCount: 2,
        children: <Widget>[
          IconButton(
            iconSize: 90,
            icon: const Icon(
              Icons.person,
            ),
            onPressed: () {
              //
            },
            tooltip: 'persona',
          ),
          IconButton(
            iconSize: 90,
            icon: const Icon(
              Icons.construction,
            ),
            onPressed: () {
              //
            },
            tooltip: 'material',
          ),
          IconButton(
            iconSize: 90,
            icon: const Icon(
              Icons.book_sharp,
            ),
            onPressed: () {
              //
            },
            tooltip: 'proyectos',
          ),
          IconButton(
            iconSize: 90,
            icon: const Icon(
              Icons.account_balance_wallet,
            ),
            onPressed: () {
              //
            },
            tooltip: 'cuentas',
          ),
        ],
      ),
    );
  }
}