import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {

  const token = localStorage.getItem('token');
  const router = new Router;
  
  if(token) {
    return true;
  } else
    {
      router.navigate(['login']);
      return false;
    }
  
};
