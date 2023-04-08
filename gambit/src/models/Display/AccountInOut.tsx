class AccountInOut {
  id: string;
  name: string;
  entry: number;
  exit: number;

  constructor(id: string, name: string, entry: number, exit: number)
  {
    this.id = id;
    this.name = name;
    this.entry = entry;
    this.exit = exit;
  }
}

export default AccountInOut;