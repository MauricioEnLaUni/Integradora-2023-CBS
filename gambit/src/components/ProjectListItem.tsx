import Accordion from '@mui/material/Accordion';
import AccordionSummary from '@mui/material/AccordionSummary';
import AccordionDetails from '@mui/material/AccordionDetails';
import Typography from '@mui/material/Typography';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import Stack from '@mui/material/Stack';
import Paper from '@mui/material/Paper';
import { styled } from '@mui/material/styles';

const Item = styled(Paper)(({ theme } : { theme: any }) => ({
  backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
  ...theme.typography.body2,
  padding: theme.spacing(1),
  textAlign: 'center',
  color: theme.palette.text.secondary,
}));

const ProjectListItem = ({item} : {item: ProjectDto}) => {
  return (
    <div key={item.Id}>
      <Accordion>
        <AccordionSummary
          expandIcon={<ExpandMoreIcon />}
          aria-controls="panel1a-content"
          id="panel1a-header"
        >
          <Typography>{item.Name}</Typography>
        </AccordionSummary>
        <AccordionDetails>
          <Stack direction="row" spacing={2}>
            <Item>{item.Starts.toDateString()}</Item>
            <Item>{item.Ends.toDateString()}</Item>
            <Item>
              <Stack direction="row" spacing={2}>
                <Item>{item.LastTask.Name}</Item>
                <Item>{item.LastTask.StartDate.toDateString()}</Item>
                <Item>{item.LastTask.Ends.toDateString()}</Item>
              </Stack>
            </Item>
          </Stack>
        </AccordionDetails>
      </Accordion>
    </div>
  );
}

export default ProjectListItem;