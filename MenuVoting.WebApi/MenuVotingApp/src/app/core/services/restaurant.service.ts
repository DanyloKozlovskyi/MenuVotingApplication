import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Restaurant } from 'src/app/core/models';
import { ENDPOINTS } from './api-endpoints';

@Injectable({ providedIn: 'root' })
export class RestaurantService {
  private readonly baseUrl = ENDPOINTS.RESTAURANTS;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Restaurant[]> {
    return this.http.get<Restaurant[]>(this.baseUrl);
  }

  create(restaurant: Restaurant): Observable<string | null> {
    return this.http.post<string | null>(this.baseUrl, restaurant);
  }

  update(restaurant: Restaurant): Observable<string> {
    return this.http.put<string>(
      `${this.baseUrl}/${restaurant.id}`,
      restaurant
    );
  }

  delete(id: string): Observable<string> {
    return this.http.delete<string>(`${this.baseUrl}/${id}`);
  }
}
