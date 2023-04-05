import React from 'react';
import ReactDOM from 'react-dom';
import { Routes, Route } from 'react-router-dom';

import './index.css';

/* Pages */
import Layout from './pages/Layout';
import Login from './pages/Login';
import Register from './pages/Register';
import Dashboard from './pages/Dashboard';
import Unauthorized from './pages/Errors/Unauthorized';

/* Components */
import RequireAuth from './components/RequireAuth';
import Project from './pages/Project';

const App: React.FC = () => (
  <Routes>
    <Route path="/" element={<Layout />}>
      {/* Public Routes */}
      <Route path="login" element={<Login />}/>
      <Route path="register" element={<Register />}/>
      <Route path="unauthorized" element={<Unauthorized />} />
      <Route path="dashboard" element={<Dashboard />}/>
      <Route path="projects" element={<Project />}></Route>

      {/* Protected routes */}
      <Route element={<RequireAuth allowedRoles={[100]}/>}>
        
      </Route>
    </Route>
  </Routes>
);
// Main
// Project <Route path="project" element={<Project />}>
// People <Route path="people" element={<People />}>
// Material <Route path="material" element={<Material />}>
// Accounts <Route path="material" element={<Material />}>

// Extras
// User
// Settings

export default App;