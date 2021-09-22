export class LoginModel {
    userId: number = -1;
    username: string = "";
    password: string = "";
    token: string = "";

    receiveAnswerFromServer = false;
    rememberMe = false;

    constructor() { }
}
