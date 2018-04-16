import * as types from "../actions/actionTypes";
import initialState from "./initialState";

export default function teamReducer(state = initialState.teams, action) {
  switch (action.type) {
    case types.LOAD_TEAMS_SUCCESS:
      return action.teams;

    default:
      return state;
  }
}
