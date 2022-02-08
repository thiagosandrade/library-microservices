import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Store, select } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { getSelectedUser } from 'src/app/store/selectors/author.selector';
import { Update, EntitiesEnum } from 'src/app/store/actions/app.actions';
import { IAuthor } from 'src/app/_shared/model/author.model';

@Component({
  selector: 'app-edit-author',
  templateUrl: './edit-author.component.html',
  styleUrls: ['./edit-author.component.scss']
})
export class EditAuthorComponent implements OnInit {

  editForm: FormGroup;
  author: IAuthor;

  constructor(private formBuilder: FormBuilder,private router: Router, private store: Store<IAppState<IAuthor>>) { }

  author$ = this.store.pipe(select(getSelectedUser))
    .subscribe( (author : IAuthor) =>{
      if(author == null){
        this.router.navigate(['author','list-author']);
        return;
      }
      this.author = author;
    }
  );

  ngOnInit() {
    
    this.editForm = this.formBuilder.group({
      id: [''],
      name: ['', Validators.required],
      surname: ['', Validators.required],
      birth: ['', Validators.required],
      age: ['', Validators.required],
      placeOfBirthId: ['', Validators.required],
      placeOfBirth: this.formBuilder.group({
        id: ['', Validators.required],
        city: ['', Validators.required],
        state: ['', Validators.required],
        country: ['', Validators.required]
      })
    });

    if(this.author != null)
      this.editForm.setValue(this.author);
  }

  onSubmit() {
    this.store.dispatch(new Update(this.editForm.value, EntitiesEnum.Author));
    this.router.navigate(['author','list-author'])
  }
}
