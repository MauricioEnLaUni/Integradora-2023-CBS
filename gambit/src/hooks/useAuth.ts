import { useContext, useDebugValue } from "react";
import AuthContext from "../context/AuthProvider";

const useAuth: any = () => {
  const { auth }: { auth: any } = useContext(AuthContext);
  useDebugValue(auth, (auth: { user: any; }) => auth?.user ? "Logged In" : "Logged Out")
  return useContext(useAuth);
}

export default useAuth;