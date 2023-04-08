import * as React from 'react';
import { useState } from 'react';
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import Box from '@mui/material/Box';
import ContactDto from '../../models/Response/Contact/ContactDto';
import StringTab from './StringTab';
import AddressTab from './AddressTab';

interface TabPanelProps {
  children?: React.ReactNode;
  index: number;
  value: number;
}

const TabPanel = (props: TabPanelProps) => {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`contact-tabpanel-${index}`}
      aria-labelledby={`contact-tab-${index}`}
      {...other}
    >
      {value === index && (
        <Box sx={{ p: 3 }}>
          {children}
        </Box>
      )}
    </div>
  );
}

const ContactContainer = ({ contact }: { contact: ContactDto }) => {
  const [value, setValue] = useState(0);

  const handleChanges = (event: React.SyntheticEvent, newValue: number) => {
    setValue(newValue);
  }

  const a11yProps = (index: number) => {
    const l = ['Address', 'Phones', 'Email']
    return {
      label: l[index],
      id: `contact-tab-${index}`,
      'aria-controls': `contact-tabpanel-${index}`,
      key: `${index + 10}`
    };
  }

  return(
    <Box>
      <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
        <Tabs value={value} onChange={handleChanges} aria-label="contact container">
            <Tab {...a11yProps(0)} />
            <Tab {...a11yProps(1)} />
            <Tab {...a11yProps(2)} />
        </Tabs>
      </Box>
      <TabPanel value={value} index={0}>
        <AddressTab addresses={contact.Addresses} />
      </TabPanel>
      <TabPanel value={value} index={1}>
        <StringTab data={contact.Phones} name={"Phone"}/>
      </TabPanel>
      <TabPanel value={value} index={2}>
        <StringTab data={contact.Emails} name={"Email"}/>
      </TabPanel>
    </Box>
  );
}

export default ContactContainer;