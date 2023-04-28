// React
import { Link } from 'react-router-dom';
// MUI
import styled from '@emotion/styled';
import Stack from '@mui/material/Stack/Stack';
import AppBar from '@mui/material/AppBar';
import Typography from '@mui/material/Typography/Typography';
import Divider from '@mui/material/Divider/Divider';
import Box from '@mui/material/Box/Box';
// MUI Icons
import CalendarMonthOutlinedIcon from '@mui/icons-material/CalendarMonthOutlined';
// Assets
import HeaderBanner from '../../assets/logo-instituto-de-salud.png';
import HeaderBg from '../../assets/istockphoto-619260624-612x612.jpg';

const NavLinks = [
  { text: 'inicio', to: '#' },
  { text: 'Acerca del ISSEA', to: '#' },
  { text: 'enlaces', to: '#' },
  { text: 'test1', to: '#' },
  { text: 'test2', to: '#' },
  { text: 'test3', to: '#' },
  { text: 'test4', to: '#' },
  { text: 'test5', to: '#' },
  { text: 'test6', to: '#' }
];

const padTo2Digits = (num: number) => (
  num.toString().padStart(2, '0')
);

const toDateString = (date: Date) => (
  [
    date.getFullYear(),
    padTo2Digits(date.getMonth() + 1),
    padTo2Digits(date.getDate())
  ].join('-') + ' ' + 
  [
    padTo2Digits(date.getHours()),
    padTo2Digits(date.getMinutes()),
    padTo2Digits(date.getSeconds())
  ].join(':')
);

const Header = () => (
  <HeaderStyled>
    <HeaderBigDivision sx={{ display: 'flex' }} spacing={2}>
      <Stack direction={'row'} sx={{ paddingTop: .25, display: 'flex', justifyContent: 'center', color: 'hsl(0,0%,10.2%)' }} spacing={12}>
        <Box>
          <Typography sx={{ display: 'flex', justifyContent: 'center' }}>
            <CalendarMonthOutlinedIcon />
            {toDateString(new Date())}
          </Typography>
        </Box>
        <Box>
          <Typography sx={{ display: 'flex', justifyContent: 'center' }}>
            No recuerdo que iba de este lado
          </Typography>
        </Box>
        <Box>
          <Typography sx={{ display: 'flex', justifyContent: 'center' }}>
            No recuerdo que iba de este lado
          </Typography>
        </Box>
        <Box>

        </Box>
      </Stack>
      <Box sx={{ display: 'flex', justifyContent: 'center' }}>
        <Banner src={HeaderBanner} />
      </Box>
      <Box sx={{ backgroundColor: 'hsla(215.6,49.5%,38%,0.95)', minHeight: '36px', display: 'flex', justifyContent: 'center' }}>
        <HeaderSmallDivision
          direction={'row'}
          divider={<Divider orientation='vertical' flexItem/>}
        >
          {NavLinks.map(e => (
            <HeaderLink to={e.to} key={e.text}>
              <Typography sx={{ fontFamily: 'Arial', fontWeight: 700, fontSize: '1.15rem' }}>
                {e.text.toUpperCase()}
              </Typography>
            </HeaderLink>
          ))}
        </HeaderSmallDivision>
      </Box>
    </HeaderBigDivision>
  </HeaderStyled>
);

export default Header;

const HeaderStyled = styled(AppBar)(() => ({
  backgroundColor: 'hsla(0,0%,92.2%,1)'
}));

const Banner = styled('img')(() => ({
  maxHeight: '100px',
  width: '87vw'
}));

const HeaderBigDivision = styled(Stack)(() => ({
  backgroundPosition: 'center',
  backgroundRepeat: 'no-repeat',
  backgroundSize: 'cover'
}));

const HeaderSmallDivision = styled(Stack)(() => ({
  justifyContent: 'space-evenly',
  width: '87vw'
}));

const HeaderLink = styled(Link)(() => ({
  alignItems: 'center',
  color: 'hsl(0,0%,94.9%)',
  display: 'flex',
  fontWeight: 700,
  justifyContent: 'center',
  width: '100%',
  '&:hover': {
    backgroundColor: 'hsl(194.3,84.9%,53.3%)',
    color: 'hsl(0,0%,10.2%)'
  }
}));