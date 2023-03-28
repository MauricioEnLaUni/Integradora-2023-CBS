import 'package:flutter/material.dart';
import 'package:intl/intl.dart' as intl;

class FTask{
  final String title;
  final String description;
  final List<String> assigned;
  final DateTime starts;
  final DateTime ends;

  FTask({
    required this.title,
    required this.assigned,
    required this.starts,
    required this.ends,
    required this.description
  });
}

class GanttDiagram extends StatefulWidget {
  @override
  _GanttDiagramState createState() => _GanttDiagramState();
}

class _GanttDiagramState extends State<GanttDiagram> {
  final List<FTask> _tasks = [
    FTask(
      title: "Testo",
      description: "This is a test.",
      assigned: ["16516489", "15616418"],
      starts: DateTime.now(),
      ends: DateTime(2023,4,1)
    ),
    FTask(
      title: "Testo",
      description: "This is a test.",
      assigned: ["16516489", "15616418"],
      starts: DateTime.now(),
      ends: DateTime(2023,4,1)
    ),
    FTask(
      title: "Testo",
      description: "This is a test.",
      assigned: ["16516489", "15616418"],
      starts: DateTime.now(),
      ends: DateTime(2023,4,1)
    ),
    FTask(
      title: "Testo",
      description: "This is a test.",
      assigned: ["16516489", "15616418"],
      starts: DateTime.now(),
      ends: DateTime(2023,4,1)
    ),
    FTask(
      title: "Testo",
      description: "This is a test.",
      assigned: ["16516489", "15616418"],
      starts: DateTime.now(),
      ends: DateTime(2023,4,1)
    ),
    FTask(
      title: "Testo",
      description: "This is a test.",
      assigned: ["16516489", "15616418"],
      starts: DateTime.now(),
      ends: DateTime(2023,4,1)
    ),
    FTask(
      title: "Testo",
      description: "This is a test.",
      assigned: ["16516489", "15616418"],
      starts: DateTime.now(),
      ends: DateTime(2023,4,1)
    ),
    FTask(
      title: "Testo",
      description: "This is a test.",
      assigned: ["16516489", "15616418"],
      starts: DateTime.now(),
      ends: DateTime(2023,4,1)
    ),
    FTask(
      title: "Testo",
      description: "This is a test.",
      assigned: ["16516489", "15616418"],
      starts: DateTime.now(),
      ends: DateTime(2023,4,1)
    ),
    FTask(
      title: "Testo",
      description: "This is a test.",
      assigned: ["16516489", "15616418"],
      starts: DateTime.now(),
      ends: DateTime(2023,4,1)
    ),
    FTask(
      title: "Testo",
      description: "This is a test.",
      assigned: ["16516489", "15616418"],
      starts: DateTime.now(),
      ends: DateTime(2023,4,1)
    ),
    FTask(
      title: "Testo",
      description: "This is a test.",
      assigned: ["16516489", "15616418"],
      starts: DateTime.now(),
      ends: DateTime(2023,4,1)
    ),
    FTask(
      title: "Testo",
      description: "This is a test.",
      assigned: ["16516489", "15616418"],
      starts: DateTime.now(),
      ends: DateTime(2023,4,1)
    ),
    FTask(
      title: "Testo",
      description: "This is a test.",
      assigned: ["16516489", "15616418"],
      starts: DateTime.now(),
      ends: DateTime(2023,4,1)
    ),
    FTask(
      title: "Testo",
      description: "This is a test.",
      assigned: ["16516489", "15616418"],
      starts: DateTime.now(),
      ends: DateTime(2023,4,1)
    ),
  ];

  late ScrollController _scrollController;

  @override
  void initState() {
    super.initState();
    _scrollController = ScrollController();
    _scrollController.addListener(_scrollListener);
  }

  @override
  void dispose() {
    _scrollController.removeListener(_scrollListener);
    _scrollController.dispose();
    super.dispose();
  }

  void _scrollListener() {
    if (_scrollController.offset >= _scrollController.position.maxScrollExtent
      && !_scrollController.position.outOfRange) {
      setState(() {
        _tasks.addAll([
          FTask(
            title: "Testo",
            description: "This is a test.",
            assigned: ["16516489", "15616418"],
            starts: DateTime.now(),
            ends: DateTime(2023,4,1)
          ),
          FTask(
            title: "Testo",
            description: "This is a test.",
            assigned: ["16516489", "15616418"],
            starts: DateTime.now(),
            ends: DateTime(2023,4,1)
          ),
        ]);
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
        home: Scaffold(
          body: ListView.builder(
          controller: _scrollController,
          itemCount: _tasks.length,
          itemBuilder: (context, index) {
            final task = _tasks[index];
            return ListTile(
              title: Text(task.title),
              subtitle: Text(task.description),
              trailing: Text(intl.DateFormat.yMd().format(task.starts)),
            );
          },
        ),
      ),
    );
  }
}