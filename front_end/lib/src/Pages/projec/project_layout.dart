import 'package:flutter/material.dart';

class project_large_Screen extends StatefulWidget {
  const project_large_Screen({Key? key}) : super(key: key);
  @override
  _project_large_ScreenState createState() => _project_large_ScreenState();
}

class _project_large_ScreenState extends State<project_large_Screen> {
  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisSize: MainAxisSize.min,
      mainAxisAlignment: MainAxisAlignment.spaceAround,
      children: [
        DataTable(
          columns: const [
            DataColumn(label: Text('proyecto')),
            DataColumn(label: Text('Fecha de inicio')),
            DataColumn(label: Text('Fecha de cierre')),
            DataColumn(label: Text('No se')),
          ],
          rows: [],
          /*_data.asMap().entries.map((entry) {
            final index = entry.key;
            final data = entry.value;
            return DataRow(cells: [
              DataCell(TextField(
                controller: _controllers[index],
                decoration: InputDecoration(
                  hintText: data['proyecto'],
                ),
              )),
              DataCell(
                TextField(
                  controller: _controllers[index],
                  decoration: InputDecoration(
                    hintText: data['Fecha Inicio'],
                  ),
                ),
              ),
              DataCell(
                TextField(
                  controller: _controllers[index],
                  decoration: InputDecoration(
                    hintText: data['FechaCierre'],
                  ),
                ),
              ),
            ]);
          }).toList(),*/
          sortColumnIndex: 0,
          sortAscending: true,
        ),
      ],
    );
  }
}
