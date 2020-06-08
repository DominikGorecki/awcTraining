import React from 'react';
import Form from 'react-bootstrap/Form';
import Col from 'react-bootstrap/Col';
import FormControl from '../../components/FormControl';


export const FirstNameInput = ({ formik }) => (
    <Form.Group as={Col} lg="6" controlId="firstName">
        <FormControl
            type="text"
            formik={formik}
            name="firstName"
            label="First Name"
        />
    </Form.Group>
);

export const LastNameInput = ({ formik }) => (
    <Form.Group
        as={Col}
        lg="6"
        controlId="lastName">
        <FormControl
            type="text"
            formik={formik}
            name="lastName"
            label="Last Name"
        />
    </Form.Group>
);

export const EmailInput = ({ formik }) => (
    <Form.Group
        as={Col}
        controlId="email">
        <FormControl
            type="text"
            formik={formik}
            name="email"
            label="Email"
        />
    </Form.Group>
);

export const ExperienceInput = ({ formik }) => (
    <Form.Group
        as={Col}
        xs="3"
        lg="2"
        controlId="yearsExperience">
        <FormControl
            type="number"
            formik={formik}
            name="yearsExperience"
            label="Experience (yrs)"
        />
    </Form.Group>
);
