// React imports
import { SyntheticEvent, useEffect, useState } from "react";

// MUI
import Box from "@mui/material/Box";
import Tab from "@mui/material/Tab";
import Tabs from "@mui/material/Tabs";

// Fictichos
import AddressDto from "../../models/Response/Contact/AddressDto";
import AddressPanel from "./AddressPanel";
import IconButton from "@mui/material/IconButton";
import Tooltip from '@mui/material/Tooltip';
import Icon from '@mui/material/Icon';
import AddCircleIcon from '@mui/icons-material/AddCircle';
import AddAddressModal from "../PeoplePage/AddAddressModal";

const a11yProps = (index: number) => {
  return {
    label: `Address ${index + 1}`,
    id: `address-tab-${index}`,
    'aria-controls': `simple-tabpanel-${index}`,
    key: `${index}`
  };
}

const AddressTab = ({ addresses, owner, token, refresh, setRefresh }: {addresses: Array<AddressDto>, owner: string, token: string, refresh: boolean, setRefresh: any }) => {
  const [value, setValue] = useState(0);

  const handleChange = (event: SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };

  useEffect(() => {
  }, [refresh]);

  return (
    <Box sx={{ width: '100%' }} >
      { addresses && addresses.length > 0 ? 
      <Box sx={{ borderBottom: 1 }} >
        <Tabs value={value} onChange={handleChange} aria-label="address container">
          {addresses?.map((e, i) => (
            <Tab {...a11yProps(i)} />
          ))}
          <AddAddressModal token={token} refresh={refresh} setRefresh={setRefresh} owner={owner} />
        </Tabs>
      </Box> : <></> }
      { addresses && addresses.length > 0 ?
      addresses?.map((e, i) => (
        <div
          role="tabpanel"
          hidden={value !== i}
          id={`address-tabpanel-${i}`}
          aria-labelledby={`address-tab-${i}`}
          key={i}
        >
          <AddressPanel address={e} tab={i}/>
        </div>
      )) : <AddAddressModal token={token} refresh={refresh} setRefresh={setRefresh} owner={owner} />}
    </Box>
  );
}

export default AddressTab;