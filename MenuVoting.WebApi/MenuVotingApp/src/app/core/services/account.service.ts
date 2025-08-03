import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { isPlatformBrowser } from '@angular/common';
import { BehaviorSubject, Observable, map, tap } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { CookieService } from 'ngx-cookie-service';
import { ENDPOINTS } from './api-endpoints';
import { LoginUser, RegisterUser } from 'src/app/core/models';

interface AuthResponse {
  token: string;
  refreshToken: string;
}

interface DecodedToken {
  sub: string;
  Organization: string;
  'http://schemas.microsoft.com/ws/2008/06/identity/claims/role': string;
}

@Injectable({ providedIn: 'root' })
export class AccountService {
  private isBrowser: boolean;
  private tokenSubject = new BehaviorSubject<string | null>(null);
  readonly token$ = this.tokenSubject.asObservable();

  readonly isAdmin$ = this.token$.pipe(
    map((token) => {
      if (!token) return false;
      const {
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/role': role,
      } = jwtDecode<DecodedToken>(token);
      return role === 'Admin';
    })
  );

  readonly userId$ = this.token$.pipe(
    map((token) => (token ? jwtDecode<DecodedToken>(token).sub : null))
  );

  readonly restaurantId$ = this.token$.pipe(
    map((token) => (token ? jwtDecode<DecodedToken>(token).Organization : null))
  );

  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
    @Inject(PLATFORM_ID) platformId: Object
  ) {
    this.isBrowser = isPlatformBrowser(platformId);
    if (this.isBrowser) {
      const saved = this.cookieService.get('token');
      this.tokenSubject.next(saved || null);
    }
  }

  register(payload: RegisterUser): Observable<void> {
    return this.http.post<void>(`${ENDPOINTS.ACCOUNT}/register`, payload);
  }

  login(payload: LoginUser): Observable<void> {
    return this.http
      .post<AuthResponse>(`${ENDPOINTS.ACCOUNT}/login`, payload, {
        withCredentials: true,
      })
      .pipe(
        tap((res) => this.saveTokens(res)),
        map(() => void 0)
      );
  }

  logout(): Observable<void> {
    return this.http
      .get<void>(
        `${ENDPOINTS.ACCOUNT}/logout`,
        {
          withCredentials: true,
        }
      )
      .pipe(
        tap(() => this.clearTokens()),
        map(() => void 0)
      );
  }

  refreshToken(): Observable<void> {
    return this.http
      .post<AuthResponse>(
        `${ENDPOINTS.ACCOUNT}/generate-new-jwt-token`,
        {},
        { withCredentials: true }
      )
      .pipe(
        tap((res) => this.saveTokens(res)),
        map(() => void 0)
      );
  }

  private saveTokens(res: AuthResponse) {
    if (!this.isBrowser) return;

    // Store as secure, sameSite=strict cookies
    this.cookieService.set('token', res.token, {
      path: '/',
      secure: true,
      sameSite: 'Strict',
    });
    this.cookieService.set('refreshToken', res.refreshToken, {
      path: '/',
      secure: true,
      sameSite: 'Strict',
    });
    this.tokenSubject.next(res.token);
  }

  private clearTokens() {
    if (this.isBrowser) {
      this.cookieService.delete('token', '/');
      this.cookieService.delete('refreshToken', '/');
    }
    this.tokenSubject.next(null);
  }
}
