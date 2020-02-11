import { Component, OnInit } from '@angular/core';
import { ApiAuthorService } from 'src/app/_shared/service/api.author.service';
import { Router } from '@angular/router';
import { Author } from 'src/app/_shared/model/author.model';
import { ApiAuthorListResponse } from 'src/app/_shared/model/api.author.response';

@Component({
  selector: 'app-list-author',
  templateUrl: './list-author.component.html',
  styleUrls: ['./list-author.component.scss']
})
export class ListAuthorComponent implements OnInit {

  authors: Author[] = [];
  
  constructor(private router: Router, private apiService: ApiAuthorService) { }

  ngOnInit() {
    if(!window.localStorage.getItem('token')) {
      this.router.navigate(['login']);
      return;
    }
    this.apiService.getAuthors()
      .subscribe( (data : ApiAuthorListResponse) => {
        this.authors = data.value.authors;
      });
  }

  deleteAuthor(author: Author): void {
    this.apiService.deleteAuthor(author.id)
      .subscribe( data => {
        this.authors = this.authors.filter((u: Author) => u !== author);
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

}
