import { Action } from '@ngrx/store';
import { IAuthor } from 'src/app/_shared/model/author.model';

export enum AuthorActionsEnum {
    GetAuthors = '[Author] Get Authors',
    GetAuthorsSuccess = '[Author] Get Authors Success',
    GetAuthor = '[Author] Get Author Success',
    GetAuthorSuccess = '[Author] Get Author Success',
    SetSelectedAuthor = '[Author] Set Selected Author',
    UpdateAuthor = "[Author] Update Author",
    Success = "[Author] Success",
    Fail = "[Author] Fail",
    DeleteAuthor = "[Author] Delete Author",
    CreateAuthor = "[Author] Create Author"
}

export class GetAuthors implements Action {
    public readonly type = AuthorActionsEnum.GetAuthors;
}

export class GetAuthorsSuccess implements Action {
    public readonly type = AuthorActionsEnum.GetAuthorsSuccess;
    constructor(public payload: IAuthor[]){}
}

export class GetAuthor implements Action {
    public readonly type = AuthorActionsEnum.GetAuthor;
    constructor(public payload: IAuthor){}
}

export class GetAuthorSuccess implements Action {
    public readonly type = AuthorActionsEnum.GetAuthorSuccess;
    constructor(public payload: IAuthor){}
}

export class SetSelectedAuthor implements Action {
    public readonly type = AuthorActionsEnum.SetSelectedAuthor;
    constructor(public payload: IAuthor){}
}

export class CreateAuthor implements Action {
    public readonly type = AuthorActionsEnum.CreateAuthor;
    constructor(public payload: IAuthor){}
}

export class UpdateAuthor implements Action {
    public readonly type = AuthorActionsEnum.UpdateAuthor;
    constructor(public payload: IAuthor){}
}

export class DeleteAuthor implements Action {
    public readonly type = AuthorActionsEnum.DeleteAuthor;
    constructor(public payload: IAuthor){}
}

export class Success implements Action {
    public readonly type = AuthorActionsEnum.Success;
    constructor(public payload: any){}
}

export class Fail implements Action {
    public readonly type = AuthorActionsEnum.Fail;
    constructor(public payload: any){}
}

export type AuthorActions = GetAuthors | GetAuthorsSuccess | GetAuthor | GetAuthorSuccess | 
    SetSelectedAuthor | UpdateAuthor | DeleteAuthor | CreateAuthor |
    Success | Fail;