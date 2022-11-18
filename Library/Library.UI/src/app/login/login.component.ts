import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { IUser } from '../_shared/model/user.model';
import { Store, select } from '@ngrx/store';
import { IAppState } from '../store/state/app.state';
import { ActionsEnum, EntitiesEnum, Fail } from '../store/actions/app.actions';
import { ofType, Actions } from '@ngrx/effects';
import { Router } from '@angular/router';
import { selectLoggedUser } from '../store/selectors/login.selector';
import { Login, Logout } from '../store/actions/login.actions';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  invalidLogin: boolean = false;
  errorMessage: string = '';

  constructor(private formBuilder: FormBuilder, private store: Store<IAppState<IUser>>, private router: Router,
    private _actions$: Actions) { }

  loggedUser$ = this.store.pipe(select(selectLoggedUser));

  onSubmit() {
    if (this.loginForm.invalid) {
      return;
    }
    let loginPayload : IUser = {
      email: "",
      name: "",
      id: null,
      token: "",
      login: this.loginForm.controls.username.value,
      password: this.loginForm.controls.password.value,
      userRoles: null
    }

    this.store.dispatch(new Login(loginPayload, EntitiesEnum.Login));
    
  }

  ngOnInit() {
    this.store.dispatch(new Logout(null, EntitiesEnum.Login));
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.compose([Validators.required])],
      password: ['', Validators.required]
    });

    this._actions$.pipe(
      ofType<Fail>(`${EntitiesEnum.Login}_${ActionsEnum.Fail}`))
      .subscribe((data: any) => {
        	this.invalidLogin = true;
          this.errorMessage = data.payload
      });

    this.loggedUser$.subscribe(logged => {
      if(logged)
      {
        this.invalidLogin = false;
        this.router.navigate(['user','list-user']);
      }
    })
  }
}
