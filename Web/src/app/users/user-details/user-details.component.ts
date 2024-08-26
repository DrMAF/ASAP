import { Component, Input } from '@angular/core';
import { User } from '../../../shared/models/user.model';
import { UserService } from '../../../shared/services/user.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { NgFor, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-details',
  standalone: true,
  imports: [UserDetailsComponent, FormsModule, RouterModule, NgIf, NgFor],
  templateUrl: './user-details.component.html',
  styleUrl: './user-details.component.css'
})
export class UserDetailsComponent {
  @Input() viewMode = false;
  @Input() itemsCount = 0;

  
  @Input() currentUser: User = {
    id: 0,
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: ""
  };

  message = "";
  constructor(private userService: UserService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    if (!this.viewMode) {
      this.message = "";
      this.getUser(this.route.snapshot.params["id"]);
    }
  }

  getUser(id: string): void {
    this.userService.getById(id).subscribe({
      next: (data) => {
        this.currentUser = data;
      },
      error: (e) => console.error(e)
    });
  }

  onDeleteUser() {
    let res = confirm("Are you sure you to delete this user?");

    if (res) {
      this.deleteUser();
    }
  }

  onCancel() {
    let res = confirm("Any modification will be lost. Are you sure?");

    if (res) {
      this.router.navigate(['/users']);
    }
  }


  updateUser(): void {
    this.message = "";

    this.userService.update(this.currentUser.id, this.currentUser).subscribe({
      next: (res) => {
        this.message = "The user was updated successfully.";

        alert("User updated successfully.");
        this.router.navigate(['/users']);

        //setInterval(() => {

        //}, 1000);
      },
      error: (e) => {
        console.error(e);
        this.message = e.message;
      }
    });
  }

  deleteUser(): void {
    this.userService.delete(this.currentUser.id).subscribe({
      next: (res) => {
        alert("User deleted.");

        this.router.navigate(['/users']);
      },
      error: (e) => console.error(e)
    });
  }

}
