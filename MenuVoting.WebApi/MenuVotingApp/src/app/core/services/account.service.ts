import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterUser } from 'src/app/core/models/register-user';
import { Observable } from 'rxjs';
import { LoginUser } from 'src/app/core/models/login-user';
import { jwtDecode } from 'jwt-decode';
import { ENDPOINTS } from './api-endpoints';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  currentToken: string | null = null;
  restaurantId: string | null = null;
  isAdmin: boolean = false;
  userId: string | null = null;
  constructor(private httpClient: HttpClient) {}

  setUserRole(token: string | null) {
    if (token != null) {
      const decodedToken = jwtDecode<{ [key: string]: any }>(token);
      const role =
        decodedToken[
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        ];
      this.restaurantId = decodedToken['Organization'];
      this.userId = decodedToken['sub'];
      this.isAdmin = role === 'Admin';
    }
  }

  public postRegister(registerUser: RegisterUser): Observable<any> {
    return this.httpClient.post<any>(
      `${ENDPOINTS.ACCOUNT}/register`,
      registerUser
    );
  }

  public postLogin(loginUser: LoginUser): Observable<any> {
    return this.httpClient.post<any>(`${ENDPOINTS.ACCOUNT}/login`, loginUser);
  }

  public getLogout(): Observable<string> {
    return this.httpClient.get<string>(`${ENDPOINTS.ACCOUNT}/logout`);
  }

  public postGenerateNewToken(): Observable<any> {
    var token = localStorage['token'];
    var refreshToken = localStorage['refreshToken'];

    return this.httpClient.post<any>(
      `${ENDPOINTS.ACCOUNT}/generate-new-jwt-token`,
      {
        token: token,
        refreshToken: refreshToken,
      }
    );
  }
}
