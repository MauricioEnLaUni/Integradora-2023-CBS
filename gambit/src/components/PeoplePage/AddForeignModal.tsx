import * as React from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Modal from '@mui/material/Modal';
import AddCircleOutlinedIcon from '@mui/icons-material/AddCircleOutlined';
import TextField from '@mui/material/TextField';
import { Grid } from '@mui/material';
import { useState } from 'react';
import axios from '../../api/axios';

const style = {
  position: 'absolute' as 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: '50%',
  bgcolor: 'background.paper',
  border: '2px solid #000',
  boxShadow: 24,
  p: 4,
};

const AddForeignModal = ({ token, refresh, setRefresh }: { token: string, refresh: boolean, setRefresh: any }) => {
  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const [name, setName] = useState('');
  const [lastName, setLastName] = useState('');
  const [area, setArea] = useState('');
  const [position, setPosition] = useState('');
  const [role, setRole] = useState('');
  const [project, setProject] = useState('');
  const [email, setEmail] = useState('');
  const [phone, setPhone] = useState('');
  
  const handleSubmit = async () => {
    const dto: NewExtPersonDto = new NewExtPersonDto(name, lastName, area, position, role, project, email, phone);
    try {
      const url = '/foreign';
      const data = (await axios.post(url, 
        JSON.stringify(dto),
        {
          headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json',
            'withCredentials': true
          }
        })).data;
        setOpen(false);
        setRefresh(!refresh);
    } catch (err: any)
    {
      console.error(err.response);
    }
  }

  return (
    <Box sx={{ justifyContent: 'center' }}>
      <Button onClick={handleOpen}>
        <AddCircleOutlinedIcon />NUEVO
      </Button>
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
          <Grid container columns={6} direction={'column'} spacing={2}>
            <Grid item>
              <TextField id="company-name" label="Nombre" variant="filled" fullWidth={true} onChange={(e) => setName(e.target.value)}/>
            </Grid>
            <Grid item>
              <TextField id="company-last-name" label="Apellidos" variant="filled" fullWidth={true} onChange={(e) => setLastName(e.target.value)}/>
            </Grid>
            <Grid item>
              <TextField id="company-area" label="Area" variant="filled" fullWidth={true} onChange={(e) => setArea(e.target.value)}/>
            </Grid>
            <Grid item>
              <TextField id="company-position" label="Posicion" variant="filled" fullWidth={true} onChange={(e) => setPosition(e.target.value)}/>
            </Grid>
            <Grid item>
              <TextField id="company-role" label="Rol" variant="filled" fullWidth={true} onChange={(e) => setRole(e.target.value)}/>
            </Grid>
            <Grid item>
              <TextField id="company-project" label="Project" variant="filled" fullWidth={true} onChange={(e) => setProject(e.target.value)}/>
            </Grid>
            <Grid item>
              <TextField id="company-relation" type={'email'} label="Email" variant="filled" fullWidth={true} onChange={(e) => setEmail(e.target.value)} />
            </Grid>
            <Grid item>
              <TextField id="company-relation" type={'phone'} label="Teléfono" variant="filled" fullWidth={true} onChange={(e) => setPhone(e.target.value)} />
            </Grid>
            <Grid container columns={2} direction={'row'} pt={3}>
              <Grid xs={1} item={true}>
                <Button variant="contained" sx={{ mx: 'auto', width: 200 }} onClick={handleSubmit}>Submit</Button>
              </Grid>
              <Grid xs={1} item={true}>
                <Button variant="contained" sx={{ mx: 'auto', width: 200 }} onClick={handleClose}>Cancel</Button>
              </Grid>
            </Grid>
          </Grid>
        </Box>
      </Modal>
    </Box>
  );
}

export default AddForeignModal;

class NewExtPersonDto {
  name: string;
  lastName: string;
  area: string;
  position: string;
  role: string;
  project: string;
  email: string | undefined;
  phone: string | undefined;

  constructor(name:string, lastName: string, area: string, position: string, role: string, project: string, email?: string, phone?: string)
  {
    this.name = name;
    this.lastName = lastName;
    this.area = area;
    this.position = position;
    this.role = role;
    this.project = project;
    this.email = email;
    this.phone = phone;
  }
}