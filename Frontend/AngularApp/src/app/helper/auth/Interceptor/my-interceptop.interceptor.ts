import { HttpInterceptorFn } from '@angular/common/http';

export const myInterceptopInterceptor: HttpInterceptorFn = (req, next) => {
  //console.log(`request is on its way to ${req.url}`)
  return next(req);
};
