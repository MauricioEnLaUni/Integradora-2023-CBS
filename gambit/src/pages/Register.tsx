import { useRef, useState, useEffect } from "react";
import { CheckCircle, Close, Info } from '@mui/icons-material';
import axios from '../api/axios';

const USER_REGEX = /^[A-z][A-z0-9-_]{3,23}$/;
const PWD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,24}$/;
const EML_REGEX = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i;
const REGISTER_URL = '/register';

const Register = () => {
    const [user, setUser] = useState('');
    const [validName, setValidName] = useState(false);
    const [userFocus, setUserFocus] = useState(false);

    const [pwd, setPwd] = useState('');
    const [validPwd, setValidPwd] = useState(false);
    const [pwdFocus, setPwdFocus] = useState(false);

    const [matchPwd, setMatchPwd] = useState('');
    const [validMatch, setValidMatch] = useState(false);
    const [matchFocus, setMatchFocus] = useState(false);

    const [owner, setOwner] = useState('');
    const [ownerFocus, setOwnerFocus] = useState(false);

    const [email, setEmail] = useState('');
    const [validEmail, setValidEmail] = useState(false);
    const [emailFocus, setEmailFocus] = useState(false);

    const [errMsg, setErrMsg] = useState('');
    const [success, setSuccess] = useState(false);

    useEffect(() => {
        setValidName(USER_REGEX.test(user));
    }, [user]);

    useEffect(() => {
        setValidPwd(PWD_REGEX.test(pwd));
        setValidMatch(pwd === matchPwd);
    }, [pwd, matchPwd]);

    useEffect(() => {
        setErrMsg('');
    }, [user, pwd, matchPwd]);

    useEffect(() => {
        setValidEmail(EML_REGEX.test(email));
    }, [email])

    const handleSubmit = async (e: any) => {
        e.preventDefault();
        // if button enabled with JS hack
        const v1 = USER_REGEX.test(user);
        const v2 = PWD_REGEX.test(pwd);
        const v3 = EML_REGEX.test(email);
        if (!v1 || !v2 || !v3) {
            setErrMsg("Bad Request");
            return;
        }
        const DTO = new LoginDto(user, pwd, email, owner);
        try {
            const response = await axios.post(REGISTER_URL,
                JSON.stringify(DTO),
                {
                    headers: { 'Content-Type': 'application/json' },
                    withCredentials: true
                }
            );
            setUser('');
            setPwd('');
            setMatchPwd('');
            setOwner('');
            setEmail('');
        } catch (err: any) {
            if (!err?.response) {
                setErrMsg('El servidor no ha respondido');
            } else if (err.response?.status === 409) {
                setErrMsg('Username Taken');
            } else {
                setErrMsg('Registration Failed')
            }
        }
    }

    return (
        <>
            {success ? (
                <section>
                    <h1>¡Éxito!</h1>
                    <p>
                        Por favor espere a que su Cuenta de Usuario sea activada por el administrador del sitio.
                        <a href="#">Acceder</a>
                    </p>
                </section>
            ) : (
                <section>
                    <p className={errMsg ? "errmsg" : "offscreen"} aria-live="assertive">{errMsg}</p>
                    <h1>Registro</h1>
                    <form onSubmit={handleSubmit}>
                        <label htmlFor="username">
                            Usuario:
                            <CheckCircle className={validName ? "valid" : "hide"} />
                            <Close className={validName || !user ? "hide" : "invalid"} />
                        </label>
                        <input
                            type="text"
                            id="username"
                            autoComplete="off"
                            onChange={(e) => setUser(e.target.value)}
                            value={user}
                            required
                            aria-invalid={validName ? "false" : "true"}
                            aria-describedby="uidnote"
                            onFocus={() => setUserFocus(true)}
                            onBlur={() => setUserFocus(false)}
                        />
                        <p id="uidnote" className={userFocus && user && !validName ? "instructions" : "offscreen"}>
                            <Info />
                            4 a 24 caracteres.<br />
                            Debe empezar con letra.<br />
                            Letras, números, guiones y guiones bajos permitidos.
                        </p>


                        <label htmlFor="password">
                            Contraseña:
                            <CheckCircle className={validPwd ? "valid" : "hide"} />
                            <Close className={validPwd || !pwd ? "hide" : "invalid"} />
                        </label>
                        <input
                            type="password"
                            id="password"
                            onChange={(e) => setPwd(e.target.value)}
                            value={pwd}
                            required
                            aria-invalid={validPwd ? "false" : "true"}
                            aria-describedby="pwdnote"
                            onFocus={() => setPwdFocus(true)}
                            onBlur={() => setPwdFocus(false)}
                        />
                        <p id="pwdnote" className={pwdFocus && !validPwd ? "instructions" : "offscreen"}>
                            <Info />
                            8 a 24 caracteres.<br />
                            Debe contener mayúscula, minúscula, un número y un caracter especial.<br />
                            Se permiten: <span aria-label="signo de exclamación">!</span> <span aria-label="arroba">@</span> <span aria-label="gato">#</span> <span aria-label="signo de dolar">$</span> <span aria-label="porcentaje">%</span>
                        </p>


                        <label htmlFor="confirm_pwd">
                            Repita su password:
                            <CheckCircle className={validMatch && matchPwd ? "valid" : "hide"} />
                            <Close className={validMatch || !matchPwd ? "hide" : "invalid"} />
                        </label>
                        <input
                            type="password"
                            id="confirm_pwd"
                            onChange={(e) => setMatchPwd(e.target.value)}
                            value={matchPwd}
                            required
                            aria-invalid={validMatch ? "false" : "true"}
                            aria-describedby="confirmnote"
                            onFocus={() => setMatchFocus(true)}
                            onBlur={() => setMatchFocus(false)}
                        />
                        <p id="confirmnote" className={matchFocus && !validMatch ? "instructions" : "offscreen"}>
                            <Info />
                            Debe coincidir con el campo anterior.
                        </p>
                        <label htmlFor="confirm_pwd">
                            Email:
                            <CheckCircle className={validMatch && matchPwd ? "valid" : "hide"} />
                            <Close className={validMatch || !matchPwd ? "hide" : "invalid"} />
                        </label>
                        <input
                            type="email"
                            id="email"
                            onChange={(e) => setEmail(e.target.value)}
                            value={email}
                            required
                            aria-describedby="emailnote"
                            onFocus={() => setEmailFocus(true)}
                            onBlur={() => setEmailFocus(false)}
                        />
                        <p id="confirmnote" className={matchFocus && !validMatch ? "instructions" : "offscreen"}>
                            <Info />
                            Correo electrónico de la empresa.
                        </p>
                        <label htmlFor="confirm_pwd">
                            Id del empleado:
                            <CheckCircle className={validMatch && matchPwd ? "valid" : "hide"} />
                            <Close className={validMatch || !matchPwd ? "hide" : "invalid"} />
                        </label>
                        <input
                            type="text"
                            id="owner"
                            onChange={(e) => setOwner(e.target.value)}
                            value={owner}
                            required
                            aria-describedby="ownernote"
                            onFocus={() => setOwnerFocus(true)}
                            onBlur={() => setOwnerFocus(false)}
                        />
                        <p id="confirmnote" className={matchFocus && !validMatch ? "instructions" : "offscreen"}>
                            <Info />
                            Introduzca su Id de empleado aquí.
                        </p>

                        <button disabled={!validName || !validPwd || !validEmail
                            || !validMatch ? true : false
                            || owner == ''}>Inscribirse</button>
                    </form>
                    <p>
                        ¿Tiene cuenta?<br />
                        <span className="line">
                            {/*put router link here*/}
                            <a href="#">Acceder</a>
                        </span>
                    </p>
                </section>
            )}
        </>
    )
}

export default Register