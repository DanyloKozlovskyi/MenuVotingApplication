import { Component, ElementRef, Renderer2, ViewChild, AfterViewInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account/account.service';
import { RegisterUser } from 'src/app/models/register-user/register-user';
import { CommonModule } from '@angular/common';
import { Restaurant } from 'src/app/models/restaurant/restaurant';
import { RestaurantService } from 'src/app/services/restaurant/restaurant.service';


@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements AfterViewInit {
  registerForm: FormGroup;
  isRegisterFormSubmitted: boolean = false;
  isRegisterValid: boolean = true;
  restaurants: Restaurant[] = [];
  selectedRestaurant: Restaurant | null = null;

  constructor(private accountService: AccountService, private restaurantService: RestaurantService, private router: Router, private renderer: Renderer2) {
    //this.renderer.listen('document', 'click', () => {
    //  this.hideList();
    //});

    this.registerForm = new FormGroup({
      personName: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [Validators.required, Validators.email]),
      phoneNumber: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required]),
      confirmPassword: new FormControl(null, [Validators.required]),
      restaurants: new FormArray([]),
      restaurantId: new FormControl(null, [Validators.required]),
      isAdmin: new FormControl(false, [Validators.required])
    });
    this.fillRestaurants();
  }

  ngAfterViewInit() {
    this.renderer.listen('document', 'click', () => {
      this.hideList();
    });
  }

  @ViewChild('restaurantList', { static: false }) restaurantList!: ElementRef;
  @ViewChild('restaurantInput', { static: false }) restaurantInput!: ElementRef;

  showList() {
    const restaurantList = this.restaurantList.nativeElement;
    restaurantList.classList.remove('hidden');
  }

  hideList() {
    const restaurantList = this.restaurantList.nativeElement;
    restaurantList.classList.add('hidden');
  }

  selectRestaurant(restaurant: Restaurant) {
    this.selectedRestaurant = restaurant;
    this.restaurantInput.nativeElement.value = restaurant.name + " " + restaurant.address;
    // to use setValue() explicitly cast object to FormControl
    this.registerRestaurantIdControl.setValue(restaurant.id);
    this.hideList();
  }

  preventClose(event: MouseEvent) {
    event.stopPropagation();
  }
  
  public fillRestaurants() {
    this.restaurantService.getRestaurants().subscribe({
      next: (response: Restaurant[]) => {
        this.registerRestaurantsFormArray.clear();
        response.forEach(r => {
          this.registerRestaurantsFormArray.push(new FormControl(r));
        });
        this.restaurants.length = 0;
        response.forEach(r => {
          this.restaurants.push(r);
        });
      },
      error: (error) => {
        console.log(error);
      },
      complete: () => {

      }
    });
  }

  get registerNameControl(): FormControl {
    return this.registerForm.controls['personName'] as FormControl;
  }
  get registerEmailControl(): FormControl {
    return this.registerForm.controls['email'] as FormControl;
  }
  get registerPhoneControl(): FormControl {
    return this.registerForm.controls['phoneNumber'] as FormControl;
  }
  get registerPasswordControl(): FormControl {
    return this.registerForm.controls['password'] as FormControl;
  }
  get registerConfirmPasswordControl(): FormControl {
    return this.registerForm.controls['confirmPassword'] as FormControl;
  }
  get registerRestaurantsFormArray(): FormArray {
    return this.registerForm.get("restaurants") as FormArray;
  }
  get registerRestaurantIdControl(): FormControl {
    return this.registerForm.controls['restaurantId'] as FormControl;
  }
  get registerRoleControl(): FormControl {
    return this.registerForm.controls['role'] as FormControl;
  }

  registerSubmitted() {
    this.isRegisterFormSubmitted = true;
    if (this.registerForm.valid) {
      this.accountService.postRegister(this.registerForm.value).subscribe({
        next: (response: any) => {
          this.isRegisterValid = true;
          this.accountService.currentToken = response.email;
          this.isRegisterFormSubmitted = false;
          localStorage["token"] = response.token;
          localStorage["refreshToken"] = response.refreshToken;

          this.router.navigate(['/menu-voting']);

          this.registerForm.reset();
        },
        error: (error: any) => {
          console.log('incorrect response');
          this.isRegisterValid = false;
          console.log(error);
        },
        complete: () => { }
      });
    }
  }
}
