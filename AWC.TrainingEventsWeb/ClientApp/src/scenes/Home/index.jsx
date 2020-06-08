import React from 'react';
import Button from 'react-bootstrap/Button';
import FullPageCard from '../../components/FullPageCard';
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';

import { LinkContainer } from 'react-router-bootstrap'

const Home = ({history}) => {
    return (
        <FullPageCard title={
            <h3>What would you like to do?</h3>
        }>
            <Row>
            <Col> 
                <LinkContainer to="/signups">
                    <Button block size="lg">View Signups</Button>
                </LinkContainer>
            </Col>
            <Col > 
                <LinkContainer to="/new">
                    <Button block size="lg">New Signup</Button>
                </LinkContainer>
            </Col>
            </Row>
        </FullPageCard>
    )
}

export default Home; 
