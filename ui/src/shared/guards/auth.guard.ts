import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '@shared/services/user.service';

export const AuthGuard: CanActivateFn = (route, state) => {
  const userService = inject(UserService);
  const router = inject(Router);
  userService.isLoggedIn$.next(userService.isLoggedIn);
  
  if (!userService.isLoggedIn) {
    router.navigate(['/login']);
  }

  return userService.isLoggedIn;
};
