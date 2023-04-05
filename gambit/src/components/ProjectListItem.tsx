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
    item != null
    ?
    (<div key={item.id}>
      <Accordion>
        <AccordionSummary
          expandIcon={<ExpandMoreIcon />}
          aria-controls="panel1a-content"
          id="panel1a-header"
        >
          <Typography>{item.name}</Typography>
        </AccordionSummary>
        <AccordionDetails>
          <Stack direction="row" spacing={2}>
            <Item>{item.starts}</Item>
            <Item>{item.ends}</Item>
            <Item>
              <Stack direction="row" spacing={2}>
                <Item>{item.lastTask?.Name}</Item>
                <Item>{item.lastTask?.StartDate}</Item>
                <Item>{item.lastTask?.Ends}</Item>
              </Stack>
            </Item>
          </Stack>
        </AccordionDetails>
      </Accordion>
    </div>) : <></>
  );
}

export default ProjectListItem;