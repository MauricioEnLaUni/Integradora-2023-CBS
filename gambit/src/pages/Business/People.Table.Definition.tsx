// MUI
import { GridColDef } from '@mui/x-data-grid';

// Fictichos
import PeopleCondensed from '../../models/Display/PeopleCondensed';

const getId = (params: { row: PeopleCondensed }) => (
  <a href={`localhost:5173/people/${params.row.id || ''}`}>
    {params.row.name}
  </a>
);

const gridDef: Array<GridColDef> = [
  { field: 'id', headerName: 'id', width: 0 },
  { field: 'name', headerName: 'Name', width: 175, renderCell: getId, hideable: false },
  { field: 'position', headerName: 'Position', width: 170, hideable: false }
];

export default gridDef;