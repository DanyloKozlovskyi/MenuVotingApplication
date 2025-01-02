import { Component } from '@angular/core';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { Menu } from 'src/app/models/menu/menu';
import { AccountService } from 'src/app/services/account/account.service';
import { MenuVotingService } from 'src/app/services/menu-voting/menu-voting.service';
import { MenuPool } from 'src/app/models/menu-pool/menu-pool';
import { Vote } from 'src/app/models/vote/vote';

@Component({
  selector: 'app-menu-voting',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './menu-voting.component.html',
  styleUrl: './menu-voting.component.css'
})
export class MenuVotingComponent {
  rows: number = 3;
  dishes: string[] = [];
  postMenuForm: FormGroup;
  isPostMenuFormSubmitted: boolean = false;
  postRowsForm: FormGroup;
  isPostDishesSubmitted: boolean = false;
  menus: Menu[] = [];
  putMenuForm: FormGroup;
  loading: boolean = false;
  isCurrentMenuPoolCreated: boolean = false;
  isInputValid: boolean = true;
  editId: string | null = null;
  menupool: MenuPool = new MenuPool(null, [], null);
  selectedVoteIndex: string | null = null;
  constructor(private menuVotingService: MenuVotingService, public accountService: AccountService) {
    this.postMenuForm = new FormGroup({
      dishes: new FormArray([])
    });

    this.postRowsForm = new FormGroup({
      rows: new FormControl(null)
    });

    this.putMenuForm = new FormGroup({
      menus: new FormArray([])
    })
  }

  get postRows_RowsControl(): any {
    return this.postRowsForm.controls['rows'];
  }
  get postDishesFormArray(): FormArray {
    return this.postMenuForm.get("dishes") as FormArray;
  }

  public createMenuPoolSubmitted() {
    const menuPool = new MenuPool(null, [], this.accountService.restaurantId)
    this.menuVotingService.createMenuPool(menuPool).subscribe({
      next: (response: MenuPool) => {
        this.isInputValid = true;
        this.isCurrentMenuPoolCreated = true;
        this.menupool = response;
      },

      error: (error: any) => {
        console.log(error);
        this.isInputValid = false;
        this.isCurrentMenuPoolCreated = false;
      },
      complete: () => { }
    });
  }

  public fillPutMenuForm(menu: Menu) {
    this.putMenuFormArray.push(new FormGroup({
      id: new FormControl(menu.id, [Validators.required]),
      dishes: new FormArray([]),
      menuPoolId: new FormControl(menu.menuPoolId, [Validators.required]),
    }));

    menu.dishes?.forEach((dish) => {
      this.putMenuPool_MenusControl(this.menus.length - 1).push(new FormGroup({
        value: new FormControl(dish, [Validators.required])
      }))
    });
  }

  public postMenuSubmitted() {
    this.isPostMenuFormSubmitted = true;
    this.dishes = [];
    this.loading = true;
    this.isInputValid = true;
    for (let i = 0; i < this.postDishesFormArray.length; i++) {
      if (this.postDishesFormArray.at(i).invalid) {
        this.isInputValid = false;
        return;
      }
    }
    for (let i = 0; i < this.postDishesFormArray.length; i++) {
      let dish = this.postDishesFormArray.at(i).value.value as string;
      this.dishes.push(dish);
    }

    for (let i = 0; i < this.postDishesFormArray.length; i++) {
      this.postDishesFormArray.controls[i].reset(this.postDishesFormArray.controls[i].value);
    }
    let menu = new Menu(null, this.dishes, this.menupool.id);

    this.menuVotingService.postMenu(this.menupool.id, menu).subscribe({
      next: (response: Menu) => {
        this.isInputValid = true;
        this.menus.push(response);
        this.fillPutMenuForm(response);
      },

      error: (error: any) => {
        console.log(error);
        this.isInputValid = false;
      },
      complete: () => { }
    });
  }

  get putMenuFormArray(): FormArray {
    return this.putMenuForm.get("menus") as FormArray;
  }

  loadVote() {
    this.menuVotingService.getCurrentVote(this.menupool.id).subscribe({
      next: (response: Vote) => {
        this.selectedVoteIndex = response.menuId;
      },

      error: (error: any) => {
        console.log(error);
      },

      complete: () => { }
    });
  }

