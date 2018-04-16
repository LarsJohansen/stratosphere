import React from "react";
import { Switch, Route } from "react-router-dom";
import Group from "./Group";
import AllGroups from "./AllGroups";

const Groups = () => (
  <Switch>
    <Route exact path="/groups" component={AllGroups} />
    <Route path="/groups/:char" component={Group} />
  </Switch>
);

export default Groups;
