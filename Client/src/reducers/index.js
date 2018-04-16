import { combineReducers } from 'redux';
import groups from "./groupReducer";
import teams from "./teamReducer";

const rootReducer = combineReducers({
  groups,
  teams
});


export default rootReducer;
