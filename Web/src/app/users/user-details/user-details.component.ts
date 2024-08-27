import { Component, Input } from '@angular/core';
import { User } from '../../../shared/models/user.model';
import { UserService } from '../../../shared/services/user.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { NgFor, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { KENDO_BUTTONS } from "@progress/kendo-angular-buttons";
import { LabelModule } from '@progress/kendo-angular-label';
import { KENDO_INPUTS } from '@progress/kendo-angular-inputs';

@Component({
  selector: 'app-user-details',
  standalone: true,
  imports: [UserDetailsComponent, FormsModule, RouterModule, NgIf, NgFor,
    LabelModule, KENDO_BUTTONS, KENDO_INPUTS],
  templateUrl: './user-details.component.html',
  styleUrl: './user-details.component.css'
})

export class UserDetailsComponent {
  @Input() itemsCount = 0;

  @Input() currentUser: User = {
    id: 0,
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: ""
  };

  constructor(private userService: UserService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
  }

  goUpdate() {
    this.router.navigate(["/update/" + this.currentUser.id]);
  }
}
