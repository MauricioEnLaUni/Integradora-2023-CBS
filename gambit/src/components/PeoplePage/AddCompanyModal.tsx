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

const AddCompanyModal = ({ token, refresh, setRefresh }: { token: string, refresh: boolean, setRefresh: any }) => {
  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const [name, setName] = useState('');
  const [activity, setActivity] = useState('');
  const [relation, setRelation] = useState('');
  const [email, setEmail] = useState('');
  const [phone, setPhone] = useState('');
  
  const handleSubmit = async () => {
    const dto: NewCompanyDto = new NewCompanyDto(name, activity, relation, email, phone);
    try {
      const url = '/Companies';
      (await axios.post(url, 
        JSON.stringify(dto),
        {
          headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json',
            'withCredentials': true
          }
        }));
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
              <TextField id="company-activity" label="Giro" variant="filled" fullWidth={true} onChange={(e) => setActivity(e.target.value)} />
            </Grid>
            <Grid item>
              <TextField id="company-relation" label="Relación" variant="filled" fullWidth={true} onChange={(e) => setRelation(e.target.value)} />
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

export default AddCompanyModal;

class NewCompanyDto {
  name: string;
  activity: string;
  relation: string;
  email: string | undefined;
  phone: string | undefined;

  constructor(name:string, activity: string, relation: string, email?: string, phone?: string)
  {
    this.name = name;
    this.activity = activity;
    this.relation = relation;
    this.email = email;
    this.phone = phone;
  }
}