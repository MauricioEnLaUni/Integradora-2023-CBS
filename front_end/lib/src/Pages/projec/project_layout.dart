import 'package:flutter/material.dart';
import 'package:mongo_dart/mongo_dart.dart' hide State;

class project_large_Screen extends StatefulWidget {
  const project_large_Screen({Key? key}) : super(key: key);
  @override
  _project_large_ScreenState createState() => _project_large_ScreenState();
}

class _project_large_ScreenState extends State<project_large_Screen> {
  final Db _db = Db('mongodb://localhost:27017/material');
  List<Map<String, dynamic>> _data = [];
  bool _editingEnabled = false;
  List<TextEditingController> _controllers = [];
  final bool _isEditing = false;

  @override
  void initState() {
    super.initState();
    _connectToDb();
  }

  void _connectToDb() async {
    await _db.open();
    final collection = _db.collection('project');
    final results = await collection.find().toList();
    setState(() {
      _data = results.map((result) => result).toList();
      _controllers = List.generate(
        _data.length,
        (index) => TextEditingController(),
      );
    });
  }

  void _toggleEditing() {
    setState(() {
      _editingEnabled = !_editingEnabled;
    });
  }

  Future<void> _updateDocument(int index) async {
    final collection = _db.collection('project');
    final document = _data[index];
    final updatedDocument = {
      ...document,
      'project': _controllers[index].text,
    };
    await collection.update(
      where.eq('_id', document['_id']),
      updatedDocument,
    );
  }

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
          rows: _data.asMap().entries.map((entry) {
            final index = entry.key;
            final data = entry.value;
            return DataRow(cells: [
              DataCell(
                TextField(
                        controller: _controllers[index],
                        decoration: InputDecoration(
                          hintText: data['proyecto'],
                        ),
                      )
              ),
              DataCell(
                TextField(
                        controller: _controllers[index],
                        decoration: InputDecoration(
                          hintText: data['Fecha Inicio'],
                        ),
                      ),
              ),
              DataCell(TextField(
                      controller: _controllers[index],
                      decoration: InputDecoration(
                        hintText: data['FechaCierre'],
                      ),
                    ),),
            ]);
          }).toList(),
          sortColumnIndex: 0,
          sortAscending: true,
        ),
      ],
    );
  }
}
