import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Menu, MenuCreate } from 'src/app/core/models';
import { ENDPOINTS } from './api-endpoints';

@Injectable({ providedIn: 'root' })
export class MenuService {
  private readonly baseUrl = ENDPOINTS.MENUS;

  constructor(private http: HttpClient) {}

  deleteMenu(id: string): Observable<string> {
    return this.http.delete<string>(`${this.baseUrl}/${id}`);
  }

  postMenu(menuPoolId: string, menu: MenuCreate): Observable<Menu> {
    return this.http.post<Menu>(`${this.baseUrl}/${menuPoolId}`, menu);
  }
}
