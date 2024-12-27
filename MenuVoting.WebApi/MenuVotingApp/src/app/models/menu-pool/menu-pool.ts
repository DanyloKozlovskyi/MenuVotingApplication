import { Menu } from "../menu/menu";

export class MenuPool {
  id: string | null;
  restaurantId: string | null;
  menus: Menu[];
  constructor(id: string | null, menus: Menu[], restaurantId: string | null) {
    this.id = id;
    this.menus = menus;
    this.restaurantId = restaurantId;
  }
}
