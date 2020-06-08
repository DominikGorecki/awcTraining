import React from 'react';
import Alert from 'react-bootstrap/Alert';

const ApiErrors = ({errors}) => { 
  if(!Array.isArray(errors)) errors = ['Error'];

  return (
    <>
        {
            errors.map((e,i) => (
            <Alert 
                show={true}
                variant="dark"
                key={i} className="mt-1">
                {e}
            </Alert>))
        }
    </>
  );
};


export default ApiErrors;