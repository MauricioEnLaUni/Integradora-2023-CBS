import * as short from 'short-uuid';
import AccountBalanceWalletIcon from '@mui/icons-material/AccountBalanceWallet';

export default class FictDashBoardButton {
  public Id: string = '';
  public Icon: { muiName: string; } = AccountBalanceWalletIcon;
  public Route: string = '';

  constructor(route: string)
  {
    this.Id = short.generate();
    this.Route = route;
  }
}