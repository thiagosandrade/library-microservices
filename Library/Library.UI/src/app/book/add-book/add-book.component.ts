import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { select, Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { Create, EntitiesEnum } from 'src/app/store/actions/app.actions';
import { IBook } from 'src/app/_shared/model/book.model';
import { Observable } from 'rxjs/internal/Observable';
import { IAuthor } from 'src/app/_shared/model/author.model';
import { selectAuthorList } from 'src/app/store/selectors/author.selector';
import { IDropdownSettings } from 'ng-multiselect-dropdown';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.scss']
})
export class AddBookComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private router: Router, private store: Store<IAppState<IBook>>) { }

  addForm: FormGroup;

  authors$ : Observable<IAuthor[]> = this.store.pipe(select(selectAuthorList));

  dropdownList = [];
  selectedItems = [];
  dropdownSettings: IDropdownSettings = {};
  dropDownForm: FormGroup;

  ngOnInit() {
    this.addForm = this.formBuilder.group({
      id: [''],
      title: ['', Validators.required],
      isbn: ['', Validators.required],
      pageCount: ['', Validators.required],
      publishedDate: ['', Validators.required],
      thumbnailUrl: ['', Validators.required],
      shortDescription: ['', Validators.required],
      longDescription: ['', Validators.required],
      status: ['', Validators.required],
      authors: ['', Validators.required]
    });

    this.authors$.subscribe(result => {
      result != null && result.map(author => this.dropdownList.push({item_id: author.id, item_text: author.name}))
    })

    this.dropdownSettings = {
      idField: 'item_id',
      textField: 'item_text',
      allowSearchFilter: true
    }
  }

  onSubmit() {
    this.store.dispatch(new Create(this.addForm.value, EntitiesEnum.Book));
    this.router.navigate(['book','list-book']);
  }
}
