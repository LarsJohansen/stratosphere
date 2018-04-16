import React, { Component } from "react";
import Header from "./Header";
import Main from "./Main";
import PropTypes from "proptypes";

class App extends Component {
  render() {
    return (
      <div>
        <Header />
        <Main />
      </div>
    );
  }
}
export default App;
