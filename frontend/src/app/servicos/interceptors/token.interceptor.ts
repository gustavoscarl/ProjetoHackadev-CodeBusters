import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse,
} from '@angular/common/http';
import { AuthService } from '../../auth.service';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const token = this.authService.getToken();
    const refresh = this.authService.getRefreshToken();

    const cloned = req.clone({
      setHeaders: {
        'Authorization': `Bearer ${token}`,
      },
      withCredentials: true,
    });

    return next.handle(cloned).pipe(
      catchError((error: any) => {
        if (error instanceof HttpErrorResponse && error.status === 401) {
          return this.authService.renewToken().pipe(
            switchMap((response: any) => {
              this.authService.guardarToken(response.accessToken);
              const updatedReq = req.clone({
                withCredentials:true,
                setHeaders: {
                  'Authorization': `Bearer ${response.accessToken}`,
                },
              });
              return next.handle(updatedReq);
            }),
            catchError((err: any) => {
              console.error('Error renewing token:', err);
              this.authService.logout();
              return throwError('Token renewal failed');
            })
          );
        } else {
          return throwError(error);
        }
      })
    );
  }
}