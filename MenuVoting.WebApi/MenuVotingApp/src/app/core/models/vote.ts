export interface VoteCreate {
  userId: string;
  menuId: string;
}

export interface Vote extends VoteCreate {
  id: string;
}
