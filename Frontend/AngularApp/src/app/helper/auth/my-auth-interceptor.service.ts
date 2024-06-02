import {inject, Injectable} from "@angular/core";
import {
  HttpErrorResponse,
  HttpHandler,
  HttpHandlerFn,
  HttpInterceptor,
  HttpInterceptorFn,
  HttpRequest
} from "@angular/common/http";
import {MyAuthService} from "./MyAuthService";
import {Router} from "@angular/router";
import {catchError, tap, throwError} from "rxjs";

@Injectable({providedIn:'root'})  //{providedIn: 'root'}
export class MyAuthInterceptor implements HttpInterceptor {
  constructor(
    private auth: MyAuthService,
    private router: Router) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler) {

    const authToken = this.auth.getAuthorizationToken()?.vrijednost??"";
       const authReq = req.clone({
      headers: req.headers.set('my-auth-token', authToken)
    });

    return next.handle(authReq).pipe(
      tap(()=>{}, err=>{
        if (err instanceof HttpErrorResponse)
        {
          if (err.status !== 401){
            return;
          }
          this.router.navigateByUrl('/login');
        }
      })
    );
  }
}

export const myHttpInterceptor:HttpInterceptorFn=(
  req: HttpRequest<unknown>,
  next: HttpHandlerFn,
)=>{
  //alert("dsadsadsadsadsadsaaaaaaa------------");
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      let errorMsg = "";
      if (error.error instanceof ErrorEvent) {
        //console.log("this is client side error");
        errorMsg = `Client Error: ${error.error.message}`;
      } else {
        //console.log("this is server side error");
        errorMsg = `Server Error Code: ${error.status}, Message: ${error.message}`;
      }
      return throwError(() => errorMsg);
    }),
  );
}
