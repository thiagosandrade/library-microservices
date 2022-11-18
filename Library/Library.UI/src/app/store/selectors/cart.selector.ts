import { IAppState, initialAppState } from "../state/app.state";
import { createSelector, createFeatureSelector } from '@ngrx/store';
import { appReducer } from '../reducers/app.reducer';
import { Actions } from '@ngrx/effects';
import { EntitiesEnum } from '../actions/app.actions';
import { ICart } from "src/app/_shared/model/cart.model";

const getCartFeatureState = createFeatureSelector<IAppState<ICart>>('cartsFeature');

export const cartReducer = appReducer<IAppState<ICart>>(EntitiesEnum.Cart, initialAppState, Actions);

export const getSelectedCart = createSelector(
    getCartFeatureState,
    (state: IAppState<ICart>) => state.selectedEntity
);