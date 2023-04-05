import { useState, useEffect, useContext } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import LoginDto from '../models/Requests/LoginDto';

import axios from '../api/axios';
import AuthContext from '../context/AuthProvider';
const LOGIN_URL = 'User/auth';

const Login = () => {
  const { setAuth } = useContext(AuthContext);

  const navigate = useNavigate();
  const location = useLocation();
  const from = location.state?.from?.pathname || "/login";

  const [user, setUser] = useState('');
  const [pwd, setPwd] = useState('');
  const [errMsg, setErrMsg] = useState('');

  useEffect(() => {
    setErrMsg('');
  }, [user, pwd])

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

    console.log(response);

    const accessToken = response?.data;
    setAuth({ user, pwd, accessToken });
    setUser('');
    setPwd('');
    navigate(from, { replace: true });
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
    <section>
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
          <a href="#">¡Inscíbase!</a>
        </span>
      </p>
    </section>
  )
}

export default Login