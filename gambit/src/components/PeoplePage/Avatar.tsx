import React, { ChangeEvent, useState } from "react";

const Avatar = ({ buffer }: { buffer: ArrayBuffer }) => {
  const [src, setSrc] = useState<string | undefined>(
    buffer ? URL.createObjectURL(new Blob([buffer], { type: 'image/*' }))
      : undefined
  );
  const [imgFile, setImgFile] = useState<File | undefined>();
  
  const handleChanges = (event: ChangeEvent<HTMLInputElement>) => {
    const files = event.target.files;
    if (!files || files.length === 0) return;
    const file = files[0];
    setSrc(URL.createObjectURL(file));
    setImgFile(file);
  }

  return(
    <div>
      <img src={src} width={'296px'} height={'296px'} />
      <input type="file" accept="image/*" onChange={handleChanges} />
    </div>
  );
};

export default Avatar;