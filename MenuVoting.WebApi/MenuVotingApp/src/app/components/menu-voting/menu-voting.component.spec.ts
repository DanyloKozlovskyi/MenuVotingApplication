import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuVotingComponent } from './menu-voting.component';

describe('MenuVotingComponent', () => {
  let component: MenuVotingComponent;
  let fixture: ComponentFixture<MenuVotingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MenuVotingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MenuVotingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
