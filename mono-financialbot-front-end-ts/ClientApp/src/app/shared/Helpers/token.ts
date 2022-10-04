import { TOKEN } from "../constants/token";


  export function getToken() {
    return localStorage.getItem(TOKEN);
  }

