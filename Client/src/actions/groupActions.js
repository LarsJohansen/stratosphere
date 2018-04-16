import * as types from "./actionTypes";
import groupApi from "../api/mockGroupApi";

export function loadGroupsSuccess(groups) {
  return { type: types.LOAD_GROUPS_SUCCESS, groups };
}

export function loadGroups() {
  return function(dispatch) {
    return groupApi
      .getAllGroups()
      .then(groups => {
        dispatch(loadGroupsSuccess(groups));
      })
      .catch(error => {
        throw error;
      });
  };
}
