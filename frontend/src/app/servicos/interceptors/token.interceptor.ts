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
    // const decodedToken: any = jwtDecode(token);
    // const tokenExpirationTime = decodedToken && decodedToken.exp ? decodedToken.exp * 1000 : 0;
    // const currentTime = Date.now();
    // const timeRemaining = tokenExpirationTime - currentTime;
    // const isAboutToExpire = timeRemaining <= 2000;

    if (!token) {
      const cloned = request.clone({
        setHeaders: {
          'Authorization': `Bearer ${token}`,
        },
        withCredentials: true,
      })
      return next.handle(cloned);
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



    // if (token && isAboutToExpire) {
    //   return this.authService.renewToken().pipe(
    //     switchMap((data: any) => {
    //       console.log(data)
    //       this.authService.guardarToken(data);
    //       const clonedReq = request.clone({
    //         setHeaders: {
    //           Authorization: `Bearer ${data.accessToken}`,
    //         },
    //         withCredentials: true,
    //       });
    //       return next.handle(clonedReq);
    //     }),
    //     catchError((error) => {
    //       console.error('Error refreshing token:', error);
    //       this.authService.logout();
    //       return throwError('Token renewal failed');
    //     })
    //   );
    // }

    // Se o token não está próximo da expiração, prossegue com a requisição normalmente
    return next.handle(request);
  }
}