import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { UsersListComponent } from './users/users-list/users-list.component';
import { bellIcon, menuIcon, SVGIcon } from "@progress/kendo-svg-icons";
import { KENDO_NAVIGATION } from '@progress/kendo-angular-navigation';
import { KENDO_ICONS, KENDO_SVGICON } from '@progress/kendo-angular-icons';
import { moreVerticalIcon, trashIcon } from '@progress/kendo-svg-icons';
import { KENDO_INDICATORS } from '@progress/kendo-angular-indicators';
import { KENDO_LAYOUT } from '@progress/kendo-angular-layout';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, UsersListComponent, RouterModule,
    KENDO_NAVIGATION, KENDO_ICONS, KENDO_SVGICON, KENDO_INDICATORS, KENDO_LAYOUT],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'testProj';
  public menuIcon: SVGIcon = menuIcon;
  public bellIcon: SVGIcon = bellIcon;
  public kendokaAvatar =
    "https://www.telerik.com/kendo-angular-ui-develop/components/navigation/appbar/assets/kendoka-angular.png";
}
