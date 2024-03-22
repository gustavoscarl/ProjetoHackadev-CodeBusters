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

    if (!token) {
      return next.handle(request); // Sem token, prossegue com a requisição normalmente
    }

    if(token) {
      const cloned = request.clone({
        setHeaders: {
          'Authorization': `Bearer ${token}`,
        },
        withCredentials: true,
      })
      return next.handle(cloned)
    }

    const decodedToken: any = jwtDecode(token);
    const tokenExpirationTime = decodedToken && decodedToken.exp ? decodedToken.exp * 1000 : 0;
    const currentTime = Date.now();
    const timeRemaining = tokenExpirationTime - currentTime;
    const isAboutToExpire = timeRemaining <= 3000;

    if (isAboutToExpire) {
      return this.authService.renewToken().pipe(
        switchMap((data: TokenApiModel) => {
          this.authService.guardarToken(data.accessToken);
          const clonedReq = request.clone({
            setHeaders: {
              Authorization: `Bearer ${data.accessToken}`,
            },
            withCredentials: true,
          });
          return next.handle(clonedReq);
        }),
        catchError((error) => {
          console.error('Error refreshing token:', error);
          this.authService.logout();
          return throwError('Token renewal failed');
        })
      );
    }

    // Se o token não está próximo da expiração, prossegue com a requisição normalmente
    return next.handle(request);
  }
}