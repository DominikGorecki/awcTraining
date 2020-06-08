import React from 'react';
import Form from 'react-bootstrap/Form';
import TextareaAutozie from 'react-textarea-autosize';

const TextAreaControl = ({ 
  formik,
  name,
  label,
  ...rest
}) => (
  <>
    {label &&
    <Form.Label>{label}</Form.Label>
    }
    <Form.Control
      as={TextareaAutozie}
      {...rest}
      name={name}
      value={formik.values[name]}
      onChange={formik.handleChange}
      onBlur={formik.handleBlur}
      disabled={formik.isSubmitting}
      isInvalid={formik.touched[name] && formik.errors[name]}
    />
    {formik.touched[name] && formik.errors[name] &&
      <Form.Control.Feedback type="invalid">
        {formik.errors[name]}
      </Form.Control.Feedback>
    }
  </>
);

export default TextAreaControl;