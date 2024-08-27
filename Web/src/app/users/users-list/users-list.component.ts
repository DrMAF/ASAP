import { Component, OnInit } from '@angular/core';
import { PaginatedResult, User } from '../../../shared/models/user.model';
import { UserService } from '../../../shared/services/user.service';
import { FormsModule } from '@angular/forms';
import { NgFor, NgIf } from '@angular/common';
import { UserDetailsComponent } from '../user-details/user-details.component';
import { Observable } from 'rxjs/internal/Observable';
import { GridDataResult, PageChangeEvent, PagerPosition, PagerType } from '@progress/kendo-angular-grid';
import { KENDO_GRID, RowArgs, SelectableSettings, ColumnComponent } from '@progress/kendo-angular-grid';
import { KENDO_POPUP } from '@progress/kendo-angular-popup';
import { KENDO_BUTTONS } from "@progress/kendo-angular-buttons";
import { LabelModule } from '@progress/kendo-angular-label';
import { KENDO_DROPDOWNS } from '@progress/kendo-angular-dropdowns';
import { KENDO_INPUTS } from '@progress/kendo-angular-inputs';

@Component({
  selector: 'app-users-list',
  standalone: true,
  imports: [UserDetailsComponent, FormsModule, NgIf, NgFor,
    KENDO_GRID, KENDO_POPUP, ColumnComponent, KENDO_BUTTONS, LabelModule,
    KENDO_DROPDOWNS, KENDO_INPUTS],
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.css'
})
export class UsersListComponent implements OnInit {

  //paginatedUsers: PaginatedResult<User> = {
  //  items: [], pageIndex: 1, hasNextPage: false,
  //  hasPreviousPage: false, totalPages: 0
  //};

  currentUser: User = {
    id: 0,
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: ""
  };

  itemsCount: number = 0;
  userIndex = -1;
  search: string = "";

  gridView: unknown[] = [];
  pageSize: number = 5;
  skip: number = 0;
  pageIndex: number = 1;
  buttonCount = 3;
  info = true;
  pageSizes = [2, 5, 10];
  previousNext = true;
  position: PagerPosition = "both";
  pagerTypes = ["numeric", "input"];
  type: PagerType = "numeric";

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.retrieveUsers();
  }

  retrieveUsers(): void {
    this.userService.getAll(this.search, 1, 10).subscribe({
      next: (data: PaginatedResult<User>) => {
        //this.paginatedUsers = data;
        this.gridView = data?.items;
        this.itemsCount = data?.items?.length;
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
  }

  showDetails(event: any, dataItem: User) {
    this.currentUser = dataItem;
  }

  //loadItems(): void {
  //  console.log();
  //  this.gridView = this.paginatedUsers.items.slice(this.skip, this.skip + this.pageSize);
  //}
}
