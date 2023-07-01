import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/service-proxies/services/account.service';
import { UserService } from '@shared/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  public loginForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(4),
      Validators.maxLength(30),
    ]),
  });

  constructor(
    private readonly _accountService: AccountService,
    private readonly _userService: UserService
  ) {}

  public onSubmit(): void {
    this._accountService
      .login(this.loginForm.getRawValue())
      .subscribe((token: any) => {
        this._userService
        this._accountService.getUserInfo().subscribe((user) => {
          this._userService.saveUser(user);
        });
      });
  }
}
