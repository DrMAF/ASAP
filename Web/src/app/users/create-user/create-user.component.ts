import { Component } from '@angular/core';
import { User } from '../../../shared/models/user.model';
import { UserService } from '../../../shared/services/user.service';

@Component({
  selector: 'app-create-user',
  standalone: true,
  imports: [],
  templateUrl: './create-user.component.html',
  styleUrl: './create-user.component.css'
})
export class CreateUserComponent {

  user: User = {
    id: 0,
    firstName: "",
    lastName: "",
    email: "",
    phone: ""
  };

  submitted = false;

  constructor(private userService: UserService) { }

  saveUser(): void {
    const data = {
      firstName: this.user.firstName,
      lastName: this.user.lastName,
      email: this.user.email,
      phone: this.user.phone
    };

    this.userService.create(data).subscribe({
      next: (res) => {
        console.log(res);
        this.submitted = true;
      },
      error: (e) => console.error(e)
    });
  }

  restUser(): void {
    this.submitted = false;

    this.user = {
      id: 0,
      firstName: "",
      lastName: "",
      email: "",
      phone: ""
    };
  }

}
