import { IUser } from './user.model';

export class ApiLoginResponse implements ApiLoginResponse {
    statusCode: number;
    value?: IUser;
    message: string;
  }