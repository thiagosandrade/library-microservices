import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs/index";
import { ApiAuthorResponse, ApiAuthorListResponse } from "../model/api.author.response";
import { IAuthor } from '../model/author.model';
import { ApiResponse } from '../model/api.response';
import { map } from 'rxjs/internal/operators/map';
import { environment } from 'src/environments/environment';
import { DatePipe } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class ApiAuthorService {

  constructor(private http: HttpClient, private datePipe : DatePipe) { }
  private baseUrl = `${environment.apiUrl}/author/`;

  getAuthors(token: string) : Observable<ApiAuthorListResponse> {
    return this.http.get<ApiAuthorListResponse>(this.baseUrl, {
      headers: {
        "Authorization": token
      }
    })
    .pipe(
      map( (response : ApiAuthorListResponse) => {
          response.value.map(author => {
            let dateFormatted = this.datePipe.transform(author.birth, 'yyyy/MM/dd')
            author.birth = dateFormatted;
          })
        return response;
      })
    )
  }

  getAuthorById(id: string, token: string): Observable<ApiAuthorResponse> {
    return this.http.get<ApiAuthorResponse>(this.baseUrl + id, {
      headers: {
        "Authorization": token
      }
    })
    .pipe(
      map( (response : ApiAuthorResponse) => {
            let dateFormatted = this.datePipe.transform(response.value.birth, 'yyyy/MM/dd')
            response.value.birth = dateFormatted;
            return response;
      })
    );
  }

  createAuthor(user: any, token: string): Observable<ApiAuthorResponse> {
    return this.http.post<ApiAuthorResponse>(this.baseUrl, user, {
      headers: {
        "Authorization": token
      }
    });
  }

  updateAuthor(user: any, token: string): Observable<ApiResponse> {
    return this.http.put<ApiResponse>(this.baseUrl, user, {
      headers: {
        "Authorization": token
      }
    });
  }

  deleteAuthor(id: number, token: string): Observable<ApiResponse> {
    return this.http.delete<ApiResponse>(this.baseUrl + id, {
      headers: {
        "Authorization": token
      }
    });
  }

}
