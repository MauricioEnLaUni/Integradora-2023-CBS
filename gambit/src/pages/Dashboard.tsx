import React, { useState, useEffect, useRef } from 'react';
import { Router } from 'react-router-dom';
import FictDashBoardButton from '../components/FictDashboardButton';

const Dashboard = () => {
  const BUTTONS: Array<FictDashBoardButton> = [
    new FictDashBoardButton('/projects'),
    new FictDashBoardButton('/people'),
    new FictDashBoardButton('/accounts'),
    new FictDashBoardButton('/material')
  ];

  
}

export default Dashboard;