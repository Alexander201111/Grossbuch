export class UserTokenStorage {
    private static readonly USER_TOKEN_KEY = 'USER_TOKEN';
    private static instanse: UserTokenStorage = new UserTokenStorage();

    private constructor() {
        if(UserTokenStorage.instanse) {
            throw new Error("Error: Instantiation failed: Use UserTokenStorage.getInstanse() instead of new.");
        }
        UserTokenStorage.instanse = this;
    }

    public static getInstanse(): UserTokenStorage {
        return UserTokenStorage.instanse;
    }

    public setUserToken(token: string) {
        if(sessionStorage.getItem(UserTokenStorage.USER_TOKEN_KEY) == null) {
            sessionStorage.setItem(UserTokenStorage.USER_TOKEN_KEY, token);
        }
    }

    public getUserToken() {
        return sessionStorage.getItem(UserTokenStorage.USER_TOKEN_KEY);
    }

    public clearStorage() {
        sessionStorage.clear();
    }
}
