import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs/index";
import { ApiResponse } from '../model/api.response';
import { map } from 'rxjs/internal/operators/map';
import { environment } from 'src/environments/environment';
import { ApiCartResponse } from '../model/api.cart.response';

@Injectable({
  providedIn: 'root'
})
export class ApiCartService {

  constructor(private http: HttpClient) { }
  private baseUrl = `${environment.apiUrl}/cart/`;
  private cart = `product`

  getCartById(id: number, token: string): Observable<ApiCartResponse> {
    return this.http.get<ApiCartResponse>(`${this.baseUrl}${id}`, {
      headers: {
        "Authorization": token
      }
    })
    .pipe(
      map( (response : ApiCartResponse) => {
            return response;
      })
    );
  }

  addItemToCart(cartItem: any, token: string): Observable<ApiCartResponse> {
    return this.http.post<ApiCartResponse>(`${this.baseUrl}${this.cart}/AddItem`, cartItem, {
      headers: {
        "Authorization": token
      }
    });
  }

  removeItemFromCart(cartItem: any, token: string): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(`${this.baseUrl}${this.cart}/RemoveItem`, cartItem, {
      headers: {
        "Authorization": token
      }
    });
  }

}
