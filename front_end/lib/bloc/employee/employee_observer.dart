import 'package:bloc/bloc.dart';

class EmployeeObserver extends BlocObserver {
  const EmployeeObserver();

  @override
  void onChange(BlocBase<dynamic> bloc, Change<dynamic> change) {
    super.onChange(bloc, change);

    print('${bloc.runtimeType} $change');
  }
}