import { Component, ChangeDetectionStrategy, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormArray,
  FormControl,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { BehaviorSubject, combineLatest } from 'rxjs';
import {
  switchMap,
  tap,
  take,
  finalize,
  map,
  exhaustMap,
  catchError,
} from 'rxjs/operators';
import {
  AccountService,
  MenuPoolService,
  MenuService,
  VoteService,
} from 'src/app/core/services';
import { Menu, MenuPool, Vote, MenuPoolCreate } from 'src/app/core/models';
import { CommonModule } from '@angular/common';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

@Component({
  selector: 'app-menu-voting',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ProgressSpinnerModule],
  templateUrl: './menu-voting.component.html',
  styleUrls: ['./menu-voting.component.css'],
  changeDetection: ChangeDetectionStrategy.Default,
})
export class MenuVotingComponent implements OnInit {
  // existing-pool form
  restaurantId!: string;
  menusForm!: FormGroup;
  selectedMenu$ = new BehaviorSubject<string | null>(null);

  // for the "create new menu" flow:
  postRowsForm: FormGroup;
  postMenuForm!: FormGroup;
  rowOptions = [1, 2, 3, 4, 5, 6];
  rows = 3;
  poolExists = false;

  isInputValid = true;
  isPostMenuFormSubmitted = false;
  isLoading = true;

  private votedMenu$ = new BehaviorSubject<string | null>(null);

  canRevote$ = combineLatest([this.votedMenu$, this.selectedMenu$]).pipe(
    map(([original, selected]) => selected !== original && selected != null)
  );

  constructor(
    private fb: FormBuilder,
    private menuPoolService: MenuPoolService,
    private menuService: MenuService,
    private voteService: VoteService,
    public accountService: AccountService
  ) {
    this.postRowsForm = this.fb.group({
      rows: [3, Validators.required],
    });
  }

  ngOnInit() {
    this.menusForm = this.fb.group({
      poolId: [null, Validators.required],
      menus: this.fb.array([]),
    });

    this.buildPostMenuForm();
    this.loadCurrentPool();
    this.readRestaurantId();
  }

  get menusArray(): FormArray<FormGroup> {
    return this.menusForm.get('menus') as FormArray;
  }
  getDishesArray(menuGroup: FormGroup): FormArray {
    return menuGroup.get('dishes') as FormArray;
  }
  getDishesControls(menuGroup: FormGroup) {
    return this.getDishesArray(menuGroup).controls as FormControl[];
  }
  trackByMenu(_i: number, g: FormGroup) {
    return g.get('id')!.value;
  }

  private loadCurrentPool() {
    this.isLoading = true;
    this.menuPoolService
      .getCurrentMenuPool()
      .pipe(
        tap((pool: MenuPool) => {
          this.poolExists = true;
          this.menusForm.patchValue({ poolId: pool.id });
          this.resetMenus(pool.menus);
        }),
        switchMap((pool) =>
          this.voteService.getCurrentVote(pool.id).pipe(
            tap((vote: Vote) => {
              this.selectedMenu$.next(vote.menuId);
              this.votedMenu$.next(vote.menuId);
              console.log(this.votedMenu$.value);
              console.log(this.selectedMenu$.value);
            })
          )
        ),
        finalize(() => (this.isLoading = false))
      )
      .subscribe();
  }

  private readRestaurantId() {
    this.accountService.restaurantId$.pipe(take(1)).subscribe((id) => {
      this.restaurantId = id ?? '';
    });
  }

  onCreatePool() {
    this.isLoading = true;
    this.menuPoolService
      .createMenuPool({
        restaurantId: this.restaurantId,
        menus: [],
      } as MenuPoolCreate)
      .pipe(
        tap((newPool: MenuPool) => {
          this.poolExists = true;
          this.menusForm.patchValue({ poolId: newPool.id });
          this.resetMenus([]);
        }),
        finalize(() => (this.isLoading = false))
      )
      .subscribe();
  }

  private resetMenus(menus: Menu[]) {
    this.menusArray.clear();
    menus.forEach((m) =>
      this.menusArray.push(
        this.fb.group({
          id: [m.id, Validators.required],
          dishes: this.fb.array(
            m.dishes.map((d) =>
              this.fb.control(d, [
                Validators.required,
                Validators.pattern('^[a-zA-Z]+$'),
              ])
            )
          ),
        })
      )
    );
  }

  deleteMenu(i: number) {
    const id = this.menusArray.at(i).get('id')!.value;
    if (!confirm(`Really delete menu #${id}?`)) return;
    this.menuService
      .deleteMenu(id)
      .pipe(tap(() => this.menusArray.removeAt(i)))
      .subscribe();
  }

  selectMenu(menuId: string) {
    this.selectedMenu$.next(
      this.selectedMenu$.value === menuId ? null : menuId
    );
  }

  vote() {
    const poolId = this.menusForm.get('poolId')!.value!;
    const menuId = this.selectedMenu$.value;
    if (!menuId) {
      this.selectedMenu$.next(null);
      return;
    }

    this.accountService.userId$
      .pipe(
        take(1),
        exhaustMap((uid) =>
          this.voteService
            .castVote(poolId, {
              userId: uid!,
              menuId: this.selectedMenu$.value ?? '', // or pass in directly from your UI handler
            })
            .pipe(
              tap((v) => {
                this.selectedMenu$.next(v.menuId);
                this.votedMenu$.next(v.menuId);
              }),

              catchError((err) => {
                this.selectedMenu$.next(null);
                return err;
              })
            )
        )
      )
      .subscribe();
  }

  get isAdmin$() {
    return this.accountService.isAdmin$;
  }

  private buildPostMenuForm() {
    this.rows = this.postRowsForm.value.rows!;
    const arr = Array.from({ length: this.rows }).map(() =>
      this.fb.group({ value: ['', Validators.required] })
    );
    this.postMenuForm = this.fb.group({ dishes: this.fb.array(arr) });
  }

  get postDishesFormArray(): FormArray {
    return this.postMenuForm.get('dishes') as FormArray;
  }

  resizePostForm() {
    this.rows = this.postRowsForm.value.rows!;
    const arr = this.postDishesFormArray;
    while (arr.length < this.rows) {
      arr.push(this.fb.group({ value: ['', Validators.required] }));
    }
    while (arr.length > this.rows) {
      arr.removeAt(arr.length - 1);
    }
  }

  postMenuSubmitted() {
    this.isPostMenuFormSubmitted = true;
    if (this.postMenuForm.invalid) {
      this.isInputValid = false;
      return;
    }
    this.isInputValid = true;

    const dishes = this.postDishesFormArray.value.map((d: any) => d.value);
    const poolId = this.menusForm.get('poolId')!.value!;
    this.menuService
      .postMenu(poolId, { menuPoolId: poolId, dishes })
      .pipe(
        tap((newMenu: Menu) => {
          // append it into the existing list
          this.menusArray.push(
            this.fb.group({
              id: [newMenu.id, Validators.required],
              dishes: this.fb.array(
                newMenu.dishes.map((d) =>
                  this.fb.control(d, [
                    Validators.required,
                    Validators.pattern('^[a-zA-Z]+$'),
                  ])
                )
              ),
            })
          );

          this.postRowsForm.patchValue({ rows: this.rowOptions[0] });
          this.buildPostMenuForm();
          this.isPostMenuFormSubmitted = false;
        })
      )
      .subscribe();
  }
}
