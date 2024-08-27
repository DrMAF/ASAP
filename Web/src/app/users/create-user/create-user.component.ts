import { Component } from '@angular/core';
import { User } from '../../../shared/models/user.model';
import { UserService } from '../../../shared/services/user.service';
import { FormsModule } from '@angular/forms';
import { NgFor, NgIf } from '@angular/common';
import { KENDO_INPUTS } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { KENDO_BUTTONS } from "@progress/kendo-angular-buttons";
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-create-user',
  standalone: true,
  imports: [FormsModule, NgIf, NgFor, KENDO_INPUTS, LabelModule, KENDO_BUTTONS],
  templateUrl: './create-user.component.html',
  styleUrl: './create-user.component.css'
})

export class CreateUserComponent {

  user: User = {
    id: 0,
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: ""
  };

  editMode = false;
  submitted = false;
  error = "";
  constructor(private userService: UserService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    let id = this.route.snapshot.params["id"];

    if (id) {
      this.editMode = true;
      this.getUser(this.route.snapshot.params["id"]);
    }
  }

  saveUser() {
    let validRes = this.validateForrm();

    if (!validRes) {
      return;
    }

    if (this.editMode) {
      this.updateUser();
    }
    else {
      this.createUser();
    }
  }

  createUser() {
    this.userService.create(this.user).subscribe({
      next: (res) => {
        this.submitted = true;
        this.error = "";
      },
      error: (e) => {
        console.log(e);
        console.error(e);
      }
    });
  }

  updateUser() {
    this.userService.update(this.user.id, this.user).subscribe({
      next: (res) => {
        //this.message = "The user was updated successfully.";
        this.submitted = true;
        this.error = "";
        //alert("User updated successfully.");
        //this.router.navigate(["/users"]);
      },
      error: (e) => {
        console.error(e);
        console.error(e);
      }
    });
  }

  restUser(): void {
    this.submitted = false;

    this.user = {
      id: 0,
      firstName: "",
      lastName: "",
      email: "",
      phoneNumber: ""
    };
  }

  getUser(id: string): void {
    this.userService.getById(id).subscribe({
      next: (data) => {
        this.user = data;
      },
      error: (e) => console.error(e)
    });
  }

  validateForrm(): boolean {
    if (!this.user.firstName
      || !this.user.lastName
      || !this.user.email
      || !this.user.phoneNumber) {
      this.error = "All fields are requied."

      return false;
    }

    const emailRegex = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);

    if (!emailRegex.test(this.user.email)) {
      this.error = "Email is not valid.";

      return false;
    }

    const phoneRegex = new RegExp(/^0\d{7,12}$/);

    if (!phoneRegex.test(this.user.phoneNumber)) {
      this.error = "Phone number is not valid.";

      return false;
    }

    return true;
  }

  onDeleteUser() {
    let res = confirm("Are you sure you to delete this user?");

    if (res) {
      this.deleteUser();
    }
  }

  deleteUser(): void {
    this.userService.delete(this.user.id).subscribe({
      next: (res) => {
        alert("User deleted.");

        this.router.navigate(["/users"]);
      },
      error: (e) => console.error(e)
    });
  }

  onCancel() {
    let res = confirm("Any modification will be lost. Are you sure?");

    if (res) {
      this.router.navigate(["/users"]);
    }
  }

  goToList() {
    this.router.navigate(["/users"]);
  }
}
