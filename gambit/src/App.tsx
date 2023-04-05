import React from 'react';
import ReactDOM from 'react-dom';
import { Routes, Route } from 'react-router-dom';
import { ColorModeContext, useMode } from './Theme';
import { CssBaseline, ThemeProvider } from '@mui/material';

import './index.css';

function App() {
  const [theme, colorMode] = useMode();

  return(
    <ColorModeContext.Provider value={colorMode}>
      <ThemeProvider theme={theme}>
        <div className='app'></div>
      </ThemeProvider>
    </ColorModeContext.Provider>
  );
}

export default App;