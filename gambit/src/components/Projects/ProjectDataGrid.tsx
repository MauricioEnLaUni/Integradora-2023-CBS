import { styled } from "@mui/material/styles";
import ProjectCondensed from "../../models/Display/ProjectCondensed";
import Grid from '@mui/material/Unstable_Grid2';
import IconButton from "@mui/material/IconButton";
import SaveIcon from '@mui/icons-material/Save';
import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import pink from "@mui/material/colors/pink";
import { Link } from "react-router-dom";
import Typography from "@mui/material/Typography";
import Collapse from "@mui/material/Collapse";
import InsertLinkIcon from '@mui/icons-material/InsertLink';
import { InsertLink } from "@mui/icons-material";

const ProjectContainer = styled(Grid)(({ theme }) => ({

}));
// 10

const ProjectSummary = ({ projects }: { projects: Array<ProjectCondensed>}) => (
  <>
    <Grid container spacing={2} columns={13}>
      <Grid xs={1} display='flex' alignItems='center'>
        <Typography>Id</Typography>
      </Grid>
      <Grid xs={2} display='flex' alignItems='center'>
      <Typography>Nombre</Typography>
      </Grid>
      <Grid xs={2} display='flex' alignItems='center'>
      <Typography>Cierre</Typography>
      </Grid>
      <Grid xs={2} display='flex' alignItems='center'>
      <Typography>Cliente</Typography>
      </Grid>
      <Grid xs={4} display='flex' alignItems='center'>
      <Typography>Última tarea</Typography>
      </Grid>
      <Grid xs={1} display='flex' alignItems='center'>
        <Typography>Guardar</Typography>
      </Grid>
      <Grid xs={1} display='flex' alignItems='center'>
        <Typography>Borrar</Typography>
      </Grid>
    </Grid>
    {projects.map((e, i) => (
    <Grid container spacing={2} columns={13} key={i}>
      <Grid xs={1} display='flex' alignItems='center'>
        {i + 1}
      </Grid>
      <Grid xs={2} display='flex' alignItems='center'>
        <Link to={`localhost:5173/project/${e.id}`} >{e.name}</Link>
      </Grid>
      <Grid xs={2} display='flex' alignItems='center'>
        {e.ends.toDateString()}
      </Grid>
      <Grid xs={2} display='flex' alignItems='center'>
        {e.client}
      </Grid>
      <Grid xs={4} display='flex' alignItems='center'>
        <Collapse>
          <IconButton>
            <InsertLinkIcon sx={{fontSize: 42}} color="primary" />
          </IconButton>
        </Collapse>
      </Grid>
      <Grid xs={1} display='flex' alignItems='center'>
        <IconButton>
          <SaveIcon sx={{fontSize: 42}} color="primary" />
        </IconButton>
      </Grid>
      <Grid xs={1} display='flex' alignItems='center'>
        <IconButton>
          <DeleteForeverIcon sx={{fontSize: 42, color: pink[500]}} />
        </IconButton>
      </Grid>
    </Grid>
    ))}
  </>
);

export default ProjectSummary;