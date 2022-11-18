import { Injectable } from "@angular/core";
import { Effect, ofType, Actions } from '@ngrx/effects';
import { withLatestFrom, switchMap, map, catchError, tap } from 'rxjs/operators';
import { select, Store } from '@ngrx/store';
import { ApiUserService } from 'src/app/_shared/service/api.user.service';
import { IAppState } from '../state/app.state';
import { ApiUserListResponse, ApiUserResponse } from 'src/app/_shared/model/api.user.response';
import { of } from 'rxjs';
import { selectUserList } from '../selectors/user.selector';
import { IUser } from 'src/app/_shared/model/user.model';
import { Get, ActionsEnum, GetSuccess, GetAll, GetAllSuccess, Create, Success, Fail, Delete, Update, EntitiesEnum } from '../actions/app.actions';
import { selectLoggedUser } from "../selectors/login.selector";

@Injectable()
export class UserEffects{
    
    constructor(
        private _userService: ApiUserService,
        private _actions$: Actions,
        private _store: Store<IAppState<IUser>>) {}

    @Effect()
    getUser$ = this._actions$.pipe(
        ofType<Get>(`${EntitiesEnum.User}_${ActionsEnum.Get}`),
        map(action => action.payload),
        withLatestFrom(this._store.pipe(select(selectUserList))),
        switchMap(
            ([id, users]) => {
                const selectedUser = users.filter(user => user.id === +id)[0];
                return of(new GetSuccess(selectedUser, EntitiesEnum.User));
            })
    );

    @Effect()
    getUsers$ = this._actions$.pipe(
        ofType<GetAll>(`${EntitiesEnum.User}_${ActionsEnum.GetAll}`),
        withLatestFrom(this._store.pipe(select(selectLoggedUser))),
        switchMap(
            ([, loggedUser]) => 
            this._userService
                .getUsers(loggedUser.token)
                .pipe(
                        switchMap((response : ApiUserListResponse) => of(new GetAllSuccess(response.value, EntitiesEnum.User)))
                )
        )
    );

    @Effect()
    createUser$ = this._actions$.pipe(
        ofType<Create>(`${EntitiesEnum.User}_${ActionsEnum.Create}`),
        map(action => action.payload),
        withLatestFrom(this._store.pipe(select(selectLoggedUser))),
        switchMap(
            ([user,userLogged]) => 
            this._userService
                .createUser(user, userLogged.token)
                .pipe(
                        switchMap((response : ApiUserResponse) => of(new Success(response.value, EntitiesEnum.User))),
                        catchError(err => of(new Fail(err, EntitiesEnum.User)))
                ),
        ),
        switchMap(() => [
                new GetAll(EntitiesEnum.User),
            ])
    );

    @Effect()
    updateUser$ = this._actions$.pipe(
        ofType<Update>(`${EntitiesEnum.User}_${ActionsEnum.Update}`),
        map(action => action.payload),
        withLatestFrom(this._store.pipe(select(selectLoggedUser))),
        switchMap(
            ([user,userLogged]) => 
            this._userService
                .updateUser(user, userLogged.token)
                .pipe(
                        switchMap((response : ApiUserResponse) => of(new Success(response.value, EntitiesEnum.User))),
                        catchError(err => of(new Fail(err, EntitiesEnum.User)))
                ),
        ),
        switchMap(() => [
                new GetAll(EntitiesEnum.User),
            ])
    );

    @Effect()
    deleteUser$ = this._actions$.pipe(
        ofType<Delete>(`${EntitiesEnum.User}_${ActionsEnum.Delete}`),
        map(action => action.payload),
        withLatestFrom(this._store.pipe(select(selectLoggedUser))),
        switchMap(
            ([user,userLogged]) => 
            this._userService.deleteUser(user.id, userLogged.token).pipe(
                switchMap((response : ApiUserResponse) => of(new Success(response.value, EntitiesEnum.User))),
                catchError(err => of(new Fail(err, EntitiesEnum.User)))
        ))
    );

    
}