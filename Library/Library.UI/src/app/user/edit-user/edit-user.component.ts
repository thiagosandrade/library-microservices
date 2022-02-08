import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Store, select } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { getSelectedUser } from 'src/app/store/selectors/user.selector';
import { Update, EntitiesEnum } from 'src/app/store/actions/app.actions';
import { IUser } from 'src/app/_shared/model/user.model';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.scss']
})
export class EditUserComponent implements OnInit {

  editForm: FormGroup;
  user: IUser;

  constructor(private formBuilder: FormBuilder,private router: Router, private store: Store<IAppState<IUser>>) { }

  user$ = this.store.pipe(select(getSelectedUser))
    .subscribe( (user : IUser) =>{
      if(user == null){
        this.router.navigate(['user','list-user']);
        return;
      }
      this.user = user;
    }
  );

  ngOnInit() {
    
    this.editForm = this.formBuilder.group({
      id: [''],
      name: ['', Validators.required],
      surname: ['', Validators.required],
      login: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      userRoles: ['', Validators.nullValidator]
    });

    if(this.user != null)
      this.editForm.setValue(this.user);
  }

  onSubmit() {
    this.store.dispatch(new Update(this.editForm.value, EntitiesEnum.User));
    this.router.navigate(['user','list-user'])
  }
}
