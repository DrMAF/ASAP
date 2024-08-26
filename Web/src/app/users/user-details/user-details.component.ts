import { Component, EventEmitter, Input, Output } from '@angular/core';
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
  @Output() changeViewMode = new EventEmitter<boolean>();

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

  onChangeViewMode(): void {
    this.message = "";

    this.changeViewMode.emit(false);
  }

  getUser(id: string): void {
    this.userService.getById(id).subscribe({
      next: (data) => {
        this.currentUser = data;
      },
      error: (e) => console.error(e)
    });
  }

  updateUser(): void {
    this.message = "";

    this.userService.update(this.currentUser.id, this.currentUser).subscribe({
      next: (res) => {
        this.message = "The user was updated successfully.";
        this.changeViewMode.emit(true);

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
        this.router.navigate(['/users']);
      },
      error: (e) => console.error(e)
    });
  }

}
