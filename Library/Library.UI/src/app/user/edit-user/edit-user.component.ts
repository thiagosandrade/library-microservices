import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Store, select } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { getSelectedUser } from 'src/app/store/selectors/user.selector';
import { Update, EntitiesEnum, Create } from 'src/app/store/actions/app.actions';
import { IUser } from 'src/app/_shared/model/user.model';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.scss']
})
export class EditUserComponent implements OnInit {

  form: FormGroup;
  user: IUser;
  isEdit: boolean = false;

  constructor(private formBuilder: FormBuilder,private router: Router, private store: Store<IAppState<IUser>>) { }

  user$ = this.store.pipe(select(getSelectedUser))
    .subscribe( (user : IUser) =>{
      console.log(user)
      if(user == null){
        this.isEdit = false;
      }
      else{
        this.isEdit = true;
        this.user = user;
      }
      
    }
  );

  ngOnInit() {
    
    this.form = this.formBuilder.group({
      id: [''],
      name: ['', Validators.required],
      surname: ['', Validators.required],
      login: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      userRoles: ['', Validators.nullValidator]
    });

    if(this.isEdit)
      this.form.setValue(this.user);
  }

  onSubmit() {
    if(this.isEdit){
      this.store.dispatch(new Update(this.form.value, EntitiesEnum.User));
      this.router.navigate(['user','list-user'])
    }
    else{
      this.store.dispatch(new Create(this.form.value, EntitiesEnum.User));
      this.router.navigate(['user','list-user']);
    }
  }
}
