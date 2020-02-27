import { IUser } from './user.model';

export class ApiLoginResponse implements IUser {
    login: string;  password: string;
    token: string;
    message?: string;
    id: number;
  }