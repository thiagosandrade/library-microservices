import { IBook } from './book.model';
import { ApiResponse } from './api.response';

export class ApiBookResponse implements ApiResponse {
  statusCode: number;
  value?: IBook;
  message: string;
}

export class ApiBookListResponse implements ApiResponse {
  statusCode: number;
  value?: IBook[];
  message? : string;
}