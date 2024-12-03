import { Injectable } from '@angular/core';
import { Restaurant } from "src/app/models/restaurant";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from "rxjs";

const API_BASE_URL: string = "https://localhost:7294/api/";

@Injectable({
  providedIn: 'root'
})

export class RestaurantService {
  constructor(private httpClient: HttpClient) {

  }
  public getRestaurants(): Observable<Restaurant[]> {
    console.log("Get Restaurants " + "localStorage['token']: " + localStorage['token']);
    let headers = new HttpHeaders();
    headers = headers.set("Authorization", `Bearer ${localStorage['token']}`);
    return this.httpClient.get<Restaurant[]>(`${API_BASE_URL}restaurants`, { headers: headers });
  }
  public postRestaurant(Restaurant: Restaurant): Observable<string | null> {
    //console.log(Restaurant);
    let headers = new HttpHeaders();
    headers = headers.append("Authorization", `Bearer ${localStorage['token']}`);
    return this.httpClient.post<string | null>(`${API_BASE_URL}restaurants`, Restaurant, { headers: headers });
  }
  public putRestaurant(Restaurant: Restaurant): Observable<string> {
    let headers = new HttpHeaders();
    headers = headers.append("Authorization", `Bearer ${localStorage['token']}`);

    return this.httpClient.put<string>(`${API_BASE_URL}restaurants/${Restaurant.id}`, Restaurant, { headers: headers });
  }
  public deleteRestaurant(id: string | null): Observable<string> {
    let headers = new HttpHeaders();
    headers = headers.append("Authorization", `Bearer ${localStorage['token']}`);

    return this.httpClient.delete<string>(`${API_BASE_URL}restaurants/${id}`, { headers: headers });
  }
}
