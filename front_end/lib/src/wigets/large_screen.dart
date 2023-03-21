import 'package:flutter/material.dart';
import 'package:flutter/src/widgets/icon.dart';

const IconData person = IconData(0xe491, fontFamily: 'MaterialIcons');
const IconData construction = IconData(0xe189, fontFamily: 'MaterialIcons');
const IconData book_sharp = IconData(0xe7ed, fontFamily: 'MaterialIcons');
const IconData account_balance_wallet =
    IconData(0xe041, fontFamily: 'MaterialIcons');

class LargeScreen extends StatelessWidget {
  const LargeScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Row(
        mainAxisAlignment: MainAxisAlignment.center,
        children: <Widget>[
          IconButton(
            iconSize: 60,
            icon: const Icon(
              Icons.person,
            ),
            onPressed: () {
              //
            },
            tooltip: 'persona',
          ),
          IconButton(
            iconSize: 60,
            icon: const Icon(
              Icons.construction,
            ),
            onPressed: () {
              //
            },
            tooltip: 'material',
          ),
          IconButton(
            iconSize: 60,
            icon: const Icon(
              Icons.book_sharp,
            ),
            onPressed: () {
              //
            },
            tooltip: 'proyectos',
          ),
          IconButton(
            iconSize: 60,
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
