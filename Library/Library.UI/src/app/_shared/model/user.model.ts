import { IEntity } from './entity.model';

export interface IUser extends IEntity {
    login: string;
    password: string;
    token: string;
    message?: string;
  }