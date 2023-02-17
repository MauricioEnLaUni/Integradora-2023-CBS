import React from 'react';
import { useRef, useState, useEffect } from 'react';

// Material Components
import Grid from '@mui/material/Grid';
import Grid2 from '@mui/material/Unstable_Grid2';
import { styled } from '@mui/system';
import Container from '@mui/material/Container';
import Box from '@mui/material/Box';

const USER_REGEX = /^[a-zA-Z][a-zA-Z0-9-_]{4,27}$/;
const PWD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,64}$/;

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

  return (
    <Grid2 container spacing={2} >
      <Grid xs={12}>
        <ErrorMessage ref={errRef} className={errMsg ? "errmsg" : "offscreen" }
          aria-live="assertive" >
          {errMsg}
        </ErrorMessage>
        <PageTitle>Register</PageTitle>
        <form>
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
            aria-describedby="usernote"
            onFocus={() => setUserFocus(true)}
            onBlur={() => setUserFocus(false)}
          />
          <p
            id="usernote"
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
            aria-describedby="passwordnote"
            onFocus={() => setPasswordFocus(true)}
            onBlur={() => setPasswordFocus(false)}
          />
          <p
            id="passwordnote"
            className={
              passwordFocus && !validPassword ? "instructions" : "offscreen"
            }
          >
            8 a 64 Caracteres.<br />
            Requiere una mayúscula, una minúscula, un número y un caracter especial.<br />
            Caracteres especiales permitidos !@$%#
          </p>
        </form>
      </Grid>
    </Grid2>
  )
}

const PageTitle = styled('h2')({
  color: 'darkslategray',
});

const ErrorMessage = styled('section')({
  color: 'white',
});

export default Register;