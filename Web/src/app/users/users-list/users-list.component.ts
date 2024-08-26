import { Component, OnInit } from '@angular/core';
import { PaginatedResult, User } from '../../../shared/models/user.model';
import { UserService } from '../../../shared/services/user.service';
import { FormsModule } from '@angular/forms';
import { NgFor, NgIf } from '@angular/common';
import { UserDetailsComponent } from '../user-details/user-details.component';

@Component({
  selector: 'app-users-list',
  standalone: true,
  imports: [UserDetailsComponent, FormsModule, NgIf, NgFor],
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.css'
})
export class UsersListComponent implements OnInit {

  paginatedUsers?: PaginatedResult<User>;

  currentUser: User = {
    id: 0,
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: ""
  };

  userIndex = -1;

  search: string = "";
  pageIndex: number = 1;
  pageSize: number = 10;

  viewMode = true;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.retrieveUsers();
  }

  retrieveUsers(): void {
    this.userService.getAll(this.search, this.pageIndex, this.pageSize).subscribe({
      next: (data) => {
        this.paginatedUsers = data;
      },
      error: (e) => console.error(e)
    });
  }

  refreshList(): void {
    this.retrieveUsers();

    this.currentUser = {
      id: 0,
      firstName: "",
      lastName: "",
      email: "",
      phoneNumber: ""
    };

    this.userIndex = -1;
  }

  setActiveUser(user: User, index: number): void {
    this.currentUser = user;
    this.userIndex = index;

    this.viewMode = true;
  }

  switchMode(newViewMode: boolean): void{
    this.viewMode = newViewMode;
  }
}
