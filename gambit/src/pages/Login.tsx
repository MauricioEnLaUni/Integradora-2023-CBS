import { useState, useEffect} from 'react';
import { Link, useNavigate, useLocation, Navigate } from 'react-router-dom';
import LoginDto from '../models/Requests/loginDto';

import axios from '../api/axios';
import useAuth from '../hooks/useAuth';
import { Authorization } from '../context/AuthProvider';
import getCookie from '../hooks/useCookie';
import getClaims from '../api/Tokens/GetClaims';
import Claim from '../api/Tokens/Claims';

const LOGIN_URL = 'User/auth';

const Login = () => {
  const { auth, setAuth } = useAuth();

  const navigate = useNavigate();
  const location = useLocation();

  const [user, setUser] = useState('');
  const [pwd, setPwd] = useState('');
  const [errMsg, setErrMsg] = useState('');

  useEffect(() => {
    setErrMsg('');
  }, [user, pwd]);

  const handleSubmit = async (e: any) => {
    e.preventDefault();

    try {
      const DTO: LoginDto = new LoginDto(user, pwd);
      const response = await axios.post(LOGIN_URL,
        JSON.stringify(DTO),
        {
          headers: { 'Content-Type': 'application/json' },
          withCredentials: true
        }
    );

    setUser('');
    setPwd('');
    const sub = response?.data?.sub;
    const token = response?.data?.token;
    const claims = response?.data?.claims;
    const auther = { sub, token, claims } as Authorization;
    setAuth(auther);
    document.cookie = `fid=${token}; path=/; secure; max-age=3600; SameSite=Lax`;
    navigate('/dashboard', { replace: true });
    } catch (err: any) {
      if (!err?.response) {
        setErrMsg('No Server Response');
      } else if (err.response?.status === 400) {
        setErrMsg('Missing Username or Password');
      } else if (err.response?.status === 401) {
        setErrMsg('Unauthorized');
      } else {
        setErrMsg('Login Failed');
      }
    }
  }

  return (
    auth ? <Navigate to='/dashboard' state={{ from: location }} replace />
    : (<section>
      <p  className={errMsg ? "errmsg" : "offscreen"} aria-live="assertive">{errMsg}</p>
      <h1>Acceder</h1>
      <form onSubmit={handleSubmit}>
        <label htmlFor="username">Usuario:</label>
        <input
          type="text"
          id="username"
          autoComplete="off"
          onChange={(e) => setUser(e.target.value)}
          value={user}
          required
        />

        <label htmlFor="password">Contraseña:</label>
        <input
          type="password"
          id="password"
          onChange={(e) => setPwd(e.target.value)}
          value={pwd}
          required
        />
        <button>Acceder</button>
      </form>
      <p>
        ¿No tiene cuenta?<br />
        <span className="line">
          {/*put router link here*/}
          <Link to="/register">¡Inscríbase!</Link>
        </span>
      </p>
    </section>)
  );
}

export default Login;