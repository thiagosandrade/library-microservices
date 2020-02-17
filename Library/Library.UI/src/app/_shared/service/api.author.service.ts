import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable} from "rxjs/index";
import {ApiAuthorResponse, ApiAuthorListResponse} from "../model/api.author.response";
import { Author } from '../model/author.model';
import { ApiResponse } from '../model/api.response';
import { map } from 'rxjs/internal/operators/map';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiAuthorService {

  constructor(private http: HttpClient) { }
  private baseUrl = `${environment.apiUrl}/author/`;

  getAuthors() : Observable<ApiAuthorListResponse> {
    return this.http.get<ApiAuthorListResponse>(this.baseUrl)
    .pipe(
      map( (response : ApiAuthorListResponse) => {
        return response;
      })
    )
  }

  getAuthorById(id: string): Observable<ApiAuthorResponse> {
    return this.http.get<ApiAuthorResponse>(this.baseUrl + id)
    .pipe(
      map( (response : ApiAuthorResponse) => {
          return response;
      })
    );
  }

  createAuthor(user: Author): Observable<ApiAuthorResponse> {
    return this.http.post<ApiAuthorResponse>(this.baseUrl, user);
  }

  updateAuthor(user: Author): Observable<ApiResponse> {
    return this.http.put<ApiResponse>(this.baseUrl, user);
  }

  deleteAuthor(id: string): Observable<ApiResponse> {
    return this.http.delete<ApiResponse>(this.baseUrl + id);
  }

}
