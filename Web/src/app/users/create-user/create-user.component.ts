import { Component } from '@angular/core';
import { User } from '../../../shared/models/user.model';
import { UserService } from '../../../shared/services/user.service';
import { FormsModule } from '@angular/forms';
import { NgFor, NgIf } from '@angular/common';
import { KENDO_INPUTS } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { KENDO_BUTTONS } from "@progress/kendo-angular-buttons";

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

  submitted = false;
  error = "";
  constructor(private userService: UserService) { }

  saveUser(): void {
    //const data: User = {
    //  id: 0,
    //  firstName: this.user.firstName,
    //  lastName: this.user.lastName,
    //  email: this.user.email,
    //  phoneNumber: this.user.phoneNumber
    //};

    if (!this.user.firstName
      || !this.user.lastName
      || !this.user.email
      || !this.user.phoneNumber) {
      this.error = "All fields are requied."

      return;
    }

    const emailRegex = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);

    if (!emailRegex.test(this.user.email)) {
      this.error = "Email is not valid.";

      return;
    }

    const phoneRegex = new RegExp(/^0\d{7,12}$/);

    if (!phoneRegex.test(this.user.phoneNumber)) {
      this.error = "Phone number is not valid.";

      return;
    }

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

}
