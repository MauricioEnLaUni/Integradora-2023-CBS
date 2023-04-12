import getCookie from "../../hooks/useCookie";
import getClaims from "./GetClaims";

const ClaimsFromCookies = (cookieName: string) => {
  const cookie: string | undefined = getCookie(cookieName);
  if (!cookie) return undefined;
  const claims = getClaims(cookie);
  return claims;
}

export default ClaimsFromCookies;