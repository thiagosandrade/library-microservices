import { IAppState, initialAppState } from "../state/app.state";
import { createSelector, createFeatureSelector } from '@ngrx/store';
import { IAuthor } from 'src/app/_shared/model/author.model';
import { appReducer } from '../reducers/app.reducer';
import { Actions } from '@ngrx/effects';
import { EntitiesEnum } from '../actions/app.actions';

const getAuthorFeatureState = createFeatureSelector<IAppState<IAuthor>>('authorsFeature');

export const authorReducer = appReducer<IAppState<IAuthor>>(EntitiesEnum.Author, initialAppState, Actions);

export const selectAuthorList = createSelector(
    getAuthorFeatureState,
    (state: IAppState<IAuthor>) => state != null ? state.entities : null
);

export const selectSelectedUser = createSelector(
    getAuthorFeatureState,
    (state: IAppState<IAuthor>) => state.selectedEntity
);