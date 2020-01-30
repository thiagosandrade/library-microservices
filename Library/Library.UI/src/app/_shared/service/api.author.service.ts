import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable} from "rxjs/index";
import {ApiAuthorResponse, ApiAuthorListResponse} from "../model/api.author.response";
import { Author } from '../model/author.model';

@Injectable({
  providedIn: 'root'
})
export class ApiAuthorService {

  constructor(private http: HttpClient) { }
  baseUrl: string = 'http://localhost:59580/author/';

  getAuthors() : Observable<ApiAuthorListResponse> {
    return this.http.get<ApiAuthorListResponse>(this.baseUrl);
  }

  getAuthorById(id: string): Observable<ApiAuthorResponse> {
    return this.http.get<ApiAuthorResponse>(this.baseUrl + id);
  }

  createAuthor(user: Author): Observable<ApiAuthorResponse> {
    return this.http.post<ApiAuthorResponse>(this.baseUrl, user);
  }

  updateAuthor(user: Author): Observable<ApiAuthorResponse> {
    return this.http.put<ApiAuthorResponse>(this.baseUrl + user.id, user);
  }

  deleteAuthor(id: string): Observable<ApiAuthorResponse> {
    return this.http.delete<ApiAuthorResponse>(this.baseUrl + id);
  }

}
