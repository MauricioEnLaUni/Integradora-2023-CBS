import React from 'react';
import { Routes, Route } from 'react-router-dom';

/* Pages */
import Layout from './pages/Layout';
import Login from './pages/Landing';
import Register from './pages/Register';

const App: React.FC = () => (
  <Routes>
    <Route path="/" element={<Layout />}>
      // Basics
      <Route path="login" element={<Login />}/>
      <Route path="register" element={<Register />}/>

      // Main
      // Project
      // People
      // Material
      // Accounts

      // Extras
      // User
      // Settings
    </Route>
  </Routes>
);

export default App;