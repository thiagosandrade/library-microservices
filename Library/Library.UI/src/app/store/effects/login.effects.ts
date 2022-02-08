import { Injectable } from "@angular/core";
import { Effect, ofType, Actions } from '@ngrx/effects';
import { withLatestFrom, switchMap, map, catchError } from 'rxjs/operators';
import { select, Store } from '@ngrx/store';
import { IAppState } from '../state/app.state';
import { of } from 'rxjs';
import { Get, ActionsEnum, GetSuccess, Success, Fail, SetSelected, EntitiesEnum, GetAll } from '../actions/app.actions';
import { ApiLoginService } from 'src/app/_shared/service/api.login.service';
import { IUser } from 'src/app/_shared/model/user.model';
import { ApiLoginResponse } from 'src/app/_shared/model/api.login.response';
import { selectLoggedUser } from '../selectors/login.selector';
import { Login, AuthorActionTypes } from '../actions/login.actions';

@Injectable()
export class LoginEffects{
    
    constructor(
        private _loginService: ApiLoginService,
        private _actions$: Actions,
        private _store: Store<IAppState<IUser>>) {}

    @Effect()
    getUser$ = this._actions$.pipe(
        ofType<Get>(`${EntitiesEnum.Login}_${ActionsEnum.Get}`),
        map(action => action.payload),
        withLatestFrom(this._store.pipe(select(selectLoggedUser))),
        switchMap(([, user]) => this._loginService.loginDetails(user.token).pipe(
            switchMap((response : ApiLoginResponse) => [
                new GetSuccess(response.value, EntitiesEnum.Login)
            ])))
    );

    @Effect()
    loginUser$ = this._actions$.pipe(
        ofType<Login>(`${EntitiesEnum.Login}_${AuthorActionTypes.Login}`),
        map(action => action.payload),
        switchMap((user : IUser) => this._loginService.login(user).pipe(
            switchMap((response : ApiLoginResponse) => [
                new SetSelected(response.value, EntitiesEnum.Login),
                new Success(user, EntitiesEnum.Login),
                new Get(user, EntitiesEnum.Login)
            ])
        )),
        catchError(err => of(new Fail(err, EntitiesEnum.Login)))
    );

    @Effect()
    logoutUser$ = this._actions$.pipe(
        ofType<Login>(`${EntitiesEnum.Login}_${AuthorActionTypes.Logout}`),
        map(action => action.payload),
        switchMap(() =>  [
                new SetSelected(null, EntitiesEnum.Login),
                new Success(null, EntitiesEnum.Login)
        ]),
        catchError(err => of(new Fail(err, EntitiesEnum.Login)))
    );
}