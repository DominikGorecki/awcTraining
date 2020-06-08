import React, { useState, useEffect } from 'react';
import { Formik } from 'formik';
import * as Yup from 'yup';
import Form from 'react-bootstrap/Form';
import Col from 'react-bootstrap/Col';
import Card from 'react-bootstrap/Card';
import Creatable from 'react-select/creatable';

import TextAreaControl from '../../components/TextAreaControl';
import FormButton from '../../components/FormButton';

import { GetActivities, PutSignup } from './actions'
import PrefferedStartInput from './PrefferedStartInput';
import { FirstNameInput, 
    LastNameInput, 
    EmailInput,
    ExperienceInput } from './FormInputs';
import ApiErrors from '../../components/ApiErrors';
import { Link } from 'react-router-dom';

const yesterday = new Date();
yesterday.setDate(yesterday.getDate() - 1);

const schema = Yup.object({
    firstName: Yup
        .string()
        .required("First name required"),
    lastName: Yup
        .string()
        .required("Last name required"),
    email: Yup
        .string()
        .email("Invalid email")
        .required("Required"),
    timeOfDayHour: Yup
        .number()
        .max(24, "Hour has to be between 0 and 24")
        .min(0, "Hour has to be between 0 and 24")
        .required("hour required"),
    timeOfDayMin: Yup
        .number()
        .max(60, "Minutes have to be between 0 and 60")
        .min(0, "Minutes have to be between 0 and 60")
        .required("Required"),
    yearsExperience: Yup
        .number()
        .min(0, "Can't be negative")
        // Purposely allowing the input to be higher than the api allows 
        // to show what happens there is an error from the API call.
        .max(101, "Really? I don't believe you have this much experience")
        .required(),
    prefferedStart: Yup
        .date()
        .min(yesterday, "Can't book into the past")
        .required("Required"),
    comments: Yup
        .string()
});

const NewSignup = () => {
    const [activities, setActivities] = useState(null);
    const [apiErrors, setApiErrors] = useState(null);
    const [activityId, setActivityId] = useState(null);
    const [activityName, setActivityName] = useState(null);
    const [activityTouch, setActivityTouch] = useState(false);
    const [complete, setComplete] = useState(false);

    useEffect(() => {
        (async () => {
            const response = await GetActivities();
            if (response.success) {
                var actv = response.value.map(a => {
                    return {
                        value: a.id,
                        label: a.name
                    };
                });
                setActivities(actv);
            }
            else
                setApiErrors(response.errors);
        })();
    }, [])

    const handleChange = (newValue, actionMeta) => {
        console.log(newValue);
        setActivityTouch(true);
        if(actionMeta.action === 'create-option')
        {
            setActivityId(null);
            setActivityName(newValue.value);
        }
        else 
        {
            setActivityName(newValue.label);
            setActivityId(newValue.value);
        }
    };

    const ActivityInput = () => (
        <Form.Group
            as={Col}
            xs="9"
            lg="10"
            controlId="activity">
            <label className="form-label">Activity</label>
            <Creatable
                placeholder="Select Activity or Create New One..."
                isLoading={!activities}
                options={activities}
                value={activityName ? {label: activityName} : null}
                onBlur={() => setActivityTouch(true)}
                onChange={handleChange}
            />
            {activityTouch && (!activityId || !activityName) &&
            <Form.Control.Feedback className="d-block" type="invalid">
                Required
            </Form.Control.Feedback>
            }
        </Form.Group>
    );

    const CommentInput = ({ formik }) => (
        <Form.Group
            xs="12"
            as={Col}
            controlId="comments">
            <TextAreaControl
                formik={formik}
                name="comments"
                label="Comments"
            />
        </Form.Group>
    );

    const saveInput = async (values, { setSubmitting }) => {
        console.log(values);
        setSubmitting(true);
        setActivityTouch(true);
        if(activityId)
        {
            values.activityId = activityId;
        }
        else if (activityName)
        {
            values.activityName = activityName;
        }
        else return;

        var response = await PutSignup(values);
        setSubmitting(false);
        if(response.success) setComplete(true);
        else setApiErrors(response.errors);
            
    };

    if(complete)
        return(
            <Card body>
                <h1>Complete</h1>
                <hr/>
                <Link to="/signups">Check out existing signups</Link>
            </Card>

        );

    return (
        <Card body>
            <h1>New Signup</h1>
            <p><strong>Note: </strong>Purposefully allow years of experience be greater than API allows to show what api errors will look like if they aren't caught by the front-end. Try "101" years of experience and fill in the rest of the form correctly</p>
            <Formik
                validationSchema={schema}
                onSubmit={saveInput}
                initialValues={{
                    firstName: "",
                    lastName: "",
                    email: "",
                    timeOfDayHour: "",
                    timeOfDayMin: "",
                    yearsExperience: 0,
                    prefferedStart: new Date().toISOString().split('T')[0],
                }}
            >
                {formik => (
                    <Form noValidate onSubmit={formik.handleSubmit}>
                        <h5>Personal Info</h5>
                        <Form.Row className="justify-content-md-center">
                            <FirstNameInput formik={formik} />
                            <LastNameInput formik={formik} />
                            <EmailInput formik={formik} />
                        </Form.Row>
                        <hr />
                        <h5>Activity</h5>
                        <Form.Row className="justify-content-md-center">
                            <ActivityInput />
                            <ExperienceInput formik={formik} />
                        </Form.Row>
                        <Form.Row className="justify-content-md-center">
                            <CommentInput formik={formik} />
                        </Form.Row>

                        <h6>Preffered Start Time and Date</h6>
                        <PrefferedStartInput formik={formik} />
                        <FormButton
                            formik={formik}
                            label="Add Signup" />
                    </Form>
                )}
           </Formik>
           {apiErrors && 
           <ApiErrors errors={apiErrors} />
            }
        </Card>
    );
};

export default NewSignup;