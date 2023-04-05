import FictDashBoardButton from '../components/FictDashboardButton';
import Container from '@mui/material/Container';
import FictDashboardButton from '../components/FictDashboardButton';

const Dashboard = () => {
  const BUTTONS: Array<FictDashBoardButton> = [
    new FictDashBoardButton('Projects', '/projects', {muiName: 'business_center'} ),
    new FictDashBoardButton('Accounts', '/accounts', {muiName: 'account_balance_wallet'} ),
    new FictDashBoardButton('People', '/people', {muiName: 'recent_actors'}),
    new FictDashBoardButton('Material', '/material', {muiName: 'handyman'})
  ];

  return(
    <Container>
      {FictDashboardButton.DrawDashboard(BUTTONS)}
    </Container>
  );
  
}

export default Dashboard;