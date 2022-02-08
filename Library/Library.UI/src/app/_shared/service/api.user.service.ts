import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs/index";
import { ApiUserResponse, ApiUserListResponse } from "../model/api.user.response";
import { IUser } from '../model/user.model';
import { ApiResponse } from '../model/api.response';
import { map } from 'rxjs/internal/operators/map';
import { environment } from 'src/environments/environment';
import { IEntity } from '../model/entity.model';

@Injectable({
  providedIn: 'root'
})
export class ApiUserService {

  constructor(private http: HttpClient) { }
  private baseUrl = `${environment.apiUrl}/users`;

  getUsers(token: string) : Observable<ApiUserListResponse> {
    return this.http.get<ApiUserListResponse>(`${this.baseUrl}/withroles`, {
      headers: {
        "Authorization": token
      }
    });
  }

  getUserById(id: string): Observable<ApiUserResponse> {
    return this.http.get<ApiUserResponse>(this.baseUrl + id)
  }

  createUser(user: IEntity, token: string): Observable<ApiUserResponse> {
    return this.http.post<ApiUserResponse>(this.baseUrl, user, {
      headers: {
        "Authorization": token
      }
    });
  }

  updateUser(user: IEntity, token: string): Observable<ApiResponse> {
    return this.http.put<ApiResponse>(this.baseUrl, user, {
      headers: {
        "Authorization": token
      }
    });
  }

  deleteUser(id: number): Observable<ApiResponse> {
    return this.http.delete<ApiResponse>(this.baseUrl + id);
  }

}
