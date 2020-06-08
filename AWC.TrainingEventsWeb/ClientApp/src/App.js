import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import Home from './scenes/Home';
import Signups from './scenes/Signups'
import NewSignup from './scenes/NewSignup'

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/signups' component={Signups} />
        <Route path='/new' component={NewSignup} />
      </Layout>
    );
  }
}
