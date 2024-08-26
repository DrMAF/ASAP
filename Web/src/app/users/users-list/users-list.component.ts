import { Component, OnInit } from '@angular/core';
import { PaginatedResult, User } from '../../../shared/models/user.model';
import { UserService } from '../../../shared/services/user.service';
import { FormsModule } from '@angular/forms';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-users-list',
  standalone: true,
  imports: [FormsModule, NgFor],
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
    phone: ""
  };

  userIndex = -1;
  title = "";

  search: string = "";
  pageIndex: number = 1;
  pageSize: number = 10;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.retrieveUsers();
  }

  retrieveUsers(): void {
    this.userService.getAll(this.search, this.pageIndex, this.pageSize).subscribe({
      next: (data) => {
        this.paginatedUsers = data;
        console.log(this.paginatedUsers);
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
      phone: ""
    };

    this.userIndex = -1;
  }

  setActiveUser(user: User, index: number): void {
    this.currentUser = user;
    this.userIndex = index;
  }

}
