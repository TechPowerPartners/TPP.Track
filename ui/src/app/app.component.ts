import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '@shared/services/user.service';
import { takeUntil } from 'rxjs';
import { RoleEnum } from 'src/service-proxies/dto/account';
import { AccountService } from 'src/service-proxies/services/account.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  isLoggedIn = false;
  username?: string;
  role?: RoleEnum;

  constructor(
    private readonly _userService: UserService,
    private readonly _accountService: AccountService,
    private readonly _router: Router
  ) {}

  ngOnInit(): void {
    this._userService.isLoggedIn$.subscribe((isLoggedIn: boolean) => {
      this.isLoggedIn = isLoggedIn;

      if (isLoggedIn) {
        const user = this._userService.getUser();
        this.role = user.role;
        this.username = user.userName;
      } else {
        this._router.navigate(['/login']);
      }
    });
  }
}
