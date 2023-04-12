// MUI
import { GridColDef } from '@mui/x-data-grid';

// Fictichos
import PeopleCondensed from '../../models/Display/PeopleCondensed';
import axios from '../../api/axios';

class UpdatedAddressDto {
  id: string;
  street: string;
  number: string;
  colony: string;
  postalCode: string;
  city: string;
  state: string;
  country: string;

  constructor(
    id: string,
    street: string,
    number: string,
    colony: string,
    postalCode: string,
    city: string,
    state: string,
    country: string
  )
  {
    this.id = id;
    this.street = street;
    this.number = number;
    this.colony = colony;
    this.postalCode = postalCode;
    this.city = city;
    this.state = state;
    this.country = country;
  }
}

const getId = (params: { row: PeopleCondensed }) => (
  <a href={`localhost:5173/people/${params.row.id || ''}`}>
    {params.row.name}
  </a>
);

const gridDef: Array<GridColDef> = [
  { field: 'id', headerName: 'id', width: 0 },
  { field: 'name', headerName: 'Name', width: 175, renderCell: getId, hideable: false },
  { field: 'position', headerName: 'Position', width: 170, hideable: false },

];

export default gridDef;