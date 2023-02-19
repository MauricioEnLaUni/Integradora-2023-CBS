import React from 'react';
import { useRef, useState, useEffect } from 'react';

import axios from '../api/axios';

// Material Components
import { styled } from '@mui/system';

const USER_REGEX = /^[a-zA-Z][a-zA-Z0-9-_]{4,27}$/;
const PWD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,64}$/;

const REGISTER_URL = '/register';

const Register = () => {
  const userRef = useRef<any>();
  const errRef = useRef<any>();

  const [user, setUser] = useState('');
  const [validUser, setValidUser] = useState(false);
  const [userFocus, setUserFocus] = useState(false);

  const [password, setPassword] = useState('');
  const [validPassword, setValidPassword] = useState(false);
  const [passwordFocus, setPasswordFocus] = useState(false);
  
  const [repeat, setRepeat] = useState('');
  const [repeatFocus, setRepeatFocus] = useState(false);
  const [repeatValid, setRepeatValid] = useState(false);

  const [errMsg, setErrMsg] = useState('');
  const [success, setSuccess] = useState(false);

  useEffect(() => {
    userRef.current.focus();
  }, []);

  useEffect(() => {
    setValidUser(USER_REGEX.test(user));
  }, [user]);

  useEffect(() => {
    setValidPassword(PWD_REGEX.test(password));
    setRepeatValid(password === repeat);
  }, [password, repeat]);

  useEffect(() => {
    setErrMsg('');
  }, [user, password, repeat]);

  const handleSubmit = async (e: any) => {
    e.preventDefault();

    const v1 = USER_REGEX.test(user);
    const v2 = PWD_REGEX.test(password);
    if(!v1 || !v2) {
      setErrMsg('Entrada inválida');
      return;
    }
    try {
      const response = await axios.post(REGISTER_URL,
        JSON.stringify({ user, password }),
        {
          headers: { 'Content-Type': 'application/json' },
          withCredentials: true
        });
        console.log(response.data);
        setSuccess(true);
        setUser('');
        setPassword('');
    } catch(err: any) {
      if(!err?.response) {
        setErrMsg('No server response');
      } else if (err.response?.status === 409) {
        setErrMsg('User name taken.');
      } else {
        setErrMsg('Registration failed.');
      }
      errRef.current.focus();
    }
  }

  return (
    <>
    {success ? (
      <section>
        <h1>Success!</h1>
        <p>
          <a href="#">Sign In</a>
        </p>
      </section>
    ) : (
    <div>
      <ErrorMessage ref={errRef} className={errMsg ? "errmsg" : "offscreen" }
        aria-live="assertive" >
        {errMsg}
      </ErrorMessage>
      <PageTitle>Register</PageTitle>
      <form onSubmit={handleSubmit}>
        <label htmlFor="username">
          <p>Username:</p>
        </label>
        <input
          type="text"
          id="username"
          ref={userRef}
          autoComplete="off"
          onChange={(e) => setUser(e.target.value)}
          required
          aria-invalid={validUser ? "false" : "true"}
          aria-describedby="user-note"
          onFocus={() => setUserFocus(true)}
          onBlur={() => setUserFocus(false)}
        />
        <p
          id="user-note"
          className={
            userFocus && user && !validUser ? "instructions" : "offscreen"
          }
        >
          4 a 27 Caracteres.<br />
          Debe iniciar con letra.<br />
          Letras, números, guión bajo y guiones permitidos.
        </p>
        <label htmlFor="password">
          <p>Password:</p>
        </label>
        <input
          type="password"
          id="password"
          onChange={(e) => setPassword(e.target.value)}
          required
          aria-invalid={validPassword ? "false" : "true"}
          aria-describedby="password-note"
          onFocus={() => setPasswordFocus(true)}
          onBlur={() => setPasswordFocus(false)}
        />
        <p
          id="password-note"
          className={
            passwordFocus && !validPassword ? "instructions" : "offscreen"
          }
        >
          8 a 64 Caracteres.<br />
          Requiere una mayúscula, una minúscula, un número y un caracter especial.<br />
          Caracteres especiales permitidos !@$%#
        </p>
        <label htmlFor="repeat_password">
          Confirme su Password:
        </label>
        <input
          type="password"
          id="repeat_password"
          onChange={(e) => setRepeat(e.target.value)}
          required
          aria-invalid={repeatValid ? "false" : "true"}
          aria-describedby="repeat-note"
          onFocus={() => setRepeatFocus(true)}
          onBlur={() => setRepeatFocus(false)}
        />
        <p id="repeat-note" className={repeatFocus && !repeatValid ? "instructions" : "offscreen"}>
          Debe ser igual a su password.
        </p>

        <button disabled={!validPassword || !validUser || !repeatValid ? true : false}>
          Inscribirse
        </button>
      </form>
      <p>
        ¿Ya tiene cuenta?<br />
        <span>Ingresar</span>
      </p>
    </div>
    )}
    </>
  )
}

const PageTitle = styled('h2')({
  color: 'darkslategray',
});

const ErrorMessage = styled('section')({
  color: 'white',
});

export default Register;