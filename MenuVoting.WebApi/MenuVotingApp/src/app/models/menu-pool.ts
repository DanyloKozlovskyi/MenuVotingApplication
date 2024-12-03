export class MenuPool {
  id: string | null;
  restaurantId: string | null;
  constructor(id: string | null, components: number[] | null, restaurantId: string | null) {
    this.id = id;
    this.restaurantId = restaurantId;
  }
}
