import http from "./httpService";
import {JwtUser} from "../appTypes";

const tokenKey = "token";
const currentUser: JwtUser = {
    Sub: "pustik@domain.com",
    NameIdentifier: "def",
    Role: "Student",
    Jti: "string"
}

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
    /*try {
        const jwt = localStorage.getItem(tokenKey) as string;
        return jwtDecode(jwt);
    } catch (ex) {
        return null;
    }*/
    return currentUser;
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
