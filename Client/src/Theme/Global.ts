import { createGlobalStyle } from "styled-components";
import "@fontsource/m-plus-1";
import "@fontsource/noto-sans";
import "@fontsource/prompt";

interface themes {
  body: string;
  text: string;
  toggleBorder: string;
  background: string;
}

export const darkTheme = {
  body: 'hsl(210, 9%, 31%)',
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
    --font-common: "M PLUS 1";
    --font-title: "Prompt";
    --font-ui: "Noto Sans";
    background: ${({ theme } : { theme: themes}) => theme.body };
    color: ${({ theme } : { theme: themes}) => theme.text };
    font-family: var(--font-common), Tahome, Helvetica, Arial, Roboto, sans-serif;
    transition: all 0.50 linear;
    margin: 0;
    padding: 0;
  }
`;