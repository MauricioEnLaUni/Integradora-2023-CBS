import Paper from '@mui/material/Paper';
import Typography from '@mui/material/Typography';
import { styled } from '@mui/material/styles';

const TitleContainer = styled(Paper)(({ theme }) => ({
  backgroundColor: '#6D8A96',
  padding: theme.spacing(1),
  textAlign: 'center',
  color: theme.palette.text.secondary,
}));

const Title = styled(Typography)(({ theme }) => ({
  backgroundColor: theme.palette.mode === 'dark' ? '#fff' : '#6D8A96',
  fontFamily: 'Helvetica',
  fontWeight: 700,
  fontSize: '1.75rem',
}));

const PeopleTitle = ({title, relation}: {title: string, relation: string}) => (
  <TitleContainer>
    <Title>
      {title}
    </Title>
    <Title>
      {relation}
    </Title>
  </TitleContainer>
);

export default PeopleTitle;