import { Injectable } from "@angular/core";
import { Effect, ofType, Actions } from '@ngrx/effects';
import { withLatestFrom, switchMap, map, catchError, mergeMap } from 'rxjs/operators';
import { select, Store } from '@ngrx/store';
import { IAppState } from '../state/app.state';
import { of } from 'rxjs';
import { Get, ActionsEnum, GetSuccess, Create, Success, Fail, Delete, Update, EntitiesEnum } from '../actions/app.actions';
import { selectLoggedUser } from "../selectors/login.selector";
import { ICart } from "src/app/_shared/model/cart.model";
import { ApiCartService } from "src/app/_shared/service/api.cart.service";
import { ApiCartResponse } from "src/app/_shared/model/api.cart.response";

@Injectable()
export class CartEffects{
    
    constructor(
        private _cartService: ApiCartService,
        private _actions$: Actions,
        private _store: Store<IAppState<ICart>>) {}

    @Effect()
    getCart$ = this._actions$.pipe(
        ofType<Get>(`${EntitiesEnum.Cart}_${ActionsEnum.Get}`),
        map(action => action.payload),
        withLatestFrom(this._store.pipe(select(selectLoggedUser))),
        switchMap(
            ([, loggedUser]) => 
            this._cartService
                .getCartById(loggedUser.id,loggedUser.token)
                .pipe(
                    map((response : ApiCartResponse) => new GetSuccess(response.value, EntitiesEnum.Cart))
        ))
    );

    @Effect()
    addItemToCart$ = this._actions$.pipe(
        ofType<Create>(`${EntitiesEnum.Cart}_${ActionsEnum.Create}`),
        map(action => action.payload),
        withLatestFrom(this._store.pipe(select(selectLoggedUser))),
        switchMap(
            ([cartItem, userLogged]) => 
            this._cartService.addItemToCart(cartItem, userLogged.token)
                .pipe(
                    mergeMap((response : ApiCartResponse) => 
                    [
                        new Success(response.value, EntitiesEnum.Cart),
                        new Get(null, EntitiesEnum.Cart),
                    ]),
                    catchError(err => of(new Fail(err, EntitiesEnum.Cart)))
            )
        )
    );

    @Effect()
    removeItemFromCart$ = this._actions$.pipe(
        ofType<Delete>(`${EntitiesEnum.Cart}_${ActionsEnum.Delete}`),
        map(action => action.payload),
        withLatestFrom(this._store.pipe(select(selectLoggedUser))),
        switchMap(
            ([cartItem, userLogged]) => 
            this._cartService.removeItemFromCart(cartItem, userLogged.token)
                .pipe(
                    mergeMap((response : ApiCartResponse) => 
                    [
                        new Success(response.value, EntitiesEnum.Cart),
                        new Get(null, EntitiesEnum.Cart),
                    ]),
                    catchError(err => of(new Fail(err, EntitiesEnum.Cart)))
        ))
    );

    
}