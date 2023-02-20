import React from 'react';
import { style, styled } from '@mui/system';
import { Stack, Button } from '@mui/material';

const Templater: React.FC = () => {
  return (
    <>
      <div>
        <h1>Test</h1>
        <h2>Test</h2>
        <h3>Test</h3>
        <h4>Test</h4>
        <h5>Test</h5>
        <h6>Test</h6>
        <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Accusamus facilis pariatur sed porro error recusandae deleniti nisi harum sit iste iusto voluptatibus, deserunt illo magnam nemo rem tenetur ab aliquid?</p>
      </div>
      <Stack direction="row">
        <Button variant="text">Text</Button>
        <Button variant="contained">Contained</Button>
        <Button variant="outlined">Outlined</Button>
        <Button disabled>Disabled</Button>
        <Button href="#text-buttons">Link</Button>
        <FictButton >Testing</FictButton>
    </Stack>
    </>
  )
}

export default Templater;

const FictButton = styled(Button) ({
  boxShadow: 'none',
  textTransform: 'uppercase',
  fontSize: '2.25em',
  margin: 'auto',
  padding: '0 .25em',
  border: '.05em solid'
})