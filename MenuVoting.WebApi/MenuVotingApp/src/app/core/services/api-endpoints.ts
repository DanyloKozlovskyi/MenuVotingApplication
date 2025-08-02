import { environment } from 'src/environments/environment';

export const ENDPOINTS = {
  MENUS: `${environment.baseApi}/menus`,
  RESTAURANTS: `${environment.baseApi}/restaurants`,
  ACCOUNT: `${environment.baseApi}/account`,
  MENUPOOLS: `${environment.baseApi}/menu-pools`,
  VOTES: `${environment.baseApi}/votes`,
} as const;
