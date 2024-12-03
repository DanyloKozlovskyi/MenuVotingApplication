import { Component } from '@angular/core';
import { FormArray, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account/account.service';
import { RegisterUser } from 'src/app/models/register-user';
import { CommonModule } from '@angular/common';
import { Restaurant } from 'src/app/models/restaurant';
import { RestaurantService } from 'src/app/services/restaurant/restaurant.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerForm: FormGroup;
  isRegisterFormSubmitted: boolean = false;
  isRegisterValid: boolean = true;
  restaurants: Restaurant[] = [];

  constructor(private accountService: AccountService, private restaurantService: RestaurantService, private router: Router) {
    this.registerForm = new FormGroup({
      personName: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [Validators.required, Validators.email]),
      phoneNumber: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required]),
      confirmPassword: new FormControl(null, [Validators.required]),
      restaurants: new FormArray([])
    });
    this.fillRestaurants();
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

  get registerNameControl(): any {
    return this.registerForm.controls['personName'];
  }
  get registerEmailControl(): any {
    return this.registerForm.controls['email'];
  }
  get registerPhoneControl(): any {
    return this.registerForm.controls['phoneNumber'];
  }
  get registerPasswordControl(): any {
    return this.registerForm.controls['password'];
  }
  get registerConfirmPasswordControl(): any {
    return this.registerForm.controls['confirmPassword'];
  }
  get registerRestaurantsFormArray(): FormArray {
    return this.registerForm.get("restaurants") as FormArray;
  }

  registerSubmitted() {
    this.isRegisterFormSubmitted = true;
    if (this.registerForm.valid) {

      this.accountService.postRegister(this.registerForm.value).subscribe({
        next: (response: any) => {
          this.isRegisterValid = true;

          this.accountService.currentUserName = response.email;
          this.isRegisterFormSubmitted = false;
          localStorage["token"] = response.token;
          localStorage["refreshToken"] = response.refreshToken;

          this.router.navigate(['/system']);

          this.registerForm.reset();
        },
        error: (error: any) => {
          this.isRegisterValid = false;
          console.log(error);
        },
        complete: () => { }
      });
    }
  }
}
