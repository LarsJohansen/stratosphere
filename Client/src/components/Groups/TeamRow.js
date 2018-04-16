import React from "react";
import Proptypes from "proptypes";

const TeamRow = ({ team }) => {
  return (
    <tr>
      <td>{team.id}</td>
      <td>{team.name}</td>
    </tr>
  );
};

TeamRow.propTypes = {
  team: Proptypes.object.isRequired
};

export default TeamRow;
