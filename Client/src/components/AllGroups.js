import React from "react";
import { connect } from "react-redux";
import GroupApi from "../api/mockGroupApi";
import { Link } from "react-router-dom";
import { Table } from "react-bootstrap";
import { PropTypes } from "prop-types";
import { bindActionCreators } from "redux";
import * as groupActions from "../actions/groupActions";

class AllGroupsPage extends React.Component {
  render() {
    const { groups } = this.props;
    return (
      <Table striped bordered condensed hover>
        <thead>
          <tr>
            <th>Group</th>
          </tr>
        </thead>
        <tbody>
          {groups.map(group => (
            <tr>
            <td key={group.id}>
              <Link to={"/groups/" + group.name}>{group.name}</Link>
            </td>
            </tr>
          ))}
        </tbody>
      </Table>
    );
  }
}

AllGroupsPage.propTypes = {
  groups: PropTypes.array.isRequired
};

function mapStateToProps(state, ownProps) {
  return {
    groups: state.groups
  }
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(groupActions, dispatch)
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(AllGroupsPage);
