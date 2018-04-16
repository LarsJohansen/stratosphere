import React from "react";
import { Switch, Route } from "react-router-dom";
import Home from "./Home";
import Groups from "./Groups/Groups";
import { PropTypes } from "prop-types";

const Main = (props) => (
  <main>
    <Switch>
      <Route exact path="/" component={Home} />
      <Route path="/groups" component={Groups} />
    </Switch>
  </main>
);

Main.propTypes = {
  groups: PropTypes.array.isRequired,
  teams: PropTypes.array.isRequired
};

export default Main;
