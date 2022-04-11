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
import { ApiCategoryService } from 'src/app/_shared/service/api.category.service';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.scss']
})
export class AddBookComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, 
    private router: Router, 
    private categoryService : ApiCategoryService,
    private store: Store<IAppState<IBook>>) { }

  addForm: FormGroup;

  authors$ : Observable<IAuthor[]> = this.store.select(selectAuthorList);

  dropdownList = [];
  selectedItems = [];
  dropdownSettings: IDropdownSettings = {};
  dropDownForm: FormGroup;

  dropdownCategoryList = [];
  selectedItemsCategory = [];
  isDropdownAvailableCategory: boolean = false;

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
      authors: ['', Validators.required],
      categories: ['', Validators.required]
    });

    this.authors$.subscribe(result => {
      result != null && result.map(author => this.dropdownList.push({item_id: author.id, item_text: author.name}))
    })

    this.categoryService.getCategories()
      .subscribe(result => {
        result != null && result.map(category => this.dropdownCategoryList.push({item_id: category.id, item_text: category.name}))
        this.isDropdownAvailableCategory = true;
      })

    this.dropdownSettings = {
      idField: 'item_id',
      textField: 'item_text',
      allowSearchFilter: true
    }

  }

  onSubmit() {
    let value = this.addForm.value;
    
    value.authors = value.authors.map(element => {
      return {
        authorId: element.item_id,
        bookId: 0
      }
    });

    value.categories = value.categories.map(element => {
      return {
        categoryId: element.item_id,
        bookId: 0
      }
    });

    this.store.dispatch(new Create(value, EntitiesEnum.Book));
    this.router.navigate(['book','list-book']);
  }
}
