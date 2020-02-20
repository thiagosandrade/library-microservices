import { IAuthorState, initialAuthorState } from './author.state';

export interface IAppState {
    authors: IAuthorState
}

export const initialAppState: IAppState = {
    authors: initialAuthorState
}

export function getInitialState(): IAppState {
    return initialAppState;
}