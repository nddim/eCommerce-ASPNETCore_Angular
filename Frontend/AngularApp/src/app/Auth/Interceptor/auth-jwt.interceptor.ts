import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Inject, inject, Injectable} from "@angular/core";
import {AuthJwtHelper} from "../AuthJwtHelper";
import {catchError, EMPTY, Observable, of, switchMap, tap, throwError} from "rxjs";
import {Router} from "@angular/router";
import {AuthJwtDataService} from "../AuthJwtDataService";
import {ToastrService} from "ngx-toastr";
import {AuthJwtLoginResponse} from "../Interfaces/AuthJwtInterfaces";

@Injectable({providedIn:'root'})
export class AuthJwtInterceptor implements HttpInterceptor {
  constructor(private router: Router,
              private authService: AuthJwtDataService,
              private toastr: ToastrService,
              private helper: AuthJwtHelper) {
  }

  ctr = 0;




  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {


    return next.handle(this.addToken(request)).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401 && !request.url.includes('refresh')) {
          return this.handle401Error(request, next);
        } else {
          return throwError(error);
        }
      })
    );
  }

  private addToken(request: HttpRequest<any>): HttpRequest<any> {
    const token = this.helper.getToken();
    const refreshToken=this.helper.getRefreshToken();

    if (request.url.includes('maps.googleapis.com')) {
      return request;
    } else {
      return request.clone({
        headers: request.headers.set('Authorization', 'Bearer ' + token)
          .append('refreshtoken', refreshToken)
      });
    }

  }

  private handle401Error(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return this.authService.refreshToken().pipe(
      switchMap((tokens: any) => {
        this.helper.storeJwtToken(tokens.token);
        return next.handle(this.addToken(request));
      }),
      catchError((error) => {
        if (error.status === 401) {
          // console.log("catch error");
          this.helper.removeToken();
          this.helper.removeRefreshToken();
          this.router.navigate(['/login']);
        }
        return throwError(error);
      })
    );
  }









}
