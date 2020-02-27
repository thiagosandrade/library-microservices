import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs/index";
import { ApiLoginResponse } from "../model/api.login.response";
import { IUser } from '../model/user.model';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { IAppState } from 'src/app/store/state/app.state';
import { Store, select } from '@ngrx/store';
import { ClearSelected, EntitiesEnum } from 'src/app/store/actions/app.actions';
import { selectLoggedUser } from 'src/app/store/selectors/user.selector';

@Injectable({
  providedIn: 'root'
})
export class ApiLoginService {

  constructor(private http: HttpClient, private router: Router, private store: Store<IAppState<IUser>>) { }
  private baseUrl = `${environment.apiUrl}/auth/`;

  private islogged = this.store.pipe(select(selectLoggedUser));

  login(loginPayload : IUser) : Observable<ApiLoginResponse> {
    return this.http.post<ApiLoginResponse>(this.baseUrl, loginPayload).pipe(
      map(response => {
        loginPayload.token = response.token;
        return loginPayload;
      })
    );
  }

  public logout() {
    this.store.dispatch(new ClearSelected(EntitiesEnum.Login));
    this.router.navigate(['']);
  }

  public isLogged() {
    return this.islogged;
  }
}
