import React from 'react';
import Form from 'react-bootstrap/Form';
import Col from 'react-bootstrap/Col';
import FormControl from '../../components/FormControl';
import DatePicker from '../../components/DatePicker';

/*
const TimeOfDayInput = ({ formik }) => (
    <Form.Row className="justify-content-md-center">
        <Form.Group
            as={Col}
            xs="6"
            controlId="timeOfDayHour">
            <Form.Label>Hour</Form.Label>
            <Form.Control
                type="number"
                name="timeOfDayHour"
                value={formik.values.timeOfDayHour}
                onChange={formik.handleChange}
                onBlur={formik.handleBlur}
                disabled={formik.isSubmitting}
                isInvalid={formik.touched.timeOfDayHour && formik.errors.timeOfDayHour}
            />
        </Form.Group>
        <Form.Group
            as={Col}
            xs="6"
            controlId="timeOfDayMin">
            <Form.Label>Minutes</Form.Label>
            <Form.Control
                type="number"
                name="timeOfDayMin"
                value={formik.values.timeOfDayMin}
                onChange={formik.handleChange}
                onBlur={formik.handleBlur}
                disabled={formik.isSubmitting}
                isInvalid={formik.touched.timeOfDayMin && formik.errors.timeOfDayMin}
            />
        {formik.touched.timeOfDayMin && formik.touched.timeOfDayHour && 
            formik.errors.timeOfDayHour &&
            <Form.Control.Feedback type="invalid">
                {formik.errors.timeOfDayHour} &nbsp;
                {formik.errors.timeOfDayMin}
            </Form.Control.Feedback>
        }
        </Form.Group>

    </Form.Row>
);
*/
const PrefferedStartInput = ({ formik }) => (
    <Form.Row className="justify-content-md-center">
        <Form.Group
            as={Col}
            xs="4"
            controlId="timeOfDayHour">
            <FormControl
                type="number"
                formik={formik}
                name="timeOfDayHour"
                label="Hour"
                placeholder="HH"
                />
        </Form.Group>
        <Form.Group
            as={Col}
            xs="4"
            controlId="timeOfDayMin">
            <FormControl
                type="number"
                formik={formik}
                name="timeOfDayMin"
                label="Minute"
                placeholder="MM"
                />
        </Form.Group>
        <Form.Group
            as={Col}
            xs="4"
            controlId="prefferedStart">
            <Form.Label>Date</Form.Label>
            <DatePicker name="prefferedStart" />
        </Form.Group>

    </Form.Row>
);

export default PrefferedStartInput;