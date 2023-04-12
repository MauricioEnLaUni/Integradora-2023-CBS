// React
import { useEffect, useMemo, useState } from "react";

// MUI
import Grid from "@mui/material/Grid";
import TextField from "@mui/material/TextField";
import Typography from "@mui/material/Typography";
import IconButton from "@mui/material/IconButton";
import SaveIcon from '@mui/icons-material/Save';
import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import pink from "@mui/material/colors/pink";

const StringTab = ({ data, name }: { data: Array<string>, name: string }) => {
  const [numbers, setNumbers] = useState<Array<string>>([]);
  console.log();

  const handleUpdate = (value: string, index: number) => {
    setNumbers((numbers) => {
      const newNumber = [...numbers];
      newNumber[index] = value;
      return newNumber;
    });
  };

  useEffect(() => {
    setNumbers(data);
  }, []);

  const memoizedValue: any = useMemo(() => numbers, [numbers]);

  return(
    <Grid container direction={'row'} columns={8}>
      {numbers?.map((element, i) => (
        <Grid direction={'row'} container columns={10} key={(i + 1) * 10}>
          <Grid xs={2} display='flex' alignItems='center'>
            <Typography align='center' variant='subtitle1'>
              {`${name} ${i + 1}:`}
            </Typography>
          </Grid>
          <Grid xs={6}>
            <TextField
              id={`${name}-${i}`}
              label={element}
              onChange={(e) => handleUpdate(e.target.value, i)}
              fullWidth={true}
            />
          </Grid>
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
      ))}
    </Grid>
  );
}

export default StringTab;