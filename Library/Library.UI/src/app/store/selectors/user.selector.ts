import { IAppState, initialAppState } from "../state/app.state";
import { createSelector, createFeatureSelector } from '@ngrx/store';
import { appReducer } from '../reducers/app.reducer';
import { IUser } from 'src/app/_shared/model/user.model';
import { EntitiesEnum } from '../actions/app.actions';
import { Actions } from "@ngrx/effects";

const getUserFeatureState = createFeatureSelector<IAppState<IUser>>('usersFeature');

export const userReducer = appReducer<IAppState<IUser>>(EntitiesEnum.User, initialAppState, Actions);

export const selectUserList = createSelector(
    getUserFeatureState,
    (state: IAppState<IUser>) => state != null ? state.entities : null
);
