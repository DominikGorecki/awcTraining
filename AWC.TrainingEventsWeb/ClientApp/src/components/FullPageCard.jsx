import React from 'react';
import Card from 'react-bootstrap/Card';
import Row from 'react-bootstrap/Row';
import Container from 'react-bootstrap/Container';
import Col from 'react-bootstrap/Col';

const FullPageCard = (
  { 
    title, 
    children,
    fullWidth
  }) => {

  const colProps = fullWidth ? 
    { 
      xs: 12 
    } : 
    {
      xs: 12,
      md: 10,
      lg: 8,
      xl: 6
    };
    
  return (
    <Container className="pt-2">
      <Row className="justify-content-md-center">
        <Col {...colProps} >
          <Card>
            <Card.Header>{title}</Card.Header>
            <Card.Body >
              {children}
            </Card.Body>
          </Card>
        </Col>
      </Row> 
    </Container>
  );
};

export default FullPageCard;