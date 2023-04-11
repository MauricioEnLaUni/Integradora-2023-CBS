import { useLocation, Navigate, Outlet } from 'react-router-dom';
import useAuth from '../hooks/useAuth';

interface Claim {
  type: string;
  value: string;
}

const RequireAuth = ({ allowedRoles } : { allowedRoles: Set<string>}) => {
  const { auth } = useAuth();
  const location = useLocation();

  //@ts-ignore
  const claims = auth?.accessToken.claims?.filter((claim : { claim: Claim}) => claim.type === 'role');

  return(
    //@ts-ignore
    claims.some((claim : { claim: any }) => allowedRoles.has(claim.value))
      ? <Outlet />
      : auth?.accessToken
      ? <Navigate to="/" state={{ from: location }} replace />
        : <Navigate to="/login" state={{ from: location }} replace />
  );
}

export default RequireAuth;