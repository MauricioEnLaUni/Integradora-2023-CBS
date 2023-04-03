import 'package:http/http.dart' as http;

class ProjectController {
  static getProjects() async {
    const String url = 'localhost:5236';

    var res = await http.get(Uri.http(url, 'Project'),
        headers: {"Content-type": "application/json"});
    if (res.statusCode == 200) return res.body;
    return res.statusCode;
  }
}
