import { createContext, useState, ReactNode } from 'react';
import Claim from '../api/Tokens/Claims';

export type AuthContextType = {
  auth: any;
  setAuth: any;
}

export interface Authorization {
  sub: string;
  token: string;
  claims: Set<Claim>;
}

export const AuthContext = createContext<AuthContextType>({} as AuthContextType);

export const AuthProvider = ({children} : {children: ReactNode}) => {
  const [auth, setAuth] = useState<Authorization | null>(null);

  return (
    <AuthContext.Provider value={{ auth, setAuth }}>
      {children}
    </AuthContext.Provider>
  );
};