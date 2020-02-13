import { Component, OnInit } from '@angular/core';
import { ApiAuthorService } from 'src/app/_shared/service/api.author.service';
import { Router } from '@angular/router';
import { Author } from 'src/app/_shared/model/author.model';
import { ApiAuthorListResponse } from 'src/app/_shared/model/api.author.response';
import { DatePipe } from '@angular/common';
import { map, takeUntil, switchMap, filter, tap } from 'rxjs/operators';
import { Subject, Observable, pipe } from 'rxjs';

@Component({
  selector: 'app-list-author',
  templateUrl: './list-author.component.html',
  styleUrls: ['./list-author.component.scss']
})
export class ListAuthorComponent implements OnInit {

  private unsubscribe$ = new Subject<void>();
  authors: Author[] = [];

  authors$ : Observable<Author[]>;
  
  constructor(private router: Router, private apiService: ApiAuthorService, private datePipe : DatePipe) { }

  ngOnInit() {
    if(!window.localStorage.getItem('token')) {
      this.router.navigate(['login']);
      return;
    }

    this.authors$ = this.getAuthors();
  }

  deleteAuthor(author: Author): void {
    this.apiService.deleteAuthor(author.id)
      .subscribe( () => {
        this.authors$ = this.getAuthors();
      })
  };

  editAuthor(user: Author): void {
    window.localStorage.removeItem("editAuthorId");
    window.localStorage.setItem("editAuthorId", user.id);
    this.router.navigate(['edit-author']);
  };

  addAuthor(): void {
    this.router.navigate(['add-author']);
  };

  getAuthors(): Observable<Author[]> {
    return this.apiService.getAuthors().pipe(
      map((data : ApiAuthorListResponse) => {
        data.value.authors.map(author => {
          let dateFormatted = this.datePipe.transform(author.birth, 'yyyy/MM/dd')
          author.birth = dateFormatted;
        })

        this.authors = data.value.authors;        

        return data.value.authors;
      })
    )
  }

  ngOnDestroy(){
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }


}
