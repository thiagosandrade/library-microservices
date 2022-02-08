import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { IUser } from '../_shared/model/user.model';
import { Store, select } from '@ngrx/store';
import { IAppState } from '../store/state/app.state';
import { EntitiesEnum } from '../store/actions/app.actions';
import { ofType, Actions } from '@ngrx/effects';
import { Router } from '@angular/router';
import { selectLoggedUser } from '../store/selectors/login.selector';
import { AuthorActionTypes, Login, Logout } from '../store/actions/login.actions';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  invalidLogin: boolean = false;
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

    this._actions$.pipe(
      ofType<Login>(`${EntitiesEnum.Login}_${AuthorActionTypes.Login}`))
      .subscribe(() => {
        	this.loggedUser$.subscribe(logged => {
            if(!logged)
            {
              this.invalidLogin = true;
            }
            else
            {
              this.router.navigate(['user','list-user']);
            }
          })
      });

      this.store.dispatch(new Login(loginPayload, EntitiesEnum.Login));
    
  }

  ngOnInit() {
    this.store.dispatch(new Logout(null, EntitiesEnum.Login));
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.compose([Validators.required])],
      password: ['', Validators.required]
    });
  }
}
