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
import AddForeignModal from '../../components/PeoplePage/AddForeignModal';

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

const TitleContainer: any = styled(Paper)(({ theme }) => ({
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
  const [loading, setLoading] = useState(true);
  const [refresh, setRefresh] = useState(false);

  const [subordinateRow, setSubordinateRow] = useState<Array<PeopleCondensed>>([]);
  const [projects, setProjects] = useState<Array<ProjectCondensed>>([]);
  const [accounts, setAccounts] = useState<Array<AccountInOut>>([]);

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
        })).data;
        setResource(data);
        setLoading(false);
      } catch (err)
      {
        console.error("Shit");
      }
    }
    GetData();
  }, []);

  useEffect(() => {
    const subt = async () => {
      if (resource?.id != undefined)
      {
        const output = (await axios.get(`/foreign/company/${resource.id}`,{
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${auth.token}`
          },
          withCredentials: true
        })).data;
        console.log(output);
        setSubordinateRow(output);
      }
    }
    if (resource?.id != undefined)
    {
      subt();
    }
  }, [resource]);

  useEffect(() => {
    const proj = async () => {
      const output = (await axios.get(`/project/company/${resource.id}`,{
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${auth.token}`
        },
        withCredentials: true
      })).data;
      setProjects(output);
    }
    proj();
    setRefresh(!refresh);
  }, [subordinateRow]);

  useEffect(() => {
    const acc = async () => {
      const list = projects.map(e => e.id);
      const output = (await axios.post(`/project/company/accounts`,
      JSON.stringify(list),
      {
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${auth.token}`
        },
        withCredentials: true
      })).data;
      setAccounts(output);
    }
    acc();
  }, [projects]);

  useEffect(() => {
  }, [refresh]);

  return(
    <Box sx={{ flexGrow: 12 }}>
      <Grid container columns={32}>
        <Grid xs={8}>
          <Grid direction={'column'}>
            <ProfileBlock>
              <Profile />
            </ProfileBlock>
            <TitleContainer>
              { loading ? <p>Loading...</p> : <PeopleTitle title={resource.name} relation={resource.relation}/> }
            </TitleContainer>
            <Box sx={{ justifyContent: 'center' }}>
              { loading ? <p>Loading...</p> : <AddForeignModal token={auth?.token} refresh={refresh} setRefresh={setRefresh} owner={resource.id}/>}
            </Box>
            { loading ? <p>Loading...</p> : <Subordinates rows={subordinateRow} columns={gridDef}/> }
          </Grid>
        </Grid>
        <Grid xs={24} container columns={7}>
          <Grid xs={7}>
            { loading ? <p>Loading...</p> : (<ContactContainer contact={resource.contact} />) }
          </Grid>
          <Grid xs={5} sx={{ overflow: 'auto' }}>
            { loading ? <p>Loading...</p> : (<ProjectSummary projects={projects}/>)}
          </Grid>
          <Grid xs={2}>
            { loading ? <p>Loading...</p> : (<AccountChart accounts={accounts}/>)}
          </Grid>
        </Grid>
      </Grid>
    </Box>
  );
}

export default CompanyPage;