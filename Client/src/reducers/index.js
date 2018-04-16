import { combineReducers } from 'redux';
import groups from "./groupReducer";

const rootReducer = combineReducers({
  groups
});


export default rootReducer;
