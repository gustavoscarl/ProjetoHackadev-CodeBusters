import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse,
} from '@angular/common/http';
import { AuthService } from '../../auth.service';
import { Observable, catchError, switchMap, throwError } from 'rxjs';
import { Router } from '@angular/router';


@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService, private route: Router) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const token = this.authService.getToken();

    if (token) {
      const cloned = req.clone({
        withCredentials: true,
        setHeaders: {
          'Authorization': `Bearer ${token}`,
        },
      });
      console.log('Intercepting!');
      return next.handle(cloned).pipe(
        catchError((err:any) =>{
          if (err instanceof HttpErrorResponse && err.status === 401 ){
            
              this.handleAuthError(req, next)
            
          }
          return throwError(()=> new Error("Algum erro aconteceu"))
        })
      );;
    } else {
      console.log('Not intercepting!');
      return next.handle(req)
    }
  }
  handleAuthError(req: HttpRequest<any>, next: HttpHandler){
    const accessToken = this.authService.getToken();
    const refreshToken = this.authService.getRefreshToken();
    return this.authService.renovarToken(accessToken).pipe(
      
      switchMap((data:string)=>{
        console.log(data);
        this.authService.guardarToken(data);
        req = req.clone({
          setHeaders: {Authorization: `Bearer ${data}`}
        })
        return next.handle(req);
      }),
      catchError((err)=>{
        return throwError(()=>{
          console.log(err);
          this.route.navigate(['login'])
        })
      })
    )
  }
}
