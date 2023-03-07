class NewEmployeeDto {
  String name;
  String last;
  ContactDto contacts;
  DateTime dob;
  String curp;
  String rfc;
  NewJobDto charges;

  NewEmployeeDto(
    this.name,
    this.last,
    this.curp,
    this.rfc,
    this.dob,
    this.contacts,
    this.charges
  );
}

class ContactDto {
  AddressDto address;
  String? phone;
  String email;

  ContactDto(
    this.email,
    this.address,
    [this.phone]
  );
}

class AddressDto {
  String? street;
  String? number;
  String? colony;
  String? zipCode;
  String? city;
  String? state;
  String? country;

  AddressDto(
    [this.street,
    this.number,
    this.colony,
    this.zipCode,
    this.city,
    this.state,
    this.country]
  );
}

class NewJobDto {
  String name;
  NewSalaryDto salaryHistory;
  String role;
  String? area;
  String? department;
  List<String> responsability;

  NewJobDto(
    this.name,
    this.role,
    this.responsability,
    this.salaryHistory,
    [this.area,
    this.department]
  );
}

class NewSalaryDto {
  double rate;

  NewSalaryDto(this.rate);
}