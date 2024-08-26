import { Routes } from '@angular/router';
import { UsersListComponent } from './users/users-list/users-list.component';
import { CreateUserComponent } from './users/create-user/create-user.component';
import { UserDetailsComponent } from './users/user-details/user-details.component';

export const routes: Routes = [
  { path: "", redirectTo: "users", pathMatch: "full" },
  { path: "users", component: UsersListComponent },
  { path: 'users/:id', component: UserDetailsComponent },
  { path: "create", component: CreateUserComponent }
];
