import { Component, OnInit } from '@angular/core';
import { ApiAuthorService } from 'src/app/_shared/service/api.author.service';
import { Router } from '@angular/router';
import { Author } from 'src/app/_shared/model/author.model';
import { ApiAuthorListResponse } from 'src/app/_shared/model/api.author.response';
import { DatePipe } from '@angular/common';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { SignalRService } from 'src/app/_shared/signalR/signalR.service';
import { ApiLoginService } from 'src/app/_shared/service/api.login.service';

@Component({
  selector: 'app-list-author',
  templateUrl: './list-author.component.html',
  styleUrls: ['./list-author.component.scss']
})
export class ListAuthorComponent implements OnInit {

  authors: Author[] = [];

  authors$ : Observable<Author[]>;
  
  constructor(private router: Router, private apiService: ApiAuthorService, private datePipe : DatePipe, private apiLoginService: ApiLoginService,
    private signalRService: SignalRService) { }

  ngOnInit() {

    if(!this.apiLoginService.isLogged()) {
      this.router.navigate(['login']);
      return;
    }

    this.signalRService.StartHub();
    this.signalRService.notificationReceived.subscribe(() => {
      this.authors$ = this.getAuthors();
  });

    this.authors$ = this.getAuthors();
  }

  deleteAuthor(author: Author): void {
    this.apiService.deleteAuthor(author.id)
      .subscribe( () => {
        this.authors$ = this.getAuthors();
      })
  };

  editAuthor(user: Author): void {
    localStorage.removeItem("editAuthorId");
    localStorage.setItem("editAuthorId", user.id);
    this.router.navigate(['author','edit-author']);
  };

  addAuthor(): void {
    this.router.navigate(['author','add-author']);
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
}
