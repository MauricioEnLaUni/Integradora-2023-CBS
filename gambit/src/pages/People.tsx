import Container from "@mui/material/Container";
import Card from '@mui/material/Card';
import CardHeader from '@mui/material/CardHeader';
import PersonDto from "../models/Response/People/PersonDto";

const PersonGenerals = (person: PersonDto) => {
  return(
    <Container>
      <Card sx={{ maxWidth: 345 }}>
        <CardHeader
          title={person.Name}
          subheader="September 14, 2016"
        />
      </Card>
    </Container>
  );
}