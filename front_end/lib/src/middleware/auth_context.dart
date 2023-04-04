import 'package:dio/dio.dart';
import 'package:dio_cookie_manager/dio_cookie_manager.dart';
import 'package:cookie_jar/cookie_jar.dart';

class ApiService {
  static Dio dio = Dio();

  static void init() {
    var cookieJar = CookieJar();
    dio.interceptors.add(CookieManager(cookieJar));
  }

  static Future<Response> fetchData() async {
    var response = await dio.get('https://example.com');
    return response;
  }

  static Future<Response> sendData(Map<String, dynamic> data) async {
    var response = await dio.post('https://example.com', data: data);
    return response;
  }
}
