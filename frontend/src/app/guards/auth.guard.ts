import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../auth.service';


export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService)
  const token = authService.getToken();
  const router = new Router;
  
  if(token) {
    return true;
  } else
    {
      router.navigate(['login']);
      return false;
    }
  
};
