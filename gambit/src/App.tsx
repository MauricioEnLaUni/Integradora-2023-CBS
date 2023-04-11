import { Routes, Route, BrowserRouter } from 'react-router-dom';

// Layouts
import PublicRoutes from './components/Outlet/PublicRoute';
import Layout from './pages/Layout';

// Pages
import Login from './pages/Login';
import Register from './pages/Register';
import Dashboard from './pages/Dashboard';
import Project from './pages/Material';
import Test from './pages/Test';
import Protected from './components/Protected';

const ROLES = {
  'User' : 200,
  'Overseer': 400,
  'Manager' : 600,
  'Admin' : 8008
}

function App() {
  return (
    <Routes>
      <Route element={<PublicRoutes />}>
        <Route path="login" element={<Login />} />
        <Route path="register" element={<Register />} />
      </Route>

      {/* Protected Routes */}
      <Route element={<Layout />}>
        <Route element={<Protected allowedRoles={new Set(['user'])}/>}>
          <Route path="/" element={<Dashboard />} />
          <Route path="projects" element={<Project />} />
          <Route path="gantt" element={null} />
          <Route path="material" element={null} />
          <Route path="user" element={null} />
          <Route path="settings" element={null} />
        </Route>

        <Route element={<Protected allowedRoles={new Set(['Overseer'])}/>}>
          <Route path="clients" element={null} />
          <Route path="foreigner" element={null} />
        </Route>

        <Route element={<Protected allowedRoles={new Set(['Manager'])}/>}>
          <Route path="accounts" element={null} />
        </Route>

        <Route element={<Protected allowedRoles={new Set(['Admin'])}/>}>
          <Route path="usrManager" element={null} />
          <Route path="test" element={<Test />} />
        </Route>
      </Route>
      <Route path="*" element={<Login />} />
    </Routes>
  );
}

export default App;