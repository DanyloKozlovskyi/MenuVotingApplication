import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Menu } from 'src/app/models/menu/menu';
import { MenuPool } from 'src/app/models/menu-pool/menu-pool';
import { Vote } from 'src/app/models/vote/vote';

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
    return this.httpClient.post<MenuPool>(`${API_BASE_URL}menuvotings`, menuPool, { headers: headers });
  }

  public putMenuPool(menuPoolId: string | null, menuPool: MenuPool): Observable<string> {
    let headers = new HttpHeaders();
    headers = headers.append("Authorization", `Bearer ${localStorage['token']}`);
    return this.httpClient.put<string>(`${API_BASE_URL}menuvotings/${menuPoolId}`, menuPool, { headers: headers });
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

  public postMenu(menuPoolId: string | null, menu: Menu): Observable<Menu> {
    let headers = new HttpHeaders();
    headers = headers.append("Authorization", `Bearer ${localStorage['token']}`);
    return this.httpClient.post<Menu>(`${API_BASE_URL}menuvotings/${menuPoolId}/menu`, menu, { headers: headers });
  }

  public getCurrentVote(menuPoolId: string | null): Observable<Vote> {
    let headers = new HttpHeaders();
    headers = headers.set("Authorization", `Bearer ${localStorage['token']}`);
    return this.httpClient.get<Vote>(`${API_BASE_URL}menuvotings/${menuPoolId}/vote/current`, { headers: headers });
  }

  public castVote(menuPoolId: string | null, vote: Vote): Observable<Vote> {
    let headers = new HttpHeaders();
    headers = headers.append("Authorization", `Bearer ${localStorage['token']}`);
    return this.httpClient.post<Vote>(`${API_BASE_URL}menuvotings/${menuPoolId}/vote`, vote, { headers: headers });
  }
}
