import { Author, Authors } from './author.model';
import { ApiResponse } from './api.response';

export class ApiAuthorResponse implements ApiResponse {
  statusCode: number;
  value?: Author;
  message: string;
}

export class ApiAuthorListResponse implements ApiResponse {
  statusCode: number;
  value?: Authors;
  message? : string;
}