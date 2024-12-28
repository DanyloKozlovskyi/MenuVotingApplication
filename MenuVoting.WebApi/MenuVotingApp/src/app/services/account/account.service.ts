import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterUser } from 'src/app/models/register-user/register-user';
import { Observable } from 'rxjs';
import { LoginUser } from 'src/app/models/login-user/login-user';
import { jwtDecode } from 'jwt-decode';

const API_BASE_URL: string = "https://localhost:7294/api/account/";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  currentUserName: string | null = null;
  restaurantId: string | null = null;
  isAdmin: boolean = false;
  constructor(private httpClient: HttpClient) { }

  setUserRole(token: string | null) {
    if (token != null) {
      const decodedToken = jwtDecode<{ [key: string]: any }>(token);
      const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      this.restaurantId = decodedToken['Organization'];
      console.log(token);
      console.log(decodedToken);
      console.log(role);
      this.isAdmin = role === 'Admin';
    }
  }

  public postRegister(registerUser: RegisterUser): Observable<any> {
    return this.httpClient.post<any>(`${API_BASE_URL}register`, registerUser);
  }

  public postLogin(loginUser: LoginUser): Observable<any> {
    return this.httpClient.post<any>(`${API_BASE_URL}login`, loginUser);
  }

  public getLogout(): Observable<string> {
    return this.httpClient.get<string>(`${API_BASE_URL}logout`);
  }

  public postGenerateNewToken(): Observable<any> {
    var token = localStorage["token"];
    var refreshToken = localStorage["refreshToken"];

    return this.httpClient.post<any>(`${API_BASE_URL}generate-new-jwt-token`, { token: token, refreshToken: refreshToken });
  }
}
