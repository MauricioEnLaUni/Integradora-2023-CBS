abstract class Constants {
  static const String appUrl = String.fromEnvironment(
    'app_url',
    defaultValue: '',
  );

  static const String appTitle = String.fromEnvironment(
    'app_title',
    defaultValue: '',
  );
}
