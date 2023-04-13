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
import AddCompanyModal from '../../components/PeoplePage/AddCompanyModal';

const CompanyBrowser = () => {
  const { auth }: { auth: Authorization } = useAuth();
  const allowedRoles: Set<string> = new Set(['manager', 'admin']);
  const edit: boolean = Array.from(auth?.claims).some((claim : Claim) => allowedRoles.has(claim.value));

  const [refresh, setRefresh] = useState(false);

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
        setRows(t);
      } catch(e)
      {
        console.error("Shit");
      }
    }
    GetData();
  }, [refresh]);

  return (
    <Box sx={{ height: 550, width: '100%' }}>
      <Box sx={{ justifyContent: 'center' }}>
        <AddCompanyModal token={auth?.token} refresh={refresh} setRefresh={setRefresh}/>
      </Box>
      <DataGrid
        rows={rows}
        columns={CompanyBrowserColumnsDef(edit, auth.token, refresh, setRefresh)}
        initialState={{
          pagination: {
            paginationModel: {
              pageSize: 5,
            },
          },
        }}
        pageSizeOptions={[5]}
        disableRowSelectionOnClick
      />
    </Box>
  );
}

export default CompanyBrowser;