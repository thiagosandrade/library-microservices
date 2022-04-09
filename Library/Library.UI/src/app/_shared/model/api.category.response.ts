import { ApiResponse } from './api.response';
import { ICategory } from './category.model';

export class ApiCategoryResponse implements ApiResponse {
  statusCode: number;
  value?: ICategory;
  message: string;
}

export class ApiCategoryListResponse implements ApiResponse {
  statusCode: number;
  value?: ICategory[];
  message? : string;
}