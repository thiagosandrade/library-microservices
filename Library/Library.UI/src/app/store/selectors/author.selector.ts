import { IAppState } from "../state/app.state";
import { createSelector } from '@ngrx/store';
import { IAuthorState } from '../state/author.state';

const selectAuthors = (state: IAppState) => state.authors;

export const selectAuthorList = createSelector(
    selectAuthors,
    (state: IAuthorState) => state.authors
);

export const selectSelectedUser = createSelector(
    selectAuthors,
    (state: IAuthorState) => state.selectedAuthor
);