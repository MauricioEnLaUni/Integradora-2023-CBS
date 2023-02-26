import { useState } from 'react';
import { ThemeProvider } from 'styled-components';
import { Routes, Route } from 'react-router-dom';
import Layout from './Components/Layout';

import './App.css';

import { GlobalStyles } from './Theme/Global';
import { lightTheme, darkTheme } from './Theme/Global';

/* Public Routes */
import Login from './Pages/Login';
import PasswordRecovery from './Pages/PasswordRecovery';
import Register from './Pages/Register';

/* Private Routes */
import Dashboard from './Pages/Dashboard';
import UserManagement from './Pages/UserManagement';

/* Catch all */
import Message from './Pages/Error/Message';
import Templater from './Pages/Templater';

function App() {
  const [theme, setTheme] = useState(true);

  return (
    <ThemeProvider theme={ theme ? darkTheme : lightTheme }>
      <GlobalStyles />
      <Routes>
        <Route path='/' element={<Layout />}>

          {/* Public Routes */}
          <Route path='login' element={<Login />} />
          <Route path='recover' element={<PasswordRecovery />} />
          <Route path='register' element={<Register />} />
          <Route path='templates' element={<Templater />}/>

          {/* Private Routes */}
          <Route path='/' element={<Dashboard />}>
            <Route path='user' element={<UserManagement />} />
          </Route>

          {/* Catch all */}
          <Route path='*' element={<Message payload={404} />}/>
        </Route>

      </Routes>
    </ThemeProvider>
  );
};

export default App;
