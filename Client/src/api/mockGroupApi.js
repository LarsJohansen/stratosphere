import delay from "./delay";

// This file mocks a web API by working with the hard-coded data below.
// It uses setTimeout to simulate the delay of an AJAX call.
// All calls return promises.

const groups = [
  {
    id: 1,
    name: "A"
  },
  {
    id: 2,
    name: "B"
  },
  {
    id: 3,
    name: "C"
  },
  {
    id: 4,
    name: "D"
  },
  {
    id: 5,
    name: "E"
  },
  {
    id: 6,
    name: "F"
  },
  {
    id: 7,
    name: "G"
  },
  {
    id: 8,
    name: "H"
  },
];

class GroupApi {
  static getAllGroups() {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve(Object.assign([], groups));
      }, delay);
    });
  }
}

export default GroupApi;
