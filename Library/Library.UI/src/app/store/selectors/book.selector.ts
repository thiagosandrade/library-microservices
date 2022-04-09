import { IAppState, initialAppState } from "../state/app.state";
import { createSelector, createFeatureSelector } from '@ngrx/store';
import { IBook } from 'src/app/_shared/model/book.model';
import { appReducer } from '../reducers/app.reducer';
import { Actions } from '@ngrx/effects';
import { EntitiesEnum } from '../actions/app.actions';

const getBookFeatureState = createFeatureSelector<IAppState<IBook>>('booksFeature');

export const bookReducer = appReducer<IAppState<IBook>>(EntitiesEnum.Book, initialAppState, Actions);

export const selectBookList = createSelector(
    getBookFeatureState,
    (state: IAppState<IBook>) => state != null ? state.entities : null
);

export const getSelectedBook = createSelector(
    getBookFeatureState,
    (state: IAppState<IBook>) => state.selectedEntity
);