import { Outlet } from 'react-router-dom';

const PublicRoutes = () => {
  return(
    <main className="App">
      <Outlet />
    </main>
  );
}

export default PublicRoutes;