import { IAuthor } from 'src/app/_shared/model/author.model';

export interface IAuthorState{
    authors: IAuthor[];
    selectedAuthor: IAuthor;
}

export const initialAuthorState: IAuthorState = {
    authors: null,
    selectedAuthor: null
}