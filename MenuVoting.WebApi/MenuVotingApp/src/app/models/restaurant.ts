export class Restaurant {
  id: string | null;
  name: string | null;
  address: string | null;
  constructor(id: string | null, name: string | null, address: string | null) {
    this.id = id;
    this.name = name;
    this.address = address;
  }
}
