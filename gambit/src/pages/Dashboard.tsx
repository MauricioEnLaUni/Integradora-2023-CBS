import FictDashBoardButton from '../components/FictDashboardButton';
import Container from '@mui/material/Container';
import FictDashboardButton from '../components/FictDashboardButton';
import useAuth from '../hooks/useAuth';

const Dashboard = () => {
  const { auth } = useAuth();
  const allowedRoles = new Set(['overseer', 'manager', 'admin']);
  //@ts-ignore
  const claims = auth?.accessToken.claims?.filter((claim : { claim: any}) => claim.type === 'role');

  const BUTTONS: Array<FictDashBoardButton> = [
    new FictDashBoardButton('Projects', '/projects', {muiName: 'business_center'} ),
    new FictDashBoardButton('Accounts', '/accounts', {muiName: 'account_balance_wallet'} ),
    new FictDashBoardButton('Material', '/material', {muiName: 'handyman'}),
    new FictDashBoardButton('People', '/clients', {muiName: 'recent_actors'})
  ];
  //@ts-ignore
  if (!claims.some((claim : { claim: any }) => allowedRoles.has(claim.value)))
  {
    BUTTONS.splice(3, 3);
  }

  return(
    <Container>
      {FictDashboardButton.DrawDashboard(BUTTONS)}
    </Container>
  );
  
}

export default Dashboard;