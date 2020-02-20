import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs/index";
import { ApiLoginResponse } from "../model/api.login.response";
import { User } from '../model/user.model';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ApiLoginService {

  constructor(private http: HttpClient, private router: Router) { }
  private baseUrl = `${environment.apiUrl}/auth/`;

  login(loginPayload : User) : Observable<ApiLoginResponse> {
    return this.http.post<ApiLoginResponse>(this.baseUrl, loginPayload).pipe(
      map(response => {
        if(response.token != null)
        {
          localStorage.setItem('token', JSON.stringify(response.token));
        }
        return response;
      })
    );
  }

  public logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('token');
    this.router.navigate(['']);
  }

  public isLogged() {
    if(localStorage.getItem('token')){
      return true;
    }

    return false;
  }

  get isLoggedIn(): boolean {
    return !!this.isLogged();
  }
}
