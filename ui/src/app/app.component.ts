import { Component, OnInit } from '@angular/core';
import { UserService } from '@shared/services/user.service';
import { RoleEnum } from 'src/service-proxies/dto/account';
import { AccountService } from 'src/service-proxies/services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit  {
  isLoggedIn = false;
  showAdminBoard = false;
  username?: string;
  role?: RoleEnum;

  constructor(
    private readonly _userService: UserService,
    private readonly _accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.isLoggedIn = this._userService.isLoggedIn;

    if (this.isLoggedIn) {
      const user = this._userService.getUser();
      this.role = user.role;

      this.showAdminBoard = this.role == RoleEnum.Admin;

      this.username = user.username;
    }
  }
}
