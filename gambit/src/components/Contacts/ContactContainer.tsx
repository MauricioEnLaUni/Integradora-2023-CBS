import * as React from 'react';
import { useEffect, useState } from 'react';
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import Box from '@mui/material/Box';
import ContactDto from '../../models/Response/Contact/ContactDto';
import StringTab from './StringTab';
import AddressTab from './AddressTab';
import Accordion from '@mui/material/Accordion';
import AccordionSummary from '@mui/material/AccordionSummary';
import AccordionDetails from '@mui/material/AccordionDetails';
import CompanyDto from '../../models/Response/People/Company.Dto';

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

const ContactContainer = ({ company, token }: { company: CompanyDto, token: string }) => {
  const [value, setValue] = useState(0);
  const [refresh, setRefresh] = useState(false);
  const contact = company.contact;

  const handleChanges = (event: React.SyntheticEvent, newValue: number) => {
    event.stopPropagation();
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

  useEffect(() => {
  }, [refresh]);

  return(
  <Accordion sx={{ display: 'block', overflow: 'auto', maxHeight: '400px' }} disableGutters>
    <AccordionSummary sx={{ borderBottom: 1 }}>
      <Tabs value={value} onChange={handleChanges} aria-label="contact container">
          <Tab {...a11yProps(0)} />
          <Tab {...a11yProps(1)} />
          <Tab {...a11yProps(2)} />
      </Tabs>
    </AccordionSummary>
    <AccordionDetails>
      <TabPanel value={value} index={0}>
        { contact && contact.addresses ? <AddressTab addresses={contact.addresses} owner={company.id} token={token} refresh={refresh} setRefresh={setRefresh} /> : <p>Loading...</p> }
      </TabPanel>
      <TabPanel value={value} index={1}>
        <StringTab data={contact.phones} name={"Phone"}/>
      </TabPanel>
      <TabPanel value={value} index={2}>
        <StringTab data={contact.emails} name={"Email"}/>
      </TabPanel>
    </AccordionDetails>
  </Accordion>
  );
}

export default ContactContainer;