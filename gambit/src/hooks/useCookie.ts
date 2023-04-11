const getCookie = (key: string) => {
  const b: RegExpMatchArray | null =
      document.cookie.match("(^|;)\\s*" + key + "\\s*=\\s*([^;]+)");
  return b ? b.pop() : "";
}

export default getCookie;