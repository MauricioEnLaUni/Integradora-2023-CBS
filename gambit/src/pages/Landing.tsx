import { useRef, useState, useEffect, useContext } from 'react';
import AuthContext from "../context/AuthProvider";

import axios from '../api/axios';
const LOGIN_URL = '/auth';

const Login = () => {
  const { setAuth } = useContext(AuthContext);

  const [user, setUser] = useState('');
  const [pwd, setPwd] = useState('');
  const [errMsg, setErrMsg] = useState('');
  const [success, setSuccess] = useState(false);

  useEffect(() => {
    setErrMsg('');
  }, [user, pwd])

  const handleSubmit = async (e: any) => {
    e.preventDefault();

    try {
      const response = await axios.post(LOGIN_URL,
        JSON.stringify({ user, pwd }),
        {
          headers: { 'Content-Type': 'application/json' },
          withCredentials: true
        }
    );

    const accessToken = response?.data?.accessToken;
    const roles = response?.data?.roles;
    setAuth({ user, pwd, roles, accessToken });
    setUser('');
    setPwd('');
    setSuccess(true);
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
    <>
    {success ? (
        <section>
            <h1>You are logged in!</h1>
            <br />
            <p>
                <a href="#">Dashboard</a>
            </p>
        </section>
    ) : (
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
      )}
    </>
  )
}

export default Login