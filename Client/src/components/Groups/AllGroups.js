import React from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import { Table } from "react-bootstrap";
import { PropTypes } from "prop-types";
import { bindActionCreators } from "redux";
import * as groupActions from "../../actions/groupActions";
import * as teamActions from "../../actions/teamActions";

class AllGroupsPage extends React.Component {
  render() {
    const { groups, teams } = this.props;
    function groupPath(groupName) {
      return {
        pathname: "/groups/" + groupName,
        teams: teams
      };
    }

    return (
      <Table striped bordered condensed hover>
        <thead>
          <tr>
            <th>Group</th>
          </tr>
        </thead>
        <tbody>
          {groups.map(group => (
            <tr key={group.id}>
              <td>
                <Link to={groupPath(group.name)}>{group.name}</Link>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    );
  }
}

AllGroupsPage.propTypes = {
  groups: PropTypes.array.isRequired,
  teams: PropTypes.array.isRequired
};

function mapStateToProps(state, ownProps) {
  return {
    groups: state.groups,
    teams: state.teams
  };
}

function mapDispatchToProps(dispatch) {
  return {
    groupActions: bindActionCreators(groupActions, dispatch),
    teamActions: bindActionCreators(teamActions, dispatch)
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(AllGroupsPage);
