import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaginatedResult, User } from '../models/user.model';
import { environment } from '../../environments/environment';

const baseUrl = environment.apiUrl + "users";

@Injectable({
  providedIn: 'root'
})

export class UserService {

  constructor(private http: HttpClient) { }

  getAll(search: string = "", pageIndex: number = 1, pageSize: number = 10): Observable<PaginatedResult<User>> {
    return this.http.get<PaginatedResult<User>>(baseUrl + "?search=" + search);
  }

  getById(id: any): Observable<User> {
    return this.http.get<User>(`${baseUrl}/getById?userId=${id}`);
  }

  create(data: any): Observable<any> {
    return this.http.post(baseUrl, data);
  }

  update(id: any, data: any): Observable<any> {
    return this.http.put(`${baseUrl}`, data);
  }

  delete(id: any): Observable<any> {
    return this.http.delete(`${baseUrl}?userId=${id}`);
  }

}
