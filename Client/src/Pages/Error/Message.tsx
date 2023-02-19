import React from 'react';

interface MSGProps {
  payload: number;
}

const messages = {
  404: () => <div>Page not Found</div>
}

const Missing: React.FC<MSGProps> = (props): JSX.Element => {
  let msg: JSX.Element;

  switch(props.payload){
    default:
      msg = messages[404]();
  }
  return msg;
}

export default Missing;