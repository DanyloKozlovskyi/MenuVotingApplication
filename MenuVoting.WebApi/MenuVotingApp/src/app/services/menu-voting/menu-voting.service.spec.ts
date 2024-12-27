import { TestBed } from '@angular/core/testing';

import { MenuVotingService } from './menu-voting.service';

describe('MenuVotingService', () => {
  let service: MenuVotingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MenuVotingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
