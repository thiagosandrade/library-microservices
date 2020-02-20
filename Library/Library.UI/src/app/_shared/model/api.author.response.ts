import { IAuthor } from './author.model';
import { ApiResponse } from './api.response';

export class ApiAuthorResponse implements ApiResponse {
  statusCode: number;
  value?: IAuthor;
  message: string;
}

export class ApiAuthorListResponse implements ApiResponse {
  statusCode: number;
  value?: IAuthor[];
  message? : string;
}