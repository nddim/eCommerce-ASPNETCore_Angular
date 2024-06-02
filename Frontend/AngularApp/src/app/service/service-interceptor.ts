import { Injectable } from "@angular/core";
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import {finalize, Observable} from "rxjs";
import {SpinnerService} from "./spinner-service";
import {AuthJwtHelper} from "../Auth/AuthJwtHelper";

@Injectable({
  providedIn: 'root'
})
export class HttpInterceptorService implements HttpInterceptor {
  constructor(private spinnerService: SpinnerService,
               private auth:AuthJwtHelper) {}

  /*
  API calls always go though interceptor, so it shows spinner and then when call returns if hides
  */
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Proveri da li URL sadrži "signalr"
    const shouldSkipSpinner1 = req.url.includes('add-konekciju');
    const shouldSkipSpinner2 = req.url.includes('hub-putanja');
    const shouldSkipSpinner3 = req.url.includes('signal-r');
    const shouldSkipSpinner4 = req.url.includes('remove-connection');
    const shouldSkipSpinner5 = req.url.includes('proizvod-pretraga-search?Naziv=');
    const isAdmin=this.auth.getRole()==="Admin";

    const specijalno= shouldSkipSpinner1 ||
                               shouldSkipSpinner2 ||
                               shouldSkipSpinner3 ||
                               shouldSkipSpinner4 ||
                               shouldSkipSpinner5 ||
                               isAdmin;

    if (!specijalno) {
      this.spinnerService.show();
    }

    return next.handle(req).pipe(
      finalize(() => {
        if (!specijalno) {
          this.spinnerService.hide();
        }
      })
    );
  }
}
