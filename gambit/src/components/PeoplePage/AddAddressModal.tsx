import * as React from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Modal from '@mui/material/Modal';
import AddCircleOutlinedIcon from '@mui/icons-material/AddCircleOutlined';
import TextField from '@mui/material/TextField';
import { Grid, Icon, IconButton, Tooltip } from '@mui/material';
import { useState } from 'react';
import axios from '../../api/axios';
import AddCircleIcon from '@mui/icons-material/AddCircle';
import AddressDto from '../../models/Response/Contact/AddressDto';

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

const AddForeignModal = ({ token, refresh, setRefresh, owner }: { token: string, refresh: boolean, setRefresh: any, owner: string }) => {
  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const [street, setStreet] = useState('');
  const [number, setNumber] = useState('');
  const [colony, setColony] = useState('');
  const [postalCode, setPostalCode] = useState('');
  const [city, setCity] = useState('');
  const [state, setState] = useState('');
  const [country, setCountry] = useState('');
  
  const handleSubmit = async () => {
    const dto: AddAddressDto = new AddAddressDto(owner, street, number, colony, postalCode, city, state, country);
    try {
      const url = '/companies/address/add';
      (await axios.patch(url, 
        JSON.stringify(dto),
        {
          headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json',
          },
          withCredentials: true
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
      <Tooltip title={`Añadir dirección`} arrow>
        <IconButton onClick={handleOpen}>
          <Icon
            className={'inner-icon'}
            component={AddCircleIcon}
            sx={{ color: '#e4f1f1', fontSize: '56px' }}
          />
        </IconButton>
      </Tooltip>
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
          <Grid container columns={6} direction={'column'} spacing={2}>
            <Grid item>
              <TextField id="company-street" label="Calle" variant="filled" fullWidth={true} onChange={(e) => setStreet(e.target.value)}/>
            </Grid>
            <Grid item>
              <TextField id="company-numbers" label="Número" variant="filled" fullWidth={true} onChange={(e) => setNumber(e.target.value)}/>
            </Grid>
            <Grid item>
              <TextField id="company-colony" label="Colonia" variant="filled" fullWidth={true} onChange={(e) => setColony(e.target.value)}/>
            </Grid>
            <Grid item>
              <TextField id="company-postal-code" label="Código Postal" variant="filled" fullWidth={true} onChange={(e) => setPostalCode(e.target.value)}/>
            </Grid>
            <Grid item>
              <TextField id="company-city" label="Ciudad" variant="filled" fullWidth={true} onChange={(e) => setCity(e.target.value)}/>
            </Grid>
            <Grid item>
              <TextField id="company-state" label="Estado" variant="filled" fullWidth={true} onChange={(e) => setState(e.target.value)}/>
            </Grid>
            <Grid item>
              <TextField id="company-country" label="País" variant="filled" fullWidth={true} onChange={(e) => setCountry(e.target.value)} />
            </Grid>
            <Grid container columns={2} direction={'row'} pt={3}>
              <Grid xs={1} item={true}>
                <Button variant="contained" sx={{ mx: 'auto', width: 200 }} onClick={() => handleSubmit()}>Submit</Button>
              </Grid>
              <Grid xs={1} item={true}>
                <Button variant="contained" sx={{ mx: 'auto', width: 200 }} onClick={() => handleClose()}>Cancel</Button>
              </Grid>
            </Grid>
          </Grid>
        </Box>
      </Modal>
    </Box>
  );
}

export default AddForeignModal;

class AddAddressDto {
  id: string;
  street?: string | null;
  number?: string | null;
  colony?: string | null;
  postalCode?: string | null;
  city?: string | null;
  state?: string | null;
  country?: string | null;

  constructor(id: string, street?: string, number?: string, colony?: string, postalCode?: string, city?: string, state?: string, country?: string)
  {
    this.id = id;
    this.street = street != '' ? street : null;
    this.number = number != '' ? number : null;
    this.colony = colony != '' ? colony : null;
    this.postalCode = postalCode != '' ? postalCode : null;
    this.city = city != '' ? city : null;
    this.state = state != '' ? state : null;
    this.country = country != '' ? country : null;
  }
}