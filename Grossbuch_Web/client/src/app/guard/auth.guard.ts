import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import { UserTokenStorage } from '../classes/user-token-storage';

@Injectable()

export class AuthGuard implements CanActivate {

    constructor(private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if(UserTokenStorage.getInstanse().getUserToken()) {
            return true;
        } else {
            this.router.navigate(['/login'], { queryParams: { returnUrl: state.url} });
            return false;
        }
    }
}