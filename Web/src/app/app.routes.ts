import { Routes } from '@angular/router';
import { UsersListComponent } from './users/users-list/users-list.component';
import { CreateUserComponent } from './users/create-user/create-user.component';

export const routes: Routes = [
  { path: '', redirectTo: 'users', pathMatch: 'full' },
  { path: 'users', component: UsersListComponent },
  //{ path: 'users/:id', component: UserDetailsComponent },
  { path: 'add', component: CreateUserComponent }
];
