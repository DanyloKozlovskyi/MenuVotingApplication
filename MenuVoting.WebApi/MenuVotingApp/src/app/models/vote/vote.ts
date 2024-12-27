export class Vote {
  id: string | null;
  userId: string | null;
  menuId: string | null;
  constructor(id: string | null, userId: string | null, menuId: string | null) {
    this.id = id;
    this.userId = userId;
    this.menuId = menuId;
  }
}
