import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Menu } from 'src/app/models/menu/menu';
import { MenuPool } from 'src/app/models/menu-pool/menu-pool';

const API_BASE_URL: string = "https://localhost:7294/api/";

@Injectable({
  providedIn: 'root'
})

export class MenuVotingService {
  constructor(private httpClient: HttpClient) {

  }
  public getCurrentMenuPool(): Observable<MenuPool> {
    let headers = new HttpHeaders();
    headers = headers.set("Authorization", `Bearer ${localStorage['token']}`);
    return this.httpClient.get<MenuPool>(`${API_BASE_URL}menuvotings/current`, { headers: headers });
  }

  public createMenuPool(menuPool: MenuPool): Observable<MenuPool> {
    let headers = new HttpHeaders();
    headers = headers.append("Authorization", `Bearer ${localStorage['token']}`);
    console.log('createMenuPool: ' + localStorage['token']);
    return this.httpClient.post<MenuPool>(`${API_BASE_URL}menuvotings`, menuPool, { headers: headers });
  }

  public putMenuPool(menuPool: MenuPool): Observable<string> {
    let headers = new HttpHeaders();
    headers = headers.append("Authorization", `Bearer ${localStorage['token']}`);

    return this.httpClient.put<string>(`${API_BASE_URL}menuvotings/${menuPool.id}`, menuPool, { headers: headers });
  }

  public deleteMenuPool(id: string | null): Observable<string> {
    let headers = new HttpHeaders();
    headers = headers.append("Authorization", `Bearer ${localStorage['token']}`);

    return this.httpClient.delete<string>(`${API_BASE_URL}menuvotings/${id}`, { headers: headers });
  }

  public deleteMenu(id: string | null): Observable<string> {
    let headers = new HttpHeaders();
    headers = headers.append("Authorization", `Bearer ${localStorage['token']}`);

    return this.httpClient.delete<string>(`${API_BASE_URL}menuvotings/menu/${id}`, { headers: headers });
  }

  public postMenu(menu: Menu): Observable<Menu> {
    let headers = new HttpHeaders();
    headers = headers.append("Authorization", `Bearer ${localStorage['token']}`);

    return this.httpClient.post<Menu>(`${API_BASE_URL}menuvotings/menu`, menu, { headers: headers });
  }
}
