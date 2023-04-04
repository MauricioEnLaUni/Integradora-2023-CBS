import 'dart:io';

import 'package:dio/dio.dart';
import 'package:cookie_jar/cookie_jar.dart';
import 'package:path_provider/path_provider.dart';

class AuthMiddleware {
  Future<void> prepareJar() async {
    final dio = Dio();
    final CookieJar = CookieJar();
    final Directory appDocDir = await getApplicationDocumentsDirectory();
    final String appDocPath = appDocDir.path;
    final jar = PersistCookieJar(
      ignoreExpires: false,
      storage: FileStorage("$appDocPath/cookies"),
    );
    dio.interceptors.add(CookieManager(jar));
  }
}
