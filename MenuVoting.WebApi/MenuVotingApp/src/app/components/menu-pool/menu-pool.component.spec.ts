import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuPoolComponent } from './menu-pool.component';

describe('MenuPoolComponent', () => {
  let component: MenuPoolComponent;
  let fixture: ComponentFixture<MenuPoolComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MenuPoolComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MenuPoolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
