import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { Create, EntitiesEnum } from 'src/app/store/actions/app.actions';
import { IUser } from 'src/app/_shared/model/user.model';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss']
})
export class AddUserComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private router: Router, private store: Store<IAppState<IUser>>) { }

  addForm: FormGroup;

  ngOnInit() {
    this.addForm = this.formBuilder.group({
      id: [''],
      name: ['', Validators.required],
      surname: ['', Validators.required],
      login: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  onSubmit() {
    this.store.dispatch(new Create(this.addForm.value, EntitiesEnum.User));
    this.router.navigate(['user','list-user']);
  }
}
