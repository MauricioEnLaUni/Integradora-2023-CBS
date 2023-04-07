import { Routes, Route, BrowserRouter } from 'react-router-dom';

import Layout from './pages/Layout';
import Login from './pages/Login';
import Register from './pages/Register';
import Dashboard from './pages/Dashboard';
import Project from './pages/Material';
import Test from './pages/Test';

function App() {
  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        {/* public routes */}
        <Route path="login" element={<Login />} />
        <Route path="register" element={<Register />} />
        <Route path="dashboard" element={<Dashboard />} />
        <Route path="projects" element={<Project />} />
        <Route path="test" element={<Test />} />
      </Route>
    </Routes>
  );
}

export default App;