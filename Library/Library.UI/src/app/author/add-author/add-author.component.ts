import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { Create, EntitiesEnum } from 'src/app/store/actions/app.actions';
import { IAuthor } from 'src/app/_shared/model/author.model';

@Component({
  selector: 'app-add-author',
  templateUrl: './add-author.component.html',
  styleUrls: ['./add-author.component.scss']
})
export class AddAuthorComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private router: Router, private store: Store<IAppState<IAuthor>>) { }

  addForm: FormGroup;

  ngOnInit() {
    this.addForm = this.formBuilder.group({
      id: [''],
      name: ['', Validators.required],
      surname: ['', Validators.required],
      birth: ['', Validators.required]
    });
  }

  onSubmit() {
    this.store.dispatch(new Create(this.addForm.value, EntitiesEnum.Author));
    this.router.navigate(['author','list-author']);
  }
}
