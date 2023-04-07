import { styled } from '@mui/material/styles';
import Card, { CardTypeMap } from "@mui/material/Card";
import { OverridableComponent } from '@mui/material/OverridableComponent';

const ImgContainer: OverridableComponent<CardTypeMap<{}, "div">> =
styled(Card)(({ theme }) => ({
  backgroundColor: '#1A2027',
  padding: theme.spacing(2),
  textAlign: 'center',
  color: theme.palette.text.secondary,
  maxWidth: '296px',
  borderRadius: '100vh'
}));

const Profile = (profilePicture: string) => (
  <ImgContainer>
    <img src={profilePicture}/>
  </ImgContainer>
);

export default Profile;