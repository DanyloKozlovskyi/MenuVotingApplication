export interface MenuCreate {
  dishes: string[];
  menuPoolId: string;
}

export interface Menu extends MenuCreate {
  id: string;
}
