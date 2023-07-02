import { Component } from '@angular/core';
import { UserService } from '@shared/services/user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  constructor(public readonly _userService: UserService){}



}
