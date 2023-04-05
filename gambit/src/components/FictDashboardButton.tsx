import * as short from 'short-uuid';
import Button from '@mui/material/Button';

interface DashboardButtonIcon {
  muiName: string;
}

export default class FictDashboardButton {
  public Id: string = short.generate();
  public Text: string;
  public Icon: DashboardButtonIcon;
  public Route: string = '';

  constructor(text: string, route: string, icon: DashboardButtonIcon)
  {
    this.Text = text;
    this.Route = route;
    this.Icon = icon;
  }
  
  public static DrawDashboard(buttons: Array<FictDashboardButton>)
  {
    const handleButtonClick = (route: string) => {

    };
    
    return(
      <div>
        {buttons.map((button) => (
          <Button
            key={button.Id}
            variant="contained"
            onClick={() => handleButtonClick(button.Route)}
            aria-label={button.Text}
            startIcon={<span className={`test icon-${button.Icon.muiName}`} />}
          >
            {button.Text}
          </Button>
        ))}
      </div>
    );
  };
}