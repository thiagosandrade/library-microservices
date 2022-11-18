import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs/index";
import { ApiBookResponse, ApiBookListResponse } from "../model/api.book.response";
import { ApiResponse } from '../model/api.response';
import { map } from 'rxjs/internal/operators/map';
import { environment } from 'src/environments/environment';
import { DatePipe } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class ApiBookService {

  constructor(private http: HttpClient, private datePipe : DatePipe) { }
  private baseUrl = `${environment.apiUrl}/book/`;

  getBooks(token: string) : Observable<ApiBookListResponse> {
    return this.http.get<ApiBookListResponse>(this.baseUrl, {
      headers: {
        "Authorization": token
      }
    })
    .pipe(
      map( (response : ApiBookListResponse) => {
        response.value.map(book => {
          
          let dateFormatted = this.datePipe.transform(book.publishedDate, 'yyyy/MM/dd')
          book.publishedDate = dateFormatted;

          let authors = ''; 
          book.authors.map(author => {
            authors = authors.concat(authors !== '' ? ', ' : '', author.name)
          });

          book.authorsAsString = authors;
        })
        return response;
      })
    )
  }

  getBookById(id: string, token: string): Observable<ApiBookResponse> {
    return this.http.get<ApiBookResponse>(this.baseUrl + id, {
      headers: {
        "Authorization": token
      }
    })
    .pipe(
      map( (response : ApiBookResponse) => {
            let dateFormatted = this.datePipe.transform(response.value.publishedDate, 'yyyy/MM/dd')
            response.value.publishedDate = dateFormatted;
            return response;
      })
    );
  }

  createBook(user: any, token: string): Observable<ApiBookResponse> {
    return this.http.post<ApiBookResponse>(this.baseUrl, user, {
      headers: {
        "Authorization": token
      }
    });
  }

  updateBook(user: any, token: string): Observable<ApiResponse> {
    return this.http.put<ApiResponse>(this.baseUrl, user, {
      headers: {
        "Authorization": token
      }
    });
  }

  deleteBook(id: number, token: string): Observable<ApiResponse> {
    return this.http.delete<ApiResponse>(this.baseUrl + id, {
      headers: {
        "Authorization": token
      }
    });
  }

}
