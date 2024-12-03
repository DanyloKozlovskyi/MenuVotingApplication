import { Routes } from '@angular/router';
import { LoginComponent } from 'src/app/components/login/login.component';
import { RegisterComponent } from 'src/app/components/register/register.component';
import { AppComponent } from 'src/app/app.component';
import { MenuVotingComponent } from 'src/app/components/menu-voting/menu-voting.component';
import { RestaurantComponent } from 'src/app/components/restaurant/restaurant.component';

export const routes: Routes = [
  { path: "menu-voting", component: MenuVotingComponent },
  { path: "restaurant", component: RestaurantComponent },
  { path: "register", component: RegisterComponent },
  { path: "login", component: LoginComponent },
  { path: "logout", component: AppComponent }
];
