import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaginatedResult, User } from '../models/user.model';

const baseUrl = "https://localhost:7085/api/users"

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getAll(search: string = "", pageIndex: number = 1, pageSize: number = 10): Observable<PaginatedResult<User>> {
    return this.http.get<PaginatedResult<User>>(baseUrl + "?search=" + search
      + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize);
  }

  get(id: any): Observable<User> {
    return this.http.get<User>(`${baseUrl}/getById/${id}`);
  }

  create(data: any): Observable<any> {
    return this.http.post(baseUrl, data);
  }

  update(id: any, data: any): Observable<any> {
    return this.http.put(`${baseUrl}/${id}`, data);
  }

  delete(id: any): Observable<any> {
    return this.http.delete(`${baseUrl}/${id}`);
  }

  findByTitle(title: any): Observable<User[]> {
    return this.http.get<User[]>(`${baseUrl}?title=${title}`);
  }
}
