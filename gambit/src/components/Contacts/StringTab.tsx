import Grid from "@mui/material/Grid";
import TextField from "@mui/material/TextField";
import Typography from "@mui/material/Typography";
import { useEffect, useMemo, useState } from "react";

const StringTab = ({ data, name }: { data: Array<string>, name: string }) => {
  const [numbers, setNumbers] = useState<Array<string>>([]);

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
    <Grid container columns={3}>
      {numbers.map((element, i) => (
        <Grid direction={'row'} container columns={3} key={(i + 1) * 10}>
          <Grid xs={1} display='flex' alignItems='center'>
            <Typography align='center' variant='subtitle1'>
              {`${name} ${i + 1}:`}
            </Typography>
          </Grid>
          <Grid xs={2}>
            <TextField
              id={`${name}-${i}`}
              label={element}
              onChange={(e) => handleUpdate(e.target.value, i)}
            />
          </Grid>
        </Grid>
      ))}
    </Grid>
  );
}

export default StringTab;