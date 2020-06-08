import React from 'react';
import Row from 'react-bootstrap/Row';
import Container from 'react-bootstrap/Container';
import Col from 'react-bootstrap/Col';
import Spinner from 'react-bootstrap/Spinner';

const FullPageLoading = ({ title, children}) => (
  <Container className="pt-2">
    <Row className="justify-content-center">
      <Col xs md={10} lg={8} xl={6}>
        <div className="m-auto text-center p-5 justify-content-center">
          <Spinner
            animation="border"
            role="status"
          />
        </div>
      </Col>
    </Row> 
  </Container>
);

export default FullPageLoading;