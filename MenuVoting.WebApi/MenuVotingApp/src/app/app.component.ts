import { Component, OnInit } from '@angular/core';
import { RouterOutlet, RouterModule, Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { AccountService } from 'src/app/services/account/account.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule, HttpClientModule, ReactiveFormsModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  constructor(public accountService: AccountService, private router: Router) {
    
  }

  ngOnInit(): void {
    if (typeof window !== 'undefined' && localStorage) {
      this.accountService.currentToken = localStorage["token"];
      this.accountService.setUserRole(this.accountService.currentToken);
    }
  }

  logOutClicked() {
    this.accountService.getLogout().subscribe({
      next: (response: string) => {
        this.accountService.currentToken = null;
        this.accountService.isAdmin = false;

        localStorage.removeItem("token");
        localStorage.removeItem("refreshToken");
        this.router.navigate(['/login']);
      },
      error: (error: any) => {
        console.log(error);
      },
      complete: () => {

      }
    });
  }
}
