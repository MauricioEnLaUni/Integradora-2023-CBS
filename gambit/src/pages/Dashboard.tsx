// React
import { ElementType, useState } from 'react';
// MUI
import Container from '@mui/material/Container';
// Fictichos
import FictDashBoardButton from '../components/FictDashboardButton';
import FictDashboardButton from '../components/FictDashboardButton';

import useAuth from '../hooks/useAuth';
import BusinessCenter from '@mui/icons-material/BusinessCenter';
import AccountBalanceWallet from '@mui/icons-material/AccountBalanceWallet';
import Handyman from '@mui/icons-material/Handyman';
import RecentActors from '@mui/icons-material/RecentActors';
import { IconType } from 'react-icons';
import Grid from '@mui/material/Grid';

const Dashboard = () => {
  const { auth } = useAuth();
  const allowedRoles = new Set(['manager','admin']);

  const BUTTONS: Array<FictDashBoardButton> = [
    new FictDashBoardButton('Projects', '/projects', BusinessCenter as ElementType<IconType> ),
    new FictDashBoardButton('Accounts', '/accounts', AccountBalanceWallet as ElementType<IconType> ),
    new FictDashBoardButton('Material', '/material', Handyman as ElementType<IconType>),
    new FictDashBoardButton('People', '/clients', RecentActors as ElementType<IconType>)
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