import React from 'react';
import { styled } from '@mui/system';
import { Stack, Button } from '@mui/material';

const Templater: React.FC = () => {
  const colors = [
    "hsl(210, 17%, 98%)",
    "hsl(210, 16%, 93%)",
    "hsl(210, 14%, 89%)",
    "hsl(210, 14%, 83%)",
    "hsl(210, 11%, 71%)",
    "hsl(208, 7%, 46%)",
    "hsl(210, 9%, 31%)",
    "hsl(210, 10%, 23%)",
    "hsl(210, 11%, 15%)"
  ]

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
        <FictPallete color={colors[0]}></FictPallete>
        <FictPallete color={colors[1]}></FictPallete>
        <FictPallete color={colors[2]}></FictPallete>
        <FictPallete color={colors[3]}></FictPallete>
        <FictPallete color={colors[4]}></FictPallete>
        <FictPallete color={colors[5]}></FictPallete>
        <FictPallete color={colors[6]}></FictPallete>
        <FictPallete color={colors[7]}></FictPallete>
        <FictPallete color={colors[8]}></FictPallete>
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
  border: '.05em solid',
  background: 'hsl(210, 11%, 15%)',
  font: "var(--font-ui)",
});

let FictPallete = styled('div')`
  background-color: ${(props: any) => props.color}
  height: 12vh;
  width: 12vh;
`;