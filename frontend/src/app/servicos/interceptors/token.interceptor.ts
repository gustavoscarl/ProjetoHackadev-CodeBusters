import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse,
  HttpClient,
} from '@angular/common/http';
import { AuthService } from '../../auth.service';
import { Observable, catchError, map, switchMap, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { CookieService } from 'ngx-cookie-service';
import { TokenApiModel } from '../../modelos/token-api.model';
import { response } from 'express';


@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService, private route: Router, private cookie:CookieService, private http: HttpClient) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const token = this.authService.getToken();
    const decodedToken = jwtDecode(token);
    const tokenExpirationTime = decodedToken && decodedToken.exp ? decodedToken.exp * 1000 : 0;
    const currentTime = Date.now();
    const timeRemaining = tokenExpirationTime - currentTime;
    const isAboutToExpire = timeRemaining <= 3000;

    const cloned = request.clone({
      setHeaders: {
        'Authorization': `Bearer ${token}`,
      },
      withCredentials: true,
    })

    if (isAboutToExpire) {
      this.http.post('http://localhost:5062/auth/refresh', {}).subscribe({
      next: (data: any) => {
        console.log('Data refreshed', data)
        localStorage.setItem('token',data.accessToken)
      },
      error: (error) => console.error('Error refreshing data', error)
    });
    }

    return next.handle(cloned)
    
  }
}