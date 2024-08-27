import { Component, OnInit } from '@angular/core';
import { PaginatedResult, User } from '../../../shared/models/user.model';
import { UserService } from '../../../shared/services/user.service';
import { FormsModule } from '@angular/forms';
import { NgFor, NgIf } from '@angular/common';
import { UserDetailsComponent } from '../user-details/user-details.component';
import { Observable } from 'rxjs/internal/Observable';
import { GridModule, GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';
import { KENDO_GRID, RowArgs, SelectableSettings, ColumnComponent } from '@progress/kendo-angular-grid';
import { KENDO_POPUP } from '@progress/kendo-angular-popup';
import { KENDO_BUTTONS } from "@progress/kendo-angular-buttons";

@Component({
  selector: 'app-users-list',
  standalone: true,
  imports: [UserDetailsComponent, FormsModule, NgIf, NgFor,
    KENDO_GRID, KENDO_POPUP, ColumnComponent, KENDO_BUTTONS],
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

  gridItems: Observable<GridDataResult> | undefined;
  pageSize: number = 10;
  skip: number = 0;
  //public sortDescriptor: SortDescriptor[] = [];
  filterTerm: number | null = null;

  anchorElement: any;
  show = false;
  anchorAlign = {
    horizontal: 'left',
    vertical: 'top'
  }

  selectableSettings: SelectableSettings = {
    cell: true,
  };

  gridData: any[] = [];
  isCellSelected = (
    row: RowArgs,
    column: ColumnComponent,
    colIndex: number
  ): { selected: boolean; item: { itemKey: number; columnKey: number } } => ({
    selected:
      (row.index % 2 && !(colIndex % 2)) || column.field === "ReorderLevel",
    item: {
      itemKey: row.index,
      columnKey: colIndex,
    },
  });

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.retrieveUsers();
  }

  retrieveUsers(): void {
    this.userService.getAll(this.search, this.pageIndex, this.pageSize).subscribe({
      next: (data: PaginatedResult<User>) => {
        this.paginatedUsers = data;

        this.gridData = this.paginatedUsers.items;

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
    this.anchorElement = event.target.closest('tr');
    this.show = true;

    console.log("show", this.show);
  }

  public closePopup() {
    this.show = false;
    this.anchorElement = undefined;
    this.currentUser = new User;
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
