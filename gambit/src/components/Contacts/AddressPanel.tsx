// React
import { useEffect, useMemo, useState } from "react";

//MUI
import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import TextField from "@mui/material/TextField";
import IconButton from "@mui/material/IconButton";
import SaveIcon from '@mui/icons-material/Save';
import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import pink from '@mui/material/colors/pink';

//Fictichos
import AddressDto from "../../models/Response/Contact/AddressDto";

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

  const memoizedValue: any = useMemo(() => display, [display]);

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

export default AddressPanel;