import { Action } from '@ngrx/store';
import { IEntity } from 'src/app/_shared/model/entity.model';

export enum EntitiesEnum {
    Author = 'Author',
    Book = 'Book',
    Login = 'Login',
    User = 'User',
    Cart = 'Cart'
}

export enum ActionsEnum {
    GetAll = 'Get All',
    GetAllSuccess = 'Get All Success',
    Get = 'Get',
    GetSuccess = 'Get Success',
    SetSelected = 'Set Selected',
    Create = "Create",
    Update = "Update",
    Delete = "Delete",
    Success = "Success",
    Fail = "Fail",
    ClearSelected = "ClearSelected"
}

export class GetAll implements Action {
    public readonly type = `${this.entityType}_${ActionsEnum.GetAll}`;
    constructor(private entityType: string){}
}

export class GetAllSuccess implements Action {
    public readonly type = `${this.entityType}_${ActionsEnum.GetAllSuccess}`;
    constructor(public payload: IEntity[], private entityType: string){}
}

export class Get implements Action {
    public readonly type = `${this.entityType}_${ActionsEnum.Get}`;
    constructor(public payload: IEntity, private entityType: string){}
}

export class GetSuccess implements Action {
    public readonly type = `${this.entityType}_${ActionsEnum.GetSuccess}`;
    constructor(public payload: IEntity, private entityType: string){}
}

export class ClearSelected implements Action {
    public readonly type = `${this.entityType}_${ActionsEnum.ClearSelected}`;
    constructor(private entityType: string){}
}

export class SetSelected implements Action {
    public readonly type = `${this.entityType}_${ActionsEnum.SetSelected}`;
    constructor(public payload: IEntity, private entityType: string){}
}

export class Create implements Action {
    public readonly type = `${this.entityType}_${ActionsEnum.Create}`;
    constructor(public payload: IEntity, private entityType: string){}
}

export class Update implements Action {
    public readonly type = `${this.entityType}_${ActionsEnum.Update}`;
    constructor(public payload: IEntity, private entityType: string){}
}

export class Delete implements Action {
    public readonly type = `${this.entityType}_${ActionsEnum.Delete}`;
    constructor(public payload: IEntity, private entityType: string){}
}

export class Success implements Action {
    public readonly type = `${this.entityType}_${ActionsEnum.Success}`;
    constructor(public payload: any, private entityType: string){}
}

export class Fail implements Action {
    public readonly type = `${this.entityType}_${ActionsEnum.Fail}`;
    constructor(public payload: any, private entityType: string){}
}

export type Actions = GetAll | GetAllSuccess | Get | GetSuccess | ClearSelected | SetSelected | 
    Create | Update | Delete | Success | Fail ;