import { initialAuthorState, IAuthorState } from '../state/author.state';
import { AuthorActions, AuthorActionsEnum } from '../actions/author.actions';


export function authorReducers(
    state = initialAuthorState,
    action: AuthorActions
): IAuthorState {
    switch(action.type){
        case AuthorActionsEnum.GetAuthorsSuccess: {
            return {
                ...state,
                authors: action.payload
            };
        }
        case AuthorActionsEnum.GetAuthorSuccess: {
            return {
                ...state,
                selectedAuthor: action.payload
            };
        }
        case AuthorActionsEnum.SetSelectedAuthor: {
            return {
                ...state,
                selectedAuthor: action.payload
            }
        }
        case AuthorActionsEnum.CreateAuthor: {
            state.authors.push(action.payload);

            return {
                ...state,
                authors: state.authors
            }
        }
        case AuthorActionsEnum.UpdateAuthor: {
            const authorsUpdated = state.authors.map(item => action.payload.id === item.id ? action.payload : item)
            return {
                ...state,
                authors: authorsUpdated
            }
        }
        case AuthorActionsEnum.DeleteAuthor: {
            const authorsUpdated = state.authors.filter(item => action.payload.id !== item.id)
            return {
                ...state,
                authors: authorsUpdated
            }
        }

        default:
            return state;
    }
};