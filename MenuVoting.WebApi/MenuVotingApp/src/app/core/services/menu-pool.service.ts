import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { MenuPool, MenuPoolCreate } from 'src/app/core/models/menu-pool';
import { ENDPOINTS } from './api-endpoints';

@Injectable({
  providedIn: 'root',
})
export class MenuPoolService {
  constructor(private httpClient: HttpClient) {}

  public getCurrentMenuPool(): Observable<MenuPool> {
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${localStorage['token']}`);
    return this.httpClient.get<MenuPool>(`${ENDPOINTS.MENUPOOLS}/current`, {
      headers: headers,
    });
  }

  public createMenuPool(menuPool: MenuPoolCreate): Observable<MenuPool> {
    let headers = new HttpHeaders();
    headers = headers.append(
      'Authorization',
      `Bearer ${localStorage['token']}`
    );
    return this.httpClient.post<MenuPool>(
      `${ENDPOINTS.MENUPOOLS}`,
      menuPool,
      { headers: headers }
    );
  }

  public putMenuPool(
    menuPoolId: string,
    menuPool: MenuPool
  ): Observable<string> {
    let headers = new HttpHeaders();
    headers = headers.append(
      'Authorization',
      `Bearer ${localStorage['token']}`
    );
    return this.httpClient.put<string>(
      `${ENDPOINTS.MENUPOOLS}/${menuPoolId}`,
      menuPool,
      { headers: headers }
    );
  }

  public deleteMenuPool(id: string): Observable<string> {
    let headers = new HttpHeaders();
    headers = headers.append(
      'Authorization',
      `Bearer ${localStorage['token']}`
    );
    return this.httpClient.delete<string>(`${ENDPOINTS.MENUPOOLS}/${id}`, {
      headers: headers,
    });
  }
}
