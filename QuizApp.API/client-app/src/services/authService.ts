import jwtDecode from "jwt-decode";
import http from "./httpService";

const tokenKey = "token";

http.setJwt(getJwt());

export async function login(email: string, password: string) {
    const {data: jwt} = await http.post("auth", {json: {email, password}}).json();
    localStorage.setItem(tokenKey, jwt);
}

export function loginWithJwt(jwt: string) {
    localStorage.setItem(tokenKey, jwt);
}

export function logout() {
    localStorage.removeItem(tokenKey);
}

export function getCurrentUser() {
    try {
        const jwt = localStorage.getItem(tokenKey) as string;
        return jwtDecode(jwt);
    } catch (ex) {
        return null;
    }
}

export function getJwt() {
    return localStorage.getItem(tokenKey);
}


const auth = {
    login,
    loginWithJwt,
    logout,
    getCurrentUser,
    getJwt
};

export default auth;
