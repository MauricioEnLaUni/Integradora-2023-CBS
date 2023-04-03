// import 'package:flutter/material.dart';
// import '../../../constants.dart';

// class FMaterialPage extends StatelessWidget {
//   @override
//   Widget build(BuildContext context) {
//     return MaterialApp(
//       title: "Equipment Screen",
//       home: Scaffold(
//         appBar: AppBar(
//           title: const Text(Constants.appTitle),
//           actions: <Widget>[
//             IconButton(
//                 icon: const Icon(Icons.manage_search),
//                 tooltip: 'Show searchbar',
//                 onPressed: () {}),
//             TextFormField(
//               decoration: const InputDecoration(
//                 hintText: 'Type your query here...',
//               ),
//             ),
//           ],
//         ),
//       ),
//     );
//   }
// }


import 'package:flutter/material.dart';
import 'package:flutter/src/widgets/icon.dart';
import 'package:front_end/src/Pages/material/material_page.dart';
import 'package:front_end/src/Pages/person/person_page.dart';
import 'package:flutter/material.dart';
import 'package:front_end/src/helpers/responsiveness.dart';
import 'package:front_end/src/Pages/material/meterial_layout.dart';
import 'package:front_end/src/wigets/top_nav.dart';
import '../../../constants.dart';

class FMaterialPage extends StatelessWidget {
  final GlobalKey<ScaffoldState> scaffoldKey = GlobalKey();
   @override
   Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: topNavigationBar(context, scaffoldKey),
        drawer: Drawer(),
        body: person_large_Screen(),
      ),
    );
  }
}
