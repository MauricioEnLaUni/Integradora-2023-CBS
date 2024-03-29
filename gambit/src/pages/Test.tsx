import { styled } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Paper from '@mui/material/Paper';
import Grid from '@mui/material/Unstable_Grid2';
import PersonDto from '../models/Response/People/PersonDto';
import ContactDto from '../models/Response/Contact/ContactDto';
import { Card, Typography } from '@mui/material';
import { GridColDef } from '@mui/x-data-grid';
import Subordinates from '../components/PeoplePage/PeopleList';
import PeopleCondensed from '../models/Display/PeopleCondensed';
import PeopleTitle from '../components/PeoplePage/TitleContainer';
import Profile from '../components/PeoplePage/AvatarBox';
import ContactContainer from '../components/Contacts/ContactContainer';
import ProjectSummary from '../components/Projects/ProjectDataGrid';
import TaskCondensed from '../models/Display/TaskCondensed';
import ProjectCondensed from '../models/Display/ProjectCondensed';
import AccountInOut from '../models/Display/AccountInOut';
import AccountChart from '../components/PeoplePage/AccountChart';

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
}];

const EMAILDUMMY = [ "mauricio@cbs.com" ];
const PHONEDUMMY = ["4494494499", "4494494499", "4494494499", "4494494499", "4494494499",];

const CONTACTDUMMY: ContactDto = new ContactDto(AddressExample, EMAILDUMMY, PHONEDUMMY);

const PROJECTSDUMMY = [
  new ProjectCondensed("0", 'Venta', new Date("2023-11-18"), new TaskCondensed("123", "Clean", new Date("2023-04-09"), "Active", "Johnny"), "Bobby"),
  new ProjectCondensed("1", 'Venta', new Date("2023-11-18"), new TaskCondensed("123", "Clean", new Date("2023-04-09"), "Active", "Johnny"), "Bobby"),
  new ProjectCondensed("2", 'Venta', new Date("2023-11-18"), new TaskCondensed("123", "Clean", new Date("2023-04-09"), "Active", "Johnny"), "Bobby"),
  new ProjectCondensed("3", 'Venta', new Date("2023-11-18"), new TaskCondensed("123", "Clean", new Date("2023-04-09"), "Active", "Johnny"), "Bobby"),
  new ProjectCondensed("4", 'Venta', new Date("2023-11-18"), new TaskCondensed("123", "Clean", new Date("2023-04-09"), "Active", "Johnny"), "Bobby")
];

const ACCOUNTDUMMY: Array<AccountInOut> = [
  new AccountInOut("1", "Testor", 1340.14, 759.15),
  new AccountInOut("2", "Hello", 1988.10, 6075.10)
];

const Test = () => {

  return(
    <Box sx={{ flexGrow: 12 }}>
      <Grid container columns={32}>
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
        <Grid xs={23} container columns={7}>
          <Grid xs={7}>
            <ContactContainer contact={CONTACTDUMMY} />
          </Grid>
          <Grid xs={5} sx={{ overflow: 'auto' }}>
            <ProjectSummary projects={PROJECTSDUMMY}/>
          </Grid>
          <Grid xs={2}>
            <AccountChart accounts={ACCOUNTDUMMY}/>
          </Grid>
        </Grid>
      </Grid>
    </Box>
  );
}

export default Test;