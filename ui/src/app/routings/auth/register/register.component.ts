import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '@shared/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { catchError, EMPTY } from 'rxjs';
import { AccountService } from 'src/service-proxies/services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  public form: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    userName: new FormControl('', [
      Validators.required,
      Validators.min(3),
      Validators.max(30),
    ]),
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
      .register(this.form.getRawValue())
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
