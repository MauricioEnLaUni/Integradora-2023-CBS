// React
import { SetStateAction, useEffect, useState } from 'react';
// MUI
import { styled } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Paper from '@mui/material/Paper';
import Grid from '@mui/material/Unstable_Grid2';
import { Card, Typography } from '@mui/material';

// Fictichos
import Profile from '../../components/PeoplePage/AvatarBox';
import PeopleTitle from '../../components/PeoplePage/TitleContainer';
import Subordinates from '../../components/PeoplePage/PeopleList';
import gridDef from './People.Table.Definition';
import ProjectSummary from '../../components/Projects/ProjectDataGrid';
import ContactContainer from '../../components/Contacts/ContactContainer';
import AccountChart from '../../components/PeoplePage/AccountChart';
import CompanyDto from '../../models/Response/People/Company.Dto';

import axios from '../../api/axios';
import PeopleCondensed from '../../models/Display/PeopleCondensed';
import AccountInOut from '../../models/Display/AccountInOut';
import ProjectCondensed from '../../models/Display/ProjectCondensed';
import useAuth from '../../hooks/useAuth';

const Item = styled(Paper)(({ theme }) => ({
  backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
  ...theme.typography.body2,
  padding: theme.spacing(1),
  textAlign: 'center',
  color: theme.palette.text.secondary,
}));

const Title = styled(Typography)(({ theme }) => ({
  backgroundColor: theme.palette.mode === 'dark' ? '#fff' : '#6D8A96',
  fontFamily: 'Helvetica',
  fontWeight: 700,
  fontSize: '1.75rem',
}));

const TitleContainer = styled(Paper)(({ theme }) => ({
  backgroundColor: '#6D8A96',
  padding: theme.spacing(1),
  textAlign: 'center',
  color: theme.palette.text.secondary,
}));

const ProfileBlock = styled(Card)(({ theme }) => ({
  backgroundColor: '#1A2027',
  padding: theme.spacing(2),
  textAlign: 'center',
  color: theme.palette.text.secondary
}));

const CompanyPage = () => {
  const { auth } = useAuth();
  const [resource, setResource] = useState({} as CompanyDto);


  const SUBORDINATES = `/foreign/${resource.Id}`;
  const PROJECTS = `/project/${resource.Id}`;
  const ACCOUNTS = `/account/projects`;

  const [subordinateRow, setSubordinateRow] = useState<Array<PeopleCondensed>>([]);
  const [projects, setProjects] = useState<Array<ProjectCondensed>>([]);
  const [accounts, setAccounts] = useState<Array<AccountInOut>>([]);

  const [err, setErrMsg] = useState('');

  const GetInfo = async (url: string, set: SetStateAction<any>) => {
    try {
      const data = (await axios.get(url,
        {
          headers: { 'Content-Type': 'application/json' },
          withCredentials: true
        }
      )).data;
      set(data);
      console.log(data);
    } catch (err: any)
    {
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

  useEffect(() => {
    const st = window.location.href;
    const id = st.substring(st.lastIndexOf('/') + 1);
    const GetData = async () => {
      try {
        const data = (await axios.get(`/companies/${id}`, {
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${auth.token}`
          },
          withCredentials: true
        })).data.company;
        console.log(data);
        setResource(data);
      } catch (err)
      {
        console.error("Shit");
      }
    }
    GetData();
  }, []);

  useEffect(() => {
    GetInfo(SUBORDINATES, setSubordinateRow);
    GetInfo(PROJECTS, setProjects);
  }, []);

  useEffect(() => {
    if (projects.length != 0)
    {
      const list = projects.map(e => e.id);
      const postAccounts = async () => {
        try {
          const data = (await axios.post(ACCOUNTS,
            JSON.stringify(list),
            {
              headers: { 'Content-Type': 'application/json' },
              withCredentials: true
            })).data;
          console.log(data);
          setAccounts(data);
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
    }
  }, [projects]);

  return(
    <Box sx={{ flexGrow: 12 }}>
      <Grid container columns={32}>
        <Grid xs={9}>
          <Grid direction={'column'}>
            <ProfileBlock>
              <Profile />
            </ProfileBlock>
            <TitleContainer>
              { resource ? <PeopleTitle title={resource.Name} relation={resource.Relation}/> : <p>Loading...</p> }
            </TitleContainer>
            { subordinateRow ? <Subordinates rows={subordinateRow} columns={gridDef}/> : <p>Loading...</p> }
          </Grid>
        </Grid>
        <Grid xs={23} container columns={7}>
          <Grid xs={7}>
            <ContactContainer contact={resource.Contact} />
          </Grid>
          <Grid xs={5} sx={{ overflow: 'auto' }}>
            <ProjectSummary projects={projects}/>
          </Grid>
          <Grid xs={2}>
            <AccountChart accounts={accounts}/>
          </Grid>
        </Grid>
      </Grid>
    </Box>
  );
}

export default CompanyPage;