import { IAppState, initialAppState } from "../state/app.state";
import { createSelector, createFeatureSelector } from '@ngrx/store';
import { appReducer } from '../reducers/app.reducer';
import { IUser } from 'src/app/_shared/model/user.model';
import { EntitiesEnum } from '../actions/app.actions';
import { AuthorActionTypes } from '../actions/login.actions';

const getLoginFeatureState = createFeatureSelector<IAppState<IUser>>('loginFeature');

export const loginReducer = appReducer<IAppState<IUser>>(EntitiesEnum.Login, initialAppState, AuthorActionTypes);

export const selectLoggedUser = createSelector(
    getLoginFeatureState,
    (state: IAppState<IUser>) => state != null ? state.selectedEntity : null
);