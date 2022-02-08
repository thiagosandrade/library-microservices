import { EntitiesEnum } from './app.actions';
import { IUser } from 'src/app/_shared/model/user.model';
import { Action } from '@ngrx/store';

export enum AuthorTypes {
    Login = "Login",
    Logout = "Logout"
}

export const AuthorActionTypes = {
    ...EntitiesEnum,
    ...AuthorTypes
};

export class Login implements Action {
    public readonly type = `${this.entityType}_${AuthorActionTypes.Login}`;
    constructor(public payload: IUser, private entityType: string){}
}

export class Logout implements Action {
    public readonly type = `${this.entityType}_${AuthorActionTypes.Logout}`;
    constructor(public payload: IUser, private entityType: string){}
}

const all = AuthorActionTypes
export type AuthorActions = typeof all;