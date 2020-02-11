import { Author } from './author.model';
import { ApiResponse } from './api.response';

export class ApiAuthorResponse implements ApiResponse {
  statusCode: number;
  value?: Author;
  message: string;
}

class Authors {
  authors: Author[]
}

export class ApiAuthorListResponse implements ApiResponse {
  statusCode: number;
  value?: Authors;
  message? : string;
}