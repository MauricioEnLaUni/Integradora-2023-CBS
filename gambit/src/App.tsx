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
import RequireAuth from './components/RequireAuth';
import CompanyBrowser from './pages/Business/CompanyBrowser';
import CompanyPage from './pages/Business/Company.Page';

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
        <Route element={<RequireAuth allowedRoles={new Set(['user'])}/>}>
          <Route path="Dashboard" element={<Dashboard />} />
          <Route path="projects" element={<Project />} />
        </Route>

        <Route element={<RequireAuth allowedRoles={new Set(['manager', 'admin'])}/>}>
          <Route path="clients" element={<CompanyBrowser />} />
          <Route path="clients/:clients" element={<CompanyPage />} />
        </Route>

        <Route element={<RequireAuth allowedRoles={new Set(['manager', 'admin'])}/>}>
          <Route path="accounts" element={null} />
        </Route>

        <Route element={<RequireAuth allowedRoles={new Set(['admin'])}/>}>
          <Route path="test" element={<Test />} />
        </Route>
      </Route>
      <Route path="*" element={<Login />} />
    </Routes>
  );
}

export default App;

/*

          <Route path="gantt" element={null} />
          <Route path="material" element={null} />
          <Route path="user" element={null} />
          <Route path="settings" element={null} />

          
          <Route path="foreigner" element={null} />

          
          <Route path="usrManager" element={null} />
*/