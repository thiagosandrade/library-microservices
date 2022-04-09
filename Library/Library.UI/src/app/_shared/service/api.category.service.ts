import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs/index";
import { ICategory } from '../model/category.model';
import { ApiResponse } from '../model/api.response';
import { map } from 'rxjs/internal/operators/map';
import { environment } from 'src/environments/environment';
import { DatePipe } from '@angular/common';
import { ApiCategoryListResponse, ApiCategoryResponse } from '../model/api.category.response';

@Injectable({
  providedIn: 'root'
})
export class ApiCategoryService {

  constructor(private http: HttpClient, private datePipe : DatePipe) { }
  private baseUrl = `${environment.apiUrl}/category/`;

  getCategories() : Observable<ICategory[]> {
    return this.http.get<ApiCategoryListResponse>(this.baseUrl)
    .pipe(
      map( (response : ApiCategoryListResponse) => {
        return response.value;
      })
    )
  }

  getCategoryById(id: string): Observable<ApiCategoryResponse> {
    return this.http.get<ApiCategoryResponse>(this.baseUrl + id)
    .pipe(
      map( (response : ApiCategoryResponse) => {
            return response;
      })
    );
  }

  createCategory(user: ICategory): Observable<ApiCategoryResponse> {
    return this.http.post<ApiCategoryResponse>(this.baseUrl, user);
  }

  updateCategory(user: ICategory): Observable<ApiResponse> {
    return this.http.put<ApiResponse>(this.baseUrl, user);
  }

  deleteCategory(id: number): Observable<ApiResponse> {
    return this.http.delete<ApiResponse>(this.baseUrl + id);
  }

}
