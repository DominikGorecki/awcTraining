import React, { Component } from 'react';
import { Container, Navbar, NavbarBrand, NavItem, NavLink } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap'

import './NavMenu.css';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3">
          <Container>
            <LinkContainer to="/">
              <NavbarBrand>AWC Training Events</NavbarBrand>
            </LinkContainer>
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <LinkContainer to="/">
                    <NavLink className="text-dark">Home</NavLink>
                  </LinkContainer>
                </NavItem>
                <NavItem>
                  <LinkContainer to="/signups">
                    <NavLink className="text-dark">Signups</NavLink>
                  </LinkContainer>
                </NavItem>
                <NavItem>
                  <LinkContainer to="/new">
                    <NavLink className="text-dark">New</NavLink>
                  </LinkContainer>
                </NavItem>
              </ul>
          </Container>
        </Navbar>
      </header>
    );
  }
}
