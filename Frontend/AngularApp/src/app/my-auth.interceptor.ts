import {HTTP_INTERCEPTORS, HttpInterceptorFn} from '@angular/common/http';

export const myAuthInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req);
};

export const httpInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: myAuthInterceptor, multi: true },
];
