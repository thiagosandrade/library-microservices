import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { getSelectedBook } from 'src/app/store/selectors/book.selector';
import { Update, EntitiesEnum } from 'src/app/store/actions/app.actions';
import { IBook } from 'src/app/_shared/model/book.model';
import { Observable } from 'rxjs/internal/Observable';
import { IAuthor } from 'src/app/_shared/model/author.model';
import { selectAuthorList } from 'src/app/store/selectors/author.selector';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { ApiCategoryService } from 'src/app/_shared/service/api.category.service';

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.component.html',
  styleUrls: ['./edit-book.component.scss']
})
export class EditBookComponent implements OnInit {

  editForm: FormGroup;
  book: IBook;

  constructor(private formBuilder: FormBuilder,
    private router: Router, 
    private categoryService : ApiCategoryService,
    private store: Store<IAppState<IBook>>) { }

  authors$ : Observable<IAuthor[]> = this.store.select(selectAuthorList);

  book$ = this.store.select(getSelectedBook)
    .subscribe( (book : IBook) =>{
      if(book == null){
        this.router.navigate(['book','list-book']);
        return;
      }
      this.book = book;
    }
  );

  dropdownList = [];
  selectedItems = [];
  dropdownSettings: IDropdownSettings = {};
  isDropdownAvailable: boolean = false;

  
  dropdownCategoryList = [];
  selectedItemsCategory = [];
  isDropdownAvailableCategory: boolean = false;

  ngOnInit() {
    
    this.authors$.subscribe(result => {
      result != null && result.map(author => this.dropdownList.push({item_id: author.id, item_text: author.name}))
      this.isDropdownAvailable = true;
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

    if(this.book != null){
      this.book.authors.forEach(item => {
        this.selectedItems.push({item_id: item.id, item_text: item.name})
      })

      this.book.categories.forEach(item => {
        this.selectedItemsCategory.push({item_id: item.id, item_text: item.name})
      })

      this.editForm = this.formBuilder.group({
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
      categories: ['', Validators.required],
    });

    this.editForm.patchValue(this.book);
    this.editForm.get("authors").setValue(this.selectedItems)
    this.editForm.get("categories").setValue(this.selectedItemsCategory)

    }
  }

  onSubmit() {
    let value = this.editForm.value;

    value.authors = value.authors.map(item => {
      return {
        authorId: item.item_id,
        bookId: value.id 
      }
    })

    value.categories = value.categories.map(item => {
      return {
        categoryId: item.item_id,
        bookId: value.id 
      }
    })

    this.store.dispatch(new Update(value, EntitiesEnum.Book));
    this.router.navigate(['book','list-book'])
  }
}
