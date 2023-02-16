import { Routes, Route } from 'react-router-dom';
import Layout from './Components/Layout';

/* Public Routes */
import Login from './Pages/Login';
import PasswordRecovery from './Pages/PasswordRecovery';
import Register from './Pages/Register';

/* Private Routes */
import Dashboard from './Pages/Dashboard';
import UserManagement from './Pages/UserManagement';

/* Catch all */
import Message from './Pages/Error/Message';

function App() {

  return (
    <Routes>
      <Route path='/' element={<Layout />}>

        {/* Public Routes */}
        <Route path='login' element={<Login />} />
        <Route path='recover' element={<PasswordRecovery />} />
        <Route path='register' element={<Register />} />

        {/* Private Routes */}
        <Route path='/' element={<Dashboard />}>
          <Route path='user' element={<UserManagement />} />
        </Route>

        {/* Catch all */}
        <Route path='*' element={<Message payload={404} />}/>
      </Route>

    </Routes>
  );
};

export default App;
