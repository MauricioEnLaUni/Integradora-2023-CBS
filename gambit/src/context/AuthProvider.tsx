import { createContext, useState, ReactNode } from 'react';

interface AuthContextType {
  auth: Record<string, unknown>;
  setAuth: React.Dispatch<React.SetStateAction<Record<string, unknown>>>;
}

const AuthContext = createContext<AuthContextType>({ auth: {}, setAuth: () => {} });

export const AuthProvider = ({children} : {children: ReactNode}) => {
  const [auth, setAuth] = useState({});

  return (
    <AuthContext.Provider value={{ auth, setAuth }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;