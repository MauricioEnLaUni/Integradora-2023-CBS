import { useNavigate } from 'react-router-dom';
import { ElementType } from 'react';
import { IconType } from 'react-icons';

import * as short from 'short-uuid';
import styled from '@emotion/styled';
import Icon from '@mui/material/Icon';
import IconButton from '@mui/material/IconButton';
import Grid from '@mui/material/Grid';
import Tooltip from '@mui/material/Tooltip';

export default class FictDashboardButton {
  public Id: string = short.generate();
  public Text: string;
  public Icon: ElementType<IconType>;
  public Route: string = '';

  constructor(text: string, route: string, icon: ElementType<IconType>)
  {
    this.Text = text;
    this.Route = route;
    this.Icon = icon;
  }
  
  public static DrawDashboard(buttons: Array<FictDashboardButton>)
  {
    const navigate = useNavigate();
    
    return(
      <Grid container direction={'row'}>
        {buttons.map((button) => (
          <Grid key={button.Id} item={true} xs={6} sx={{ display: 'flex', justifyContent: 'center', p: 5 }}>
            <FDBB
              onClick={() => navigate(button.Route)}
              aria-label={button.Text}
            >
              <Tooltip title={button.Text} arrow>
                <Icon
                  className={'inner-icon'}
                  component={button.Icon}
                  sx={{ color: '#e4f1f1', fontSize: '56px' }}
                />
              </Tooltip>
            </FDBB>
          </Grid>
        ))}
      </Grid>
    );
  };
}

const FDBB = styled(IconButton)(({ theme }) => ({
  backgroundColor: '#0f182a',
  border: '4px double',
  borderColor: '#b1d7d7',
  color: '#e1e7f4',
  height: 160,
  width: 160,
  '&:hover': {
    backgroundColor: '#233862',
    border: '5px solid',
    borderColor: '#c2d1e0',
    '& .inner-icon': {
      color: '#b2d7d7'
    }
  }
}));