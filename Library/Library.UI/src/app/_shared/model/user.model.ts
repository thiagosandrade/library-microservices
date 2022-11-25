import { IEntity } from './entity.model';
import { IUserRole } from './userRoles.model';

export interface IUser extends IEntity {
    name: string;
    surname: string;
    email: string;
    login: string;
    password: string;
    token: string;
    message?: string;
    userRoles: IUserRole[];
  }