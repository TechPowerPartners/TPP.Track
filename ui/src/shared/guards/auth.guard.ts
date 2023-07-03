import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '@shared/services/user.service';

export const AuthGuard: CanActivateFn = (route, state) => {
  const isLoggedIn = inject(UserService).isLoggedIn;
  const router = inject(Router);

  if (!isLoggedIn) {
    router.navigate(['/login']);
  }

  return isLoggedIn;
};
