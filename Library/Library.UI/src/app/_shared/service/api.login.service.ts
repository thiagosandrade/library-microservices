import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable} from "rxjs/index";
import {ApiLoginResponse} from "../model/api.login.response";
import { User } from '../model/user.model';

@Injectable({
  providedIn: 'root'
})
export class ApiLoginService {

  constructor(private http: HttpClient) { }
  baseUrl: string = 'http://localhost:59580/auth/';

  login(loginPayload : User) : Observable<ApiLoginResponse> {
    return this.http.post<ApiLoginResponse>(this.baseUrl, loginPayload);
  }

}
