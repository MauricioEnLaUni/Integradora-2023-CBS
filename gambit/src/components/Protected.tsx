// React
import { useEffect, useState } from 'react';
import { Navigate, Outlet } from 'react-router-dom';

import getCookie from '../hooks/useCookie';

interface Claim {
  type: string;
  value: string;
}

const getClaims = (jwt: string) => {
  const [, payloadBase64] = jwt.split('.');
  const payloadJson = Buffer.from(payloadBase64, 'base64').toString();
  const claims = JSON.parse(payloadJson) as Claim[];
  return new Set<Claim>(claims);
}

const Protected = ({ allowedRoles } : { allowedRoles: Set<string>}) => {
  const [roles, setRoles] = useState('');
  const [claims, setClaims] = useState<Set<Claim>>(new Set());
  setRoles(getCookie("fcl") || '');

  useEffect(() => {
    if (roles != '') {
      const claims = getClaims(roles);
      setClaims(claims);
    }
  }, [roles]);
  
  return (
    Array.from(claims).some((claim : Claim) => allowedRoles.has(claim.value))
      ? <Outlet />
      : roles != ''
      ? <Navigate to="/" state={{ from: location }} replace />
        : <Navigate to="/login" state={{ from: location }} replace />
  );
}

export default Protected;