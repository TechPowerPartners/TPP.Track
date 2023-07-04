import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '@shared/services/user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  username: string = '';

  constructor(
    public readonly _userService: UserService,
    private readonly _router: Router
  ) {}

  ngOnInit(): void {
    this.username = this._userService.getUser().userName;
  }

  public logout(): void {
    this._userService.clear();
    this._router.navigate(['/login']);
  }
}
