import FictDashBoardButton from '../components/FictDashboardButton';
import AccountBalanceWalletIcon from '@mui/icons-material/AccountBalanceWallet';
import BusinessCenterIcon from '@mui/icons-material/BusinessCenter';
import RecentActorsIcon from '@mui/icons-material/RecentActors';
import HandymanIcon from '@mui/icons-material/Handyman';
import Container from '@mui/material/Container';
import FictDashboardButton from '../components/FictDashboardButton';

const Dashboard = () => {
  const BUTTONS: Array<FictDashBoardButton> = [
    new FictDashBoardButton('Projects', '/projects', {muiName: 'BusinessCenterIcon'} ),
    new FictDashBoardButton('Accounts', '/accounts', {muiName: 'AccountBalanceWalletIcon'} ),
    new FictDashBoardButton('People', '/people', {muiName: 'RecentActorsIcon'}),
    new FictDashBoardButton('Material', '/material', {muiName: 'HandymanIcon'})
  ];

  return(
    <Container>
      {FictDashboardButton.DrawDashboard(BUTTONS)}
    </Container>
  );
  
}

export default Dashboard;