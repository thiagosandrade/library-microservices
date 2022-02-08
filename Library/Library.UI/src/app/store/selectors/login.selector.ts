import { IAppState, initialAppState } from "../state/app.state";
import { createSelector, createFeatureSelector } from '@ngrx/store';
import { appReducer } from '../reducers/app.reducer';
import { IUser } from 'src/app/_shared/model/user.model';
import { EntitiesEnum } from '../actions/app.actions';
import { AuthorActionTypes } from '../actions/login.actions';
import { IUserRole } from "src/app/_shared/model/userRoles.model";

const getLoginFeatureState = createFeatureSelector<IAppState<IUser>>('loginFeature');

export const loginReducer = appReducer<IAppState<IUser>>(EntitiesEnum.Login, initialAppState, AuthorActionTypes);

export const selectLoggedUser = createSelector(
    getLoginFeatureState,
    (state: IAppState<IUser>) => state != null ? state.selectedEntity : null
);

export const isUserLogged = createSelector(
    getLoginFeatureState,
    (state: IAppState<IUser>) => state != null && state.selectedEntity != null ? true : false
)

export const isUserLoggedAdmin = createSelector(
    getLoginFeatureState,
    (state: IAppState<IUser>) => state != null 
            && state.selectedEntity != null 
            && state.selectedEntity.userRoles != null
            && state.selectedEntity.userRoles.find((x : IUserRole) => x.userRole == "SuperUser") != undefined 
            ? true : false)