import 'package:flutter/material.dart';
import 'package:mongo_dart/mongo_dart.dart' hide State;

class material_large_Screen extends StatefulWidget {
  const material_large_Screen({Key? key}) : super(key: key);
  @override
  _material_large_ScreenState createState() => _material_large_ScreenState();
}

class _material_large_ScreenState extends State<material_large_Screen> {
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
    final collection = _db.collection('material');
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
    final collection = _db.collection('material');
    final document = _data[index];
    final updatedDocument = {
      ...document,
      'material': _controllers[index].text,
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
            DataColumn(label: Text('material')),
            DataColumn(label: Text('cantidad')),
            DataColumn(label: Text('no se')),
            DataColumn(label: Text('No se')),
          ],
          rows: _data.asMap().entries.map((entry) {
            final index = entry.key;
            final data = entry.value;
            return DataRow(cells: [
              DataCell(TextField(
                        controller: _controllers[index],
                        decoration: InputDecoration(
                          hintText: data['nombre'],
                        ),
                      ),
              ),
              DataCell( TextField(
                        controller: _controllers[index],
                        decoration: InputDecoration(
                          hintText: data['cantidad'],
                        ),
                      )
              ),
              DataCell(TextField(
                      controller: _controllers[index],
                      decoration: InputDecoration(
                        hintText: data['no se'],
                      ),
                    ),)
            ]);
          }).toList(),
          sortColumnIndex: 0,
          sortAscending: true,
        ),
      ],
    );
  }
}
