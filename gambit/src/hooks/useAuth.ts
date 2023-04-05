import { useContext } from "react";
import AuthContext from "../context/AuthProvider";

const useAuth: any = () => {
  return useContext(useAuth);
}

export default useAuth;