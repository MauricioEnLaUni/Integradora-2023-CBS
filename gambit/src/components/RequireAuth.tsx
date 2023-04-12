import { useLocation, Navigate, Outlet } from 'react-router-dom';
import useAuth from '../hooks/useAuth';
import Claim from '../api/Tokens/Claims';

const RequireAuth = ({ allowedRoles } : { allowedRoles: Set<string>}) => {
  const { auth } = useAuth();
  const location = useLocation();

  return (
    auth?.claims.some((claim : Claim) => allowedRoles.has(claim.value))
      ? <Outlet />
      : auth?.claims
      ? <Navigate to="/" state={{ from: location }} replace />
        : <Navigate to="/login" state={{ from: location }} replace />
  );
}

export default RequireAuth;