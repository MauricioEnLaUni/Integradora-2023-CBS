abstract class Constants {
  static const String appUrl = String.fromEnvironment(
    'appUrl',
    defaultValue: '',
  );

  static const String appTitle = String.fromEnvironment(
    'appTitle',
    defaultValue: '',
  );

  static const String appTheme = String.fromEnvironment(
    'appTheme',
    defaultValue: '',
  );
}
