import React from "react";
import GroupApi from "../api/mockGroupApi";
import TeamApi from "../api/mockTeamApi";
import { Link } from "react-router-dom";
import TeamRow from "./TeamRow";
import { Table } from "react-bootstrap";
import Proptypes from "proptypes";

// The Player looks up the player using the number parsed from
// the URL's pathname. If no player is found with the given
// number, then a "player not found" message is displayed.
const Group = props => {
  const group = GroupApi.get(props.match.params.char);
  if (!group) {
    return <div>Sorry, group not found</div>;
  }
  const teams = TeamApi.getTeamsInGroup(props.match.params.char);
  return (
    <Table striped bordered condensed hover>
      <thead>
        <tr>
          <th>CountryId</th>
          <th>Country</th>
        </tr>
      </thead>
      <tbody>
        {teams.map(team => (
          <TeamRow key={team.id} team={team} />
        ))}
      </tbody>
    </Table>
  );
};

export default Group;
