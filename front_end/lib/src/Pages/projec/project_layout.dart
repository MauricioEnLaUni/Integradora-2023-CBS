import 'package:flutter/material.dart';
import 'package:mongo_dart/mongo_dart.dart' hide State;

class person_large_Screen extends StatefulWidget {
  const person_large_Screen({Key? key}) : super(key: key);
  @override
  _person_large_ScreenState createState() => _person_large_ScreenState();
}

class _person_large_ScreenState extends State<person_large_Screen> {
  final Db _db = Db('mongodb://localhost:27017/prueba');
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
    final collection = _db.collection('personas');
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
    final collection = _db.collection('personas');
    final document = _data[index];
    final updatedDocument = {
      ...document,
      'nombre': _controllers[index].text,
    };
    await collection.update(
      where.eq('_id', document['_id']),
      updatedDocument,
    );
  }

  @override
  Widget build(BuildContext context) {
    return DataTable(
      columns: const [
        DataColumn(label: Text('Nombre')),
        DataColumn(label: Text('Proyecto')),
        DataColumn(label: Text('puesto')),
        DataColumn(label: Text('No se')),
        DataColumn(label: Text('No se')),
        DataColumn(label: Text('Acciones')),
      ],
      rows: _data.asMap().entries.map((entry) {
        final index = entry.key;
        final data = entry.value;
        return DataRow(cells: [
          DataCell(
            _editingEnabled
                ? TextField(
                    controller: _controllers[index],
                    decoration: InputDecoration(
                      hintText: data['nombre'],
                    ),
                  )
                : Text(data['nombre']),
          ),
          DataCell(
            _editingEnabled
                ? TextField(
                    controller: _controllers[index],
                    decoration: InputDecoration(
                      hintText: data['proyecto'],
                    ),
                  )
                : Text(data['proyecto']),
          ),
          DataCell(_editingEnabled
              ? TextField(
                  controller: _controllers[index],
                  decoration: InputDecoration(
                    hintText: data['puesto'],
                  ),
                )
              : Text(data['puesto'])),
          DataCell(_isEditing
              ? TextField(
                  controller:
                      TextEditingController(text: data['no_se_1'].toString()),
                  onChanged: (value) {
                    setState(() {
                      data['no_se_1'] = int.parse(value);
                    });
                  },
                )
              : Text(data['no_se_1'].toString())),
          DataCell(_isEditing
              ? TextField(
                  controller:
                      TextEditingController(text: data['no_se_2'].toString()),
                  onChanged: (value) {
                    setState(() {
                      data['no_se_2'] = int.parse(value);
                    });
                  },
                )
              : Text(data['no_se_2'].toString())),
        ]);
      }).toList(),
      sortColumnIndex: 0,
      sortAscending: true,
    );
  }
}
