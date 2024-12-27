export class Menu {
  id: string | null;
  dishes: string[] | null;
  menuPoolId: string | null;
  constructor(id: string | null, dishes: string[] | null, menuPoolId: string | null) {
    this.id = id;
    this.dishes = dishes;
    this.menuPoolId = menuPoolId;
  }
}
