import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { IAppState } from '../store/state/app.state';
import { IUser } from '../_shared/model/user.model';
import { selectLoggedUser } from '../store/selectors/login.selector';
import { map } from 'rxjs/operators';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router, private store: Store<IAppState<IUser>>) { }

    loggedUser$ = this.store.pipe(select(selectLoggedUser));

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        return this.loggedUser$.pipe(
            map((data) => {
                if(data != null)
                    return true;
             
                // not logged in so redirect to login page with the return url
                this.router.navigate(['login'], { queryParams: { returnUrl: state.url }});
                return false;
            })
        )
    }
}