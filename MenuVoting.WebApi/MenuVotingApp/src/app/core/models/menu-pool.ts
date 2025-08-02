import { Menu } from './menu';

export interface MenuPoolCreate {
  restaurantId: string;
  menus: Menu[];
}

export interface MenuPool extends MenuPoolCreate {
  id: string;
}
