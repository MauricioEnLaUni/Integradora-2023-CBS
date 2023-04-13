import SaveIcon from '@mui/icons-material/Save';
import IconButton from '@mui/material/IconButton';
import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import Select from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';

import CompanyBrowserDto from "./CompanyBrowserType";
import axios from '../../api/axios';
import { Link } from 'react-router-dom';
import { Dispatch, SetStateAction } from 'react';

const CompanyBrowserColumnsDef = (canEdit: boolean, token: string, refresh: boolean, setRefresh: Dispatch<SetStateAction<boolean>>) => {
  const handleUpdate = async (params: any, token: string) => {
    const put = new BrowserCompanyUpdateDto(params.id, params.name, params.activity, params.relation);
    console.log(put);
    try {
      const url = `/companies`;
      const data = (await axios.patch(url,
        JSON.stringify(put),
        {
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
          },
          withCredentials: true
        }));
        setRefresh(!refresh);
    } catch (e: any) {
      console.error(e.response);
    }
  }
  
  const handleDelete = async (id: string, token: string) => {
    try {
      const url = `/companies/${id}`;
      const data = (await axios.delete(url,
      {
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`
        },
        withCredentials: true
      }));
      setRefresh(!refresh);
    } catch (e: any) {
      console.error(e.response);
    }
  }
  
  const linkToDetails = (params: { row: CompanyBrowserDto }) => (
    <Link to={`/clients/${params.row.id}`}>
      {params.row.name}
    </Link>
  );
  
  
  const MultiplePhones = (params: any) => {
    const options = params.row.phones as string[];
    const defaultV = options.length > 0 ? options[0] : '';
    return(
      <Select
        defaultValue={defaultV}
      >
        {params.row.phones.map((option: string) => (
          <MenuItem key={option} value={option || ''}>
            {option}
          </MenuItem>
        ))}
      </Select>
    );
  };
  
  const MultipleEmails = (params: any) => {
    const options = params.row.emails as string[];
    const defaultV = options.length > 0 ? options[0] : '';
    return(
      <Select
        defaultValue={defaultV}
      >
        {params.row.emails.map((option: string) => (
          <MenuItem key={option} value={option || ''}>
            {option}
          </MenuItem>
        ))}
      </Select>
    );
  }

  const Update = (params: any) => (
    <IconButton color="primary" onClick={() => handleUpdate(params.row, token)}>
      <SaveIcon />
    </IconButton>
  );
  
  const Delete = (params: any) => (
    <IconButton color="error" onClick={() => handleDelete(params.row.id, token)}>
      <DeleteForeverIcon />
    </IconButton>
  );

  return [
    { field: 'id', headerName: 'ID', width: 160 },
    {
      field: 'name',
      headerName: 'Companía',
      width: 300,
      editable: canEdit,
      renderCell: linkToDetails
    },
    {
      field: 'activity',
      headerName: 'Giro',
      width: 200,
      editable: canEdit,
    },
    {
      field: 'relation',
      headerName: 'Relación',
      width: 200,
      editable: canEdit,
    },
    {
      field: 'phones',
      headerName: 'Teléfono',
      width: 150,
      type: 'singleSelect',
      renderCell: MultiplePhones
    },
    {
      field: 'emails',
      headerName: 'Email',
      width: 150,
      type: 'singleSelect',
      renderCell: MultipleEmails
    },
    {
      field: 'save',
      headerName: 'Guardar',
      width: 90,
      renderCell: Update
    },
    {
      field: 'delete',
      headerName: 'Delete',
      width: 90,
      editable: canEdit,
      renderCell: Delete
    }
  ];
}

export default CompanyBrowserColumnsDef;

class BrowserCompanyUpdateDto {
  id: string;
  name: string;
  activity: string;
  relation: string;

  constructor(id: string, name: string, activity: string, relation: string)
  {
    this.id = id;
    this.name = name;
    this.activity = activity;
    this.relation = relation;
  }
}