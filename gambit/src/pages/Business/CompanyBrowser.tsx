// React
import { useEffect, useState } from 'react';

// MUI
import Box from '@mui/material/Box';
import { DataGrid } from '@mui/x-data-grid';

// Fictichos
import CompanyBrowserColumnsDef from './CompanyBrowserColumnsDef';
import useAuth from '../../hooks/useAuth';
import Claim from '../../api/Tokens/Claims';
import { Authorization } from '../../context/AuthProvider';
import axios from '../../api/axios';
import CompanyBrowserDto from './CompanyBrowserType';

const CompanyBrowser = () => {
  const { auth }: { auth: Authorization } = useAuth();
  const allowedRoles: Set<string> = new Set(['manager', 'admin']);
  const edit: boolean = Array.from(auth?.claims).some((claim : Claim) => allowedRoles.has(claim.value));

  const [rows, setRows] = useState<Array<CompanyBrowserDto>>([]);

  useEffect(() => {
    const GetData = async () => {
      try {
        const t = (await axios.get('/companies', 
        {
          headers: { 
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${auth.token}`
          },
          withCredentials: true
        })).data;
        console.log(t);
        setRows(t);
      } catch(e)
      {
        console.error("Shit");
      }
    }
    GetData();
  }, []);


  return (
    <Box sx={{ height: 400, width: '100%' }}>
      <DataGrid
        rows={rows}
        columns={CompanyBrowserColumnsDef(edit)}
        initialState={{
          pagination: {
            paginationModel: {
              pageSize: 5,
            },
          },
        }}
        pageSizeOptions={[5]}
        checkboxSelection
        disableRowSelectionOnClick
      />
    </Box>
  );
}

export default CompanyBrowser;