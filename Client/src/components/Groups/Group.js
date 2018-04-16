import React from "react";
import TeamRow from "./TeamRow";
import { Table } from "react-bootstrap";
import PropTypes from "prop-types";

const Group = props => {
  const group = props.match.params.char;
  const teams = props.location.teams;
  const groupTeams = getTeamsByGroup(teams, group);

  return (
    <Table striped bordered condensed hover>
      <thead>
        <tr>
          <th>CountryId</th>
          <th>Country</th>
        </tr>
      </thead>
      <tbody>
        {groupTeams.map(team => <TeamRow key={team.id} team={team} />)}
      </tbody>
    </Table>
  );
};

function getTeamsByGroup(teams, group) {
  const groupTeams = teams.filter(team => team.group === group);
  return groupTeams;
}

Group.propTypes = {
  teams: PropTypes.array.isRequired,
  match: PropTypes.shape({
    params: PropTypes.shape({
      char: PropTypes.string.isRequire
    })
  }),
  location: PropTypes.shape({
    teams: PropTypes.array.isRequired
  })
};

export default Group;
