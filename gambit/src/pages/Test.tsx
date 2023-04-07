import { styled } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Paper from '@mui/material/Paper';
import Grid from '@mui/material/Unstable_Grid2';
import PersonDto from '../models/Response/People/PersonDto';
import ContactDto from '../models/Response/Contact/ContactDto';
import { Card, Typography } from '@mui/material';
import { DataGrid, GridColDef, GridRenderCellParams, GridValueGetterParams } from '@mui/x-data-grid';
import Subordinates from '../components/PeoplePage/PeopleList';
import PeopleCondensed from '../models/Display/PeopleCondensed';
import { Link } from 'react-router-dom';
import AddressTab from '../components/Contacts/ContactContainer';
import PeopleTitle from '../components/PeoplePage/TitleContainer';
import Profile from '../components/PeoplePage/AvatarBox';

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

const DUMMY: PersonDto = {
  Id: "123456",
  Name: "COMPANY NAME",
  Contact: new ContactDto([], [], []),
  Relation: "PROVIDER"
};

const subordinateRow: Array<PeopleCondensed> = [
  { id: 0, name: 'John Smith', position: 'Cerrajero' },
  { id: 1, name: 'John Smith', position: 'Cerrajero' },
  { id: 2, name: 'John Smith', position: 'Cerrajero' },
  { id: 3, name: 'John Smith', position: 'Cerrajero' },
  { id: 4, name: 'John Smith', position: 'Cerrajero' },
  { id: 0, name: 'John Smith', position: 'Cerrajero' },
  { id: 1, name: 'John Smith', position: 'Cerrajero' },
  { id: 2, name: 'John Smith', position: 'Cerrajero' },
  { id: 3, name: 'John Smith', position: 'Cerrajero' },
  { id: 4, name: 'John Smith', position: 'Cerrajero' }
];

const getId = (params: { row: PeopleCondensed }) =>
  <a href={`localhost:5173/people/${params.row.id || ''}`}>
    {params.row.name}
  </a>

const gridDef: Array<GridColDef> = [
  { field: 'id', headerName: 'id', width: 0 },
  { field: 'name', headerName: 'Name', width: 175, renderCell: getId, hideable: false },
  { field: 'position', headerName: 'Position', width: 170, hideable: false }
];

const IMAGE: string = "https://picsum.photos/296/296";

const AddressExample = [{
  Street: "abs",
  Number: "abs",
  Colony: "abs",
  PostalCode: "abs",
  City: "abs",
  State: "abs",
  Country: "abs",
  Coordinates: {Latitude: "12346", Longitude: "123456"}
},
{
  Street: "abs",
  Number: "abs",
  Colony: "abs",
  PostalCode: "abs",
  City: "abs",
  State: "abs",
  Country: "abs",
  Coordinates: {Latitude: "12346", Longitude: "123456"}
},
{
  Street: "abs",
  Number: "abs",
  Colony: "abs",
  PostalCode: "abs",
  City: "abs",
  State: "abs",
  Country: "abs",
  Coordinates: {Latitude: "12346", Longitude: "123456"}
}]

const Test = () => (
  <Box sx={{ flexGrow: 12 }}>
    <Grid container spacing={2} columns={32}>
      <Grid xs={9}>
        <Grid direction={'column'}>
          <ProfileBlock>
            <Profile />
          </ProfileBlock>
          <TitleContainer>
            <PeopleTitle title={DUMMY.Name} relation={DUMMY.Relation}/>
          </TitleContainer>
          <Subordinates rows={subordinateRow} columns={gridDef}/>
        </Grid>
      </Grid>
      <Grid xs={23}>
        <AddressTab addresses={AddressExample}></AddressTab>
      </Grid>
    </Grid>
  </Box>
);

export default Test;