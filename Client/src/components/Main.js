import React from "react";
import { Switch, Route } from "react-router-dom";
import Home from "./Home";
import Groups from "./Groups";

const Main = () => (
  <main>
    <Switch>
      <Route exact path="/" component={Home} />
      <Route path="/groups" component={Groups} />
    </Switch>
  </main>
);

export default Main;
