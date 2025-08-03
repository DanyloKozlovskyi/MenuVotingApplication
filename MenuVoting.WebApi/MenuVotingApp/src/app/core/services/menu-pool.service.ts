import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MenuPool, MenuPoolCreate } from 'src/app/core/models';
import { ENDPOINTS } from './api-endpoints';

@Injectable({ providedIn: 'root' })
export class MenuPoolService {
  private readonly baseUrl = ENDPOINTS.MENUPOOLS;

  constructor(private http: HttpClient) {}

  getCurrentMenuPool(): Observable<MenuPool> {
    return this.http.get<MenuPool>(`${this.baseUrl}/current`);
  }

  createMenuPool(payload: MenuPoolCreate): Observable<MenuPool> {
    return this.http.post<MenuPool>(this.baseUrl, payload);
  }

  updateMenuPool(id: string, payload: MenuPool): Observable<string> {
    return this.http.put<string>(`${this.baseUrl}/${id}`, payload);
  }

  deleteMenuPool(id: string): Observable<string> {
    return this.http.delete<string>(`${this.baseUrl}/${id}`);
  }
}
