import { Injectable } from '@angular/core';
import { Restaurant } from 'src/app/core/models/restaurant';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ENDPOINTS } from './api-endpoints';

@Injectable({
  providedIn: 'root',
})
export class RestaurantService {
  constructor(private httpClient: HttpClient) {}
  public getRestaurants(): Observable<Restaurant[]> {
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${localStorage['token']}`);
    return this.httpClient.get<Restaurant[]>(`${ENDPOINTS.RESTAURANTS}`, {
      headers: headers,
    });
  }
  public postRestaurant(Restaurant: Restaurant): Observable<string | null> {
    let headers = new HttpHeaders();
    headers = headers.append(
      'Authorization',
      `Bearer ${localStorage['token']}`
    );
    return this.httpClient.post<string | null>(
      `${ENDPOINTS.RESTAURANTS}`,
      Restaurant,
      { headers: headers }
    );
  }
  public putRestaurant(Restaurant: Restaurant): Observable<string> {
    let headers = new HttpHeaders();
    headers = headers.append(
      'Authorization',
      `Bearer ${localStorage['token']}`
    );

    return this.httpClient.put<string>(
      `${ENDPOINTS.RESTAURANTS}/${Restaurant.id}`,
      Restaurant,
      { headers: headers }
    );
  }
  public deleteRestaurant(id: string | null): Observable<string> {
    let headers = new HttpHeaders();
    headers = headers.append(
      'Authorization',
      `Bearer ${localStorage['token']}`
    );

    return this.httpClient.delete<string>(`${ENDPOINTS.RESTAURANTS}/${id}`, {
      headers: headers,
    });
  }
}
