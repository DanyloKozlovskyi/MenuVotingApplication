import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Vote, VoteCreate } from 'src/app/core/models';
import { ENDPOINTS } from './api-endpoints';

@Injectable({
  providedIn: 'root',
})
export class VoteService {
  constructor(private httpClient: HttpClient) {}

  public getCurrentVote(menuPoolId: string): Observable<Vote> {
    const params = new HttpParams().set('menuPoolId', menuPoolId);
    return this.httpClient.get<Vote>(ENDPOINTS.VOTES, { params });
  }

  public castVote(menuPoolId: string, vote: VoteCreate): Observable<Vote> {
    const params = new HttpParams().set('menuPoolId', menuPoolId);
    return this.httpClient.post<Vote>(ENDPOINTS.VOTES, vote, { params });
  }
}
