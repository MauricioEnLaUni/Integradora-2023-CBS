import React from 'react';
import ReactDOM from 'react-dom';
import { Routes, Route, BrowserRouter } from 'react-router-dom';

import './index.css';
import Layout from './pages/Layout';
import Login from './pages/Login';
import Register from './pages/Register';
import Dashboard from './pages/Dashboard';
import RequireAuth from './components/RequireAuth';
import Project from './pages/Material';

function App() {
  const ROLES = {
    'User': 200,
    'Owner': 150,
    'Admin': 100
  }

  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        {/* public routes */}
        <Route path="login" element={<Login />} />
        <Route path="register" element={<Register />} />
        <Route path="dashboard" element={<Dashboard />} />
        <Route path="projects" element={<Project />} />
      </Route>
    </Routes>
  );
}

export default App;