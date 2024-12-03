export class Menu {
  id: string | null;
  components: number[] | null;
  menuPoolId: string | null;
  constructor(id: string | null, components: number[] | null, menuPoolId: string | null) {
    this.id = id;
    this.components = components;
    this.menuPoolId = menuPoolId;
  }
}
