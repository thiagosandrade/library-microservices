import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable} from "rxjs/index";
import {ApiLoginResponse} from "../model/api.login.response";
import { User } from '../model/user.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiLoginService {

  constructor(private http: HttpClient) { }
  private baseUrl = `${environment.apiUrl}/auth/`;

  login(loginPayload : User) : Observable<ApiLoginResponse> {
    return this.http.post<ApiLoginResponse>(this.baseUrl, loginPayload);
  }

}
