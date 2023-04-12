// React
import { useState } from 'react';
// MUI
import Container from '@mui/material/Container';
// Fictichos
import FictDashBoardButton from '../components/FictDashboardButton';
import FictDashboardButton from '../components/FictDashboardButton';

import useAuth from '../hooks/useAuth';

const Dashboard = () => {
  const { auth } = useAuth();
  const allowedRoles = new Set(['manager','admin']);

  const BUTTONS: Array<FictDashBoardButton> = [
    new FictDashBoardButton('Projects', '/projects', {muiName: 'business_center'} ),
    new FictDashBoardButton('Accounts', '/accounts', {muiName: 'account_balance_wallet'} ),
    new FictDashBoardButton('Material', '/material', {muiName: 'handyman'}),
    new FictDashBoardButton('People', '/clients', {muiName: 'recent_actors'})
  ];
  //@ts-ignore
  if (!auth.claims.some((claim : { claim: any }) => allowedRoles.has(claim.value)))
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