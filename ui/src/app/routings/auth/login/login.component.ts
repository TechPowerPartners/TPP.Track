import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/service-proxies/services/account.service';
import { UserService } from '@shared/services/user.service';
import { Router } from '@angular/router';
import { EMPTY, catchError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
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
    private readonly _userService: UserService,
    private readonly _router: Router,
    private readonly _toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    if (this._userService.isLoggedIn) {
      this._router.navigate(['/home']);
    }
  }

  public onSubmit(): void {
    this._accountService
      .login(this.loginForm.getRawValue())
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this._toastrService.error(error.error);
          return EMPTY;
        })
      )
      .subscribe((token: any) => {
        this._userService.saveToken(token);
        this._accountService.getUserInfo().subscribe((user) => {
          this._userService.saveUser(user);
          this._router.navigate(['/home']);
        });
      });
  }
}
