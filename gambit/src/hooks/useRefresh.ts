import axios from '../api/axios';
import useAuth from './useAuth';

const useRefreshToken = () => {
  const { setAuth } = useAuth();
  
  const refresh = async () => {
    const response = await axios.get('User/refresh', {
      withCredentials: true
    });
    setAuth((prev: any) => {
      return { ...prev, accessToken: response.data }
    });
    return response.data;
  }
  return refresh;
}

export default useRefreshToken;