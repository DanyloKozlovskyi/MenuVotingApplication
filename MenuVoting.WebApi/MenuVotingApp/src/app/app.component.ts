// app.component.ts
import { Component } from '@angular/core';
import { RouterOutlet, RouterModule, Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AccountService } from 'src/app/core/services';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterModule,
    HttpClientModule,
    ReactiveFormsModule,
    CommonModule,
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  constructor(
    public auth: AccountService,
    private router: Router
  ) {}

  logOutClicked() {
    this.auth.logout().subscribe({
      next: () => this.router.navigate(['/login']),
      error: err => {
        console.error('Logout failed', err);
        this.router.navigate(['/login']);
      }
    });
  }
}
