import { Injectable } from '@angular/core';
import { Menu, MenuCreate } from 'src/app/core/models/menu';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ENDPOINTS } from './api-endpoints';

@Injectable({
  providedIn: 'root',
})
export class MenuService {
  constructor(private httpClient: HttpClient) {}

  public deleteMenu(id: string): Observable<string> {
    let headers = new HttpHeaders();
    headers = headers.append(
      'Authorization',
      `Bearer ${localStorage['token']}`
    );
    return this.httpClient.delete<string>(`${ENDPOINTS.MENUS}/${id}`, {
      headers: headers,
    });
  }

  public postMenu(menuPoolId: string, menu: MenuCreate): Observable<Menu> {
    let headers = new HttpHeaders();
    headers = headers.append(
      'Authorization',
      `Bearer ${localStorage['token']}`
    );
    return this.httpClient.post<Menu>(`${ENDPOINTS.MENUS}/${menuPoolId}`, menu, {
      headers: headers,
    });
  }
}
