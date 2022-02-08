import { IUser } from './user.model';
import { ApiResponse } from './api.response';

export class ApiUserResponse implements ApiResponse {
  statusCode: number;
  value?: IUser;
  message: string;
}

export class ApiUserListResponse implements ApiResponse {
  statusCode: number;
  value?: IUser[];
  message? : string;
}