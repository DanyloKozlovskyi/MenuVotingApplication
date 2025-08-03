import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/core/services';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  loginForm: FormGroup;
  isLoginFormSubmitted: boolean = false;
  isLoginValid: boolean = true;

  constructor(private accountService: AccountService, private router: Router) {
    this.loginForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, [Validators.required]),
    });
  }

  get login_emailControl(): any {
    return this.loginForm.controls['email'];
  }
  get login_passwordControl(): any {
    return this.loginForm.controls['password'];
  }

  loginSubmitted() {
    this.isLoginFormSubmitted = true;

    if (this.loginForm.invalid) {
      return;
    }

    this.accountService.login(this.loginForm.value).subscribe({
      next: () => {
        this.isLoginValid = true;
        this.isLoginFormSubmitted = false;
        this.loginForm.reset();
        this.router.navigate(['/menu-voting']);
      },
      error: err => {
        console.error('Login failed', err);
        this.isLoginValid = false;
      }
    });
  }
}
