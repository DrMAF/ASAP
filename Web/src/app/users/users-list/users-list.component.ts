import { Component, OnInit } from '@angular/core';
import { PaginatedResult, User } from '../../../shared/models/user.model';
import { UserService } from '../../../shared/services/user.service';
import { FormsModule } from '@angular/forms';
import { NgFor, NgIf } from '@angular/common';
import { UserDetailsComponent } from '../user-details/user-details.component';
import { Observable } from 'rxjs/internal/Observable';
import { GridModule, GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';

@Component({
  selector: 'app-users-list',
  standalone: true,
  imports: [UserDetailsComponent, FormsModule, NgIf, NgFor],
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.css'
})
export class UsersListComponent implements OnInit {

  paginatedUsers?: PaginatedResult<User>;

  itemsCount: number = 0;

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

  public gridItems: Observable<GridDataResult> | undefined;
  public pageSize: number = 10;
  public skip: number = 0;
  //public sortDescriptor: SortDescriptor[] = [];
  public filterTerm: number | null = null;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.retrieveUsers();
  }

  retrieveUsers(): void {
    this.userService.getAll(this.search, this.pageIndex, this.pageSize).subscribe({
      next: (data: PaginatedResult<User>) => {
        this.paginatedUsers = data;
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

  //public pageChange(event: PageChangeEvent): void {
  //  this.skip = event.skip;
  //  this.loadGridItems();
  //}

  //public handleSortChange(descriptor: SortDescriptor[]): void {
  //  this.sortDescriptor = descriptor;
  //  this.loadGridItems();
  //}

  //private loadGridItems(): void {
  //  this.gridItems = this.userService.getAll(
  //    this.skip,
  //    this.pageSize,
  //    //this.sortDescriptor,
  //    this.filterTerm
  //  );
  //}
}
