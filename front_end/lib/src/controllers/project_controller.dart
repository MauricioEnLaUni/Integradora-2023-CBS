import 'dart:convert';

import 'package:http/http.dart' as http;

import '../models/project_model.dart';

class ProjectController {
  static getProjects() async {
    const String url = 'localhost:5236';

    var res = await http.get(Uri.http(url, 'Project'),
        headers: {"Content-type": "application/json"});
    if (res.statusCode == 200) return fromJson(res.body);
    return res.statusCode;
  }

  static getProjectId(id) async {
    const String url = 'localhost:5236';

    var res = await http.get(Uri.http(url, 'Project/$id'),
      headers: { "Content-type": "application/json" });
    if (res.statusCode == 200) return fromJson(res.body);
    return res.statusCode;
  }

  static List<ProjectDto> fromJson(String jsonString) {
    final dynamic data = json.decode(jsonString);
    return List<ProjectDto>.from(
      data.map((dynamic item) => ProjectDto.fromJson(item)),
    );
  }
}
