// React imports
import { SyntheticEvent, useState } from "react";

// MUI
import Box from "@mui/material/Box";
import Tab from "@mui/material/Tab";
import Tabs from "@mui/material/Tabs";

// Fictichos
import AddressDto from "../../models/Response/Contact/AddressDto";
import AddressPanel from "./AddressPanel";

const a11yProps = (index: number) => {
  return {
    label: `Address ${index + 1}`,
    id: `address-tab-${index}`,
    'aria-controls': `simple-tabpanel-${index}`,
    key: `${index}`
  };
}

const AddressTab = ({addresses}: {addresses: Array<AddressDto>}) => {
  const [value, setValue] = useState(0);

  const handleChange = (event: SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };

  return (
    <Box sx={{ width: '100%' }} >
      <Box sx={{ borderBottom: 1 }} >
        <Tabs value={value} onChange={handleChange} aria-label="address container">
          {addresses?.map((e, i) => (
            <Tab {...a11yProps(i)} />
          ))}
        </Tabs>
      </Box>
      {addresses?.map((e, i) => (
        <div
          role="tabpanel"
          hidden={value !== i}
          id={`address-tabpanel-${i}`}
          aria-labelledby={`address-tab-${i}`}
          key={i}
        >
          <AddressPanel address={e} tab={i}/>
        </div>
      ))}
    </Box>
  );
}

export default AddressTab;