  loadMenus() {
    this.menuVotingService.getCurrentMenuPool().subscribe({
      next: (response: MenuPool) => {
        this.menus = response.menus;
        this.menupool = response;

        this.putMenuFormArray.clear();

        this.isCurrentMenuPoolCreated = true;

        this.menus.forEach(menu => {
          this.putMenuFormArray.push(new FormGroup({
            id: new FormControl(menu.id, [Validators.required]),
            dishes: new FormArray([]),
          }));
        });

        this.menus.forEach((menu: Menu, ind) => {
          menu.dishes?.forEach((dish) => {
            this.putMenuPool_MenusControl(ind).push(new FormGroup({
              value: new FormControl(dish, [Validators.required])
            }));
          });
        });

        this.loadVote();
      },

      error: (error: any) => {
        console.log(error);
        this.isCurrentMenuPoolCreated = false;
      },

      complete: () => { }
    });
  }
  ngOnInit() {
    this.loadMenus();
  }

  public putMenuPool_MenusControl(i: number): FormArray {
    let currentFormGroup = this.putMenuFormArray.controls[i] as FormGroup;
    return currentFormGroup.controls['dishes'] as FormArray;
  }

  public putMenuPool_IdControl(i: number): FormArray {
    let currentFormGroup = this.putMenuFormArray.controls[i] as FormGroup;
    return currentFormGroup.controls['id'] as FormArray;
  }

  public editClicked(menu: Menu, i: number) {
    this.editId = menu.id;

    const dishesControl = this.putMenuPool_MenusControl(i);

    //for (let j = 0; j < dishesControl.length; j++) {
    //  if (dishesControl.at(j).disabled) {
    //    dishesControl.at(j).enable();
    //  } else {
    //    dishesControl.at(j).disable();
    //  }
    //}
  }

  public updateClicked(i: number) {
    let menusToUpdate = [];
    console.log(this.putMenuFormArray.controls[i].value);


    for (var i = 0; i < this.menus.length; i++) {
      let dishesToUpdate = [];
      const menusControl = this.putMenuPool_MenusControl(i);
      for (var j = 0; j < menusControl.length; j++) {
        dishesToUpdate.push(menusControl.at(j).value.value);
      }
      menusToUpdate.push(new Menu(this.putMenuPool_IdControl(i).value, dishesToUpdate, this.menupool.id));
    }

    const menuPoolUpdate = new MenuPool(this.menupool.id, menusToUpdate, this.accountService.restaurantId);
    this.menuVotingService.putMenuPool(this.menupool.id, menuPoolUpdate).subscribe({
      next: (response: string) => {
        this.editId = null;
        this.putMenuForm.reset(this.putMenuForm.value);
      },
      error: (error: any) => {
        console.log(error)
      },
      complete: () => {

      }
    })
  }

  public deleteClicked(menu: Menu, i: number): void {
    if (confirm(`Are you sure to delete this System: ${menu.dishes}?`)) {
      this.menuVotingService.deleteMenu(menu.id).subscribe({
        next: (response: string) => {
          console.log(response);
          //this.editId = null;

          this.putMenuFormArray.removeAt(i);
          this.menus.splice(i, 1);
        },
        error: (error: any) => {
          console.log(error);
        },
        complete: () => { }
      })
    }
  }

  public onVoteChange(index: string | null): void {
    if (this.selectedVoteIndex === index) {
      this.selectedVoteIndex = null;
    } else {
      this.selectedVoteIndex = index;
    }
  }

  public vote(): void {
    let vote = new Vote(null, this.accountService.userId, this.selectedVoteIndex);
    this.menuVotingService.castVote(this.menupool.id, vote).subscribe({
      next: (response: Vote) => {
        this.selectedVoteIndex = response.menuId;
      },
      error: (error: any) => {
        console.log(error);
        this.selectedVoteIndex = null;
      },
      complete: () => { }
    })
  }

  public resizePostForm(): void {
    this.rows = this.postRows_RowsControl.value;

    if (this.dishes.length < this.rows) {
      let length = this.dishes.length;
      for (var i = length; i < this.rows; i++) {
        this.dishes.push('');
      }
    }
    else {
      while (this.dishes.length != this.rows) {
        this.dishes.pop();
      }
    }
    this.postDishesFormArray.clear();
    this.dishes.forEach((dish) => {
      this.postDishesFormArray.push(new FormGroup({
        value: new FormControl(dish, [Validators.required, Validators.pattern("^[a-zA-Z]+$")])
      }));
    });
  }
}
