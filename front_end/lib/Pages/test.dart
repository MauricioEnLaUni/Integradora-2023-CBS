import 'package:flutter/material.dart';

import '../Login/login.dart';
import '../constants.dart';

class Test extends StatelessWidget {
  const Test({ super.key });

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text(Constants.appTitle),
      ),
      body: Column(
        children: [
          const Text("One"),
          const Icon(Icons.account_balance_wallet_sharp),
          TextButton(
            onPressed: () {
              Navigator.pop(context);
            },
            child: const Text("Back"),
          ),
        ],
      ),
    );
  }
}