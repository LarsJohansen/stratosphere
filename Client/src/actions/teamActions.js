import * as types from "./actionTypes";
import teamApi from "../api/mockTeamApi";

export function loadTeamsSuccess(teams) {
  return { type: types.LOAD_TEAMS_SUCCESS, teams };
}

export function loadTeams() {
  return function(dispatch) {
    return teamApi
      .getAllTeams()
      .then(teams => {
        dispatch(loadTeamsSuccess(teams));
      })
      .catch(error => {
        throw error;
      });
  };
}
