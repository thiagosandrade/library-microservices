import { ApiResponse } from './api.response';
import { ICart } from './cart.model';

export class ApiCartResponse implements ApiResponse {
  statusCode: number;
  value?: ICart;
  message: string;
}
