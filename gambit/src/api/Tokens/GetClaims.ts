import Claim from "./Claims";

const getClaims = (jwt: string) => {
  const [, payloadBase64] = jwt.split('.');
  const payloadJson = atob(payloadBase64);
  const claims = JSON.parse(payloadJson) as Claim[];
  return new Set<Claim>(claims);
}

export default getClaims;