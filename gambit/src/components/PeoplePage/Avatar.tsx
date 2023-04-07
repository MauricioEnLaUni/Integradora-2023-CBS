import React, { useEffect, useState } from "react";

const Avatar = ({ buffer }: { buffer: ArrayBuffer }) => {
  const [src, setSrc] = useState<string | undefined>(
    buffer ? URL.createObjectURL(new Blob([buffer], { type: 'image/avif' }))
      : undefined
  );
  const [imgFile, setImgFile] = useState<File | undefined>();
  
  const handleChanges = () => {
    
  }

  return <img src={src} />
};

export default Avatar;
    
  
    return (
        <div className="App">
            <h2>Add Image:</h2>
            <input type="file" onChange={handleChange} />
            <img src={file} />
  
        </div>
  
    );
}
  
export default App;