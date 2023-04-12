import { styled } from "@mui/material/styles";
import { GridColDef } from "@mui/x-data-grid";
import { DataGrid } from "@mui/x-data-grid/DataGrid";
import PeopleCondensed from "../../models/Display/PeopleCondensed";
import Box from "@mui/material/Box";

const PeopleContainer = styled(DataGrid)(({ theme }) => ({

}));

const Subordinates = ({ rows, columns } : { rows: Array<PeopleCondensed>, columns: Array<GridColDef> }) => (
  <Box sx={{height: 350, width: '100%' }}>
    <PeopleContainer
      getRowId={(row) => row.id}
      rows={rows}
      columns={columns}
      initialState={{
        pagination: {
          paginationModel: {
            pageSize: 5,
          },
        },
        columns: {
          columnVisibilityModel: {
            id: false
          },
        },
      }}
      pageSizeOptions={[5]}
    />
  </Box>
);

export default Subordinates;