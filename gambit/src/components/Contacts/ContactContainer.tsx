import * as React from 'react';
import { useState, SyntheticEvent, useEffect } from 'react';
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import AddressDto from '../../models/Response/Contact/AddressDto';
import Grid from '@mui/material/Unstable_Grid2';
import { IconButton, TextField } from '@mui/material';
import SaveIcon from '@mui/icons-material/Save';
import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import pink from '@mui/material/colors/pink';

interface TabPanelProps {
  children?: React.ReactNode;
  index: number;
  value: number;
}

const FIELDS = ["Street", "Number", "Colony", "Postal Code", "City", "State", "Country", "Latitude", "Longitude"];

const AddressPanel = ({address, tab}: {address: AddressDto, tab: number}) => {
  const [street, setStreet] = useState<string>(address.Street || '');
  const [number, setNumber] = useState<string>(address.Number || '');
  const [colony, setColony] = useState<string>(address.Colony || '');
  const [postalCode, setPostalCode] = useState<string>(address.PostalCode || '');
  const [city, setCity] = useState<string>(address.City || '');
  const [state, setState] = useState<string>(address.State || '');
  const [country, setCountry] = useState<string>(address.Country || '');
  const [latitude, setLatitude] = useState<string>(address.Coordinates ? address.Coordinates.Latitude : '');
  const [longitude, setLongitude] = useState<string>(address.Coordinates ? address.Coordinates.Longitude : '');

  const AddressValues = [
    setStreet,
    setNumber,
    setColony,
    setPostalCode,
    setCity,
    setState,
    setCountry,
    setLatitude,
    setLongitude
  ];

  const [display, setDisplay] = useState([
    street,
    number,
    colony,
    postalCode,
    city,
    state,
    country,
    latitude,
    longitude
  ]);

  const handleUpdate = (value: string, index: number) => {
    AddressValues[index](value);
  }

  useEffect(() => {
    setDisplay([
      street,
      number,
      colony,
      postalCode,
      city,
      state,
      country,
      latitude,
      longitude
    ]);
  }, [
    street,
    number,
    colony,
    postalCode,
    city,
    state,
    country,
    latitude,
    longitude
  ]);

  return(
    <Grid container columns={3}>
      {display.map((element, i) => (
        <Grid xs={1} direction={'row'} container columns={3} key={i}>
          <Grid xs={1} display='flex' alignItems='center'>
            <Typography align='left' variant='subtitle1'>
              {FIELDS[i]}
            </Typography>
          </Grid>
          <Grid xs={2}>
            <TextField
              id={`${FIELDS[i]}-${tab.toString()}`}
              label={element}
              onChange={(e) => handleUpdate(e.target.value, i)}
            />
          </Grid>
        </Grid>
      ))}
      <Grid xs={1}>
        <IconButton>
          <SaveIcon sx={{fontSize: 48}} color="primary" />
        </IconButton>
      </Grid>
      <Grid xs={1}>
        <IconButton>
          <DeleteForeverIcon sx={{fontSize: 48, color: pink[500]}} />
        </IconButton>
      </Grid>
    </Grid>
  );
}

const a11yProps = (index: number) => {
  return {
    label: `Address ${index + 1}`,
    id: `simple-tab-${index}`,
    'aria-controls': `simple-tabpanel-${index}`,
    key: `${index}`
  };
}

const AddressTab = ({addresses}: {addresses: Array<AddressDto>}) => {
  const [value, setValue] = useState(0);

  const handleChange = (event: SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };

  return (
    <Box sx={{ width: '100%' }}>
      <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
        <Tabs value={value} onChange={handleChange} aria-label="basic tabs example">
          {addresses.map((e, i) => (
            <Tab {...a11yProps(i)} />
          ))}
        </Tabs>
      </Box>
      {addresses.map((e, i) => (
        <div
          role="tabpanel"
          hidden={value !== i}
          id={`simple-tabpanel-${i}`}
          aria-labelledby={`simple-tab-${i}`}
        >
          <AddressPanel address={e} tab={i}/>
        </div>
      ))}
    </Box>
  );
}

export default AddressTab;