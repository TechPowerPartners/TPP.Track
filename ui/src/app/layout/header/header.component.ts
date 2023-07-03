import { Component, OnInit } from '@angular/core';
import { UserService } from '@shared/services/user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  username: string = '';

  constructor(public readonly _userService: UserService) {}

  ngOnInit(): void {
    this.username = this._userService.getUser().userName;
    console.log(this._userService.getUser())
  }
}
