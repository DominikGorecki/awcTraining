import React, { useState } from "react";
import { useField, useFormikContext } from "formik";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { Form } from "react-bootstrap";

export const DatePickerField = ({ ...props }) => {
    const { setFieldValue, errors } = useFormikContext();
    const [field] = useField(props);
    const [dateTouched, setDateTouched] = useState(false);

    const invalid = () => dateTouched && errors[field.name]; 
    return (
        <>
            <div className={`prefDate`}>
                <DatePicker
                    className={`form-control ${invalid() ? 'is-invalid' : ''}`}
                    {...field}
                    {...props}
                    onBlur={() => setDateTouched(true)}
                    selected={(field.value && new Date(field.value)) || null}
                    onChange={val => {
                        setFieldValue(field.name, val);
                        setDateTouched(true);
                    }}
                />
                {dateTouched && errors[field.name] &&
                    <Form.Control.Feedback className="d-block" type="invalid">
                        {errors[field.name]}
                    </Form.Control.Feedback>}
            </div>

        </>

    );
};

export default DatePickerField;