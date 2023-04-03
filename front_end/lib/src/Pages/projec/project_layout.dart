import 'package:flutter/material.dart';
import 'package:mongo_dart/mongo_dart.dart' hide State;
import 'package:http/http.dart' as http;

class project_large_Screen extends StatefulWidget {
  const project_large_Screen({Key? key}) : super(key: key);
  @override
  _project_large_ScreenState createState() => _project_large_ScreenState();
}

class ProjectController {
  static getProject() async {
    const String url = 'localhost:5236';

    var res = await http.get(Uri.http(url, 'Project'),
        headers: {"Content-type": "application/json"});
    if (res.statusCode == 200) return res.body;
    return res.statusCode;
  }
}

class _project_large_ScreenState extends State<project_large_Screen> {
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
        DataColumn(label: Text('persona a cargo')),
        DataColumn(label: Text('puesto')),
        DataColumn(label: Text('No se')),
        DataColumn(label: Text('Cuentas +-')),
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
