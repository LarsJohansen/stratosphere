import delay from "./delay";

// This file mocks a web API by working with the hard-coded data below.
// It uses setTimeout to simulate the delay of an AJAX call.
// All calls return promises.

const teams = [
  {
    id: "RUS",
    name: "Russia",
    group: "A"
  },
  {
    id: "SAU",
    name: "Saudi Arabia",
    group: "A"
  },
  {
    id: "EGY",
    name: "Egypt",
    group: "A"
  },
  {
    id: "URG",
    name: "Uruguay",
    group: "A"
  },

  {
    id: "POR",
    name: "Portugal",
    group: "B"
  },
  {
    id: "ESP",
    name: "Spain",
    group: "B"
  },
  {
    id: "MOR",
    name: "Morrocco",
    group: "B"
  },
  {
    id: "IRN",
    name: "Iran",
    group: "B"
  },

  {
    id: "FRA",
    name: "France",
    group: "C"
  },
  {
    id: "AUS",
    name: "Australia",
    group: "C"
  },
  {
    id: "PER",
    name: "Peru",
    group: "C"
  },
  {
    id: "DEN",
    name: "Denmark",
    group: "C"
  },

  {
    id: "ARG",
    name: "Argentina",
    group: "D"
  },
  {
    id: "ISL",
    name: "Iceland",
    group: "D"
  },
  {
    id: "CRO",
    name: "Croatia",
    group: "D"
  },
  {
    id: "NIG",
    name: "Nigeria",
    group: "D"
  },

  {
    id: "BRA",
    name: "Brazil",
    group: "E"
  },
  {
    id: "SWI",
    name: "Switzerland",
    group: "E"
  },
  {
    id: "COS",
    name: "Costa Rica",
    group: "E"
  },
  {
    id: "SRB",
    name: "Serbia",
    group: "E"
  },

  {
    id: "GER",
    name: "Germany",
    group: "F"
  },
  {
    id: "MEX",
    name: "Mexico",
    group: "F"
  },
  {
    id: "SWE",
    name: "Sweden",
    group: "F"
  },
  {
    id: "KOR",
    name: "Korea Republic",
    group: "F"
  },

  {
    id: "BLG",
    name: "Belgium",
    group: "G"
  },
  {
    id: "PAN",
    name: "Panama",
    group: "G"
  },
  {
    id: "TUN",
    name: "Tunisia",
    group: "G"
  },
  {
    id: "ENG",
    name: "England",
    group: "G"
  },

  {
    id: "POL",
    name: "Poland",
    group: "H"
  },
  {
    id: "SEN",
    name: "Senegal",
    group: "H"
  },
  {
    id: "COL",
    name: "Colombia",
    group: "H"
  },
  {
    id: "JAP",
    name: "Japan",
    group: "H"
  },

];

class TeamApi {
  static getAllTeams() {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve(Object.assign([], teams));
      }, delay);
    });
  }
}

export default TeamApi;
