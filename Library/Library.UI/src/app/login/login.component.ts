import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ApiLoginService } from "../_shared/service/api.login.service";
import { IUser } from '../_shared/model/user.model';
import { Store, select } from '@ngrx/store';
import { IAppState } from '../store/state/app.state';
import { Login, ActionsEnum, EntitiesEnum } from '../store/actions/app.actions';
import { ofType, Actions } from '@ngrx/effects';
import { Router } from '@angular/router';
import { selectLoggedUser } from '../store/selectors/user.selector';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  invalidLogin: boolean = false;
  constructor(private formBuilder: FormBuilder, private apiService: ApiLoginService, private store: Store<IAppState<IUser>>, private router: Router,
    private _actions$: Actions) { }

  loggedUser$ = this.store.pipe(select(selectLoggedUser));


  onSubmit() {
    if (this.loginForm.invalid) {
      return;
    }
    
    let loginPayload : IUser = {
      id: null,
      token: "",
      login: this.loginForm.controls.username.value,
      password: this.loginForm.controls.password.value
    }

    this._actions$.pipe(
      ofType<Login>(`Login_${ActionsEnum.Login}`)).subscribe(() => {
        if(!this.apiService.isLogged())
        {
          this.invalidLogin = true;
        }
        else
        {
          this.router.navigate(['author','list-author']);
        }
      });

      this.store.dispatch(new Login(loginPayload, EntitiesEnum.Login));
    
  }

  ngOnInit() {
    this.apiService.logout();
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.compose([Validators.required])],
      password: ['', Validators.required]
    });
  }
}
