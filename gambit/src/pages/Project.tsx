import Container from '@mui/material/Container';
import { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import ProjectListItem from '../components/ProjectListItem';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import axios from '../api/axios';

const Project = () => {
  const [projects, setProjects] = useState<Array<ProjectDto>>();
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    let isMounted = true;
    const controller = new AbortController();

    const getAllProjects = async () => {
      try
      {
        const response = await axios.get('/project', {
          signal: controller.signal
        });
        isMounted && setProjects(response.data);
      }
      catch(e: any)
      {
        console.error(e);
        navigate('/login', { state: { from: location }, replace: true });
      }
    }

    getAllProjects();
    console.log(projects);
    return () => {
      isMounted = false;
      controller.abort();
    }
  }, []);

  return(
    <>
      <Container maxWidth="sm">
        {projects?.length
          ? (
            <List>
              {projects.map((e) => (
              <ListItem key={e.id}>
                <ProjectListItem item={e}/>
              </ListItem>
              ))}
          </List>
              ) : <p>¡No hay proyectos registrados!</p>}
      </Container>
    </>
  );
}

export default Project;