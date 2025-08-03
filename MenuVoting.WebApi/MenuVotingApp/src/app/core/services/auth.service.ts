import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { CookieService } from 'ngx-cookie-service';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly TOKEN_KEY = 'token';
  private isBrowser: boolean;

  constructor(
    private cookieService: CookieService,
    @Inject(PLATFORM_ID) platformId: Object
  ) {
    this.isBrowser = isPlatformBrowser(platformId);
  }

  getToken(): string | null {
    if (!this.isBrowser) return null;
    const token = this.cookieService.get(this.TOKEN_KEY);
    return token || null;
  }

  setToken(token: string | null): void {
    if (!this.isBrowser) return;

    if (token) {
      this.cookieService.set(this.TOKEN_KEY, token, {
        path: '/',
        sameSite: 'Strict',
        secure: true,
      });
    } else {
      this.cookieService.delete(this.TOKEN_KEY, '/');
    }
  }
}
