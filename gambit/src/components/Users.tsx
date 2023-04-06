import { useState, useEffect } from 'react';
import useAxiosPrivate from '../hooks/useAxiosPrivate';
import { useNavigate, useLocation } from 'react-router-dom';
import UserInfoDto from '../models/Response/User/UserInfoDto';

const Users = () => {
  const [users, setUsers] = useState<Array<UserInfoDto>>();
  const axiosPrivate = useAxiosPrivate();
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    let isMounted = true;
    const controller = new AbortController();

    const getUsers = async () => {
      try
      {
        const response = await axiosPrivate.get('/users', {
          signal: controller.signal
        });
        isMounted && setUsers(response.data);
      }
      catch(e: any)
      {
        console.error(e);
        navigate('/login', { state: { from: location }, replace: true });
      }
    }

    getUsers();
    return () => {
      isMounted = false;
      controller.abort();
    }
  }, []);

  return (
    <article>
      <h2>Users List</h2>
      {users?.length
      ? (
      <ul>
        {users.map((user, i) => <li key={i}>{user?.Name}</li>)}
      </ul>) : <p>No users to display</p>}
    </article>
  )
}

export default Users