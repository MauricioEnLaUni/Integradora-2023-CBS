import { createGlobalStyle } from "styled-components";

interface themes {
  body: string;
  text: string;
  toggleBorder: string;
  background: string;
}

export const darkTheme = {
  body: 'hsl(210, 11%, 15%)',
  text: 'hsl(210, 17%, 98%)',
  toggleBorder: 'hsl(210, 11%, 15%)',
  background: 'hsl(210, 17%, 98%)'
}

export const lightTheme = {
  body: 'hsl(210, 17%, 98%)',
  text: 'hsl(210, 11%, 15%)',
  toggleBorder: 'hsl(210, 17%, 98%)',
  background: 'hsl(210, 11%, 15%)',
}

export const GlobalStyles = createGlobalStyle`
  body {
    background: ${({ theme } : { theme: themes}) => theme.body };
    color: ${({ theme } : { theme: themes}) => theme.text };
    font-family: Tahome, Helvetica, Arial, Roboto, sans-serif;
    transition: all 0.50 linear;
    margin: 0;
    padding: 0;
  }
`;