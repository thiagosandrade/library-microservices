import { ActionReducerMap } from '@ngrx/store';
import { IAppState } from '../state/app.state';
import { authorReducers } from './author.reducer';

export const appReducers: ActionReducerMap<IAppState, any> = {
    authors: authorReducers
}