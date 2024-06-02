import {ApplicationConfig, importProvidersFrom} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimations } from '@angular/platform-browser/animations';
import {
  HTTP_INTERCEPTORS,
  HttpClientModule,
  provideHttpClient,
  withInterceptors,
  withInterceptorsFromDi
} from "@angular/common/http";
import {provideToastr} from "ngx-toastr";
import {AuthJwtInterceptor} from "./Auth/Interceptor/auth-jwt.interceptor";
import {FacebookLoginProvider, GoogleLoginProvider, SocialAuthServiceConfig} from "@abacritt/angularx-social-login";
import {EnvSecret} from "../assets/googlesecret";
import {HttpInterceptorService} from "./service/service-interceptor";

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    importProvidersFrom(HttpClientModule),
    provideAnimations(),
    provideHttpClient(
      withInterceptorsFromDi(),
    ),

    { provide: HTTP_INTERCEPTORS, useClass: AuthJwtInterceptor, multi: true },
    {provide:HTTP_INTERCEPTORS, useClass:HttpInterceptorService, multi:true},
    provideToastr(),
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              EnvSecret.googleId,{
                oneTapEnabled:false,
                prompt:'consent',
              }
            )
          },
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider('clientId')
          }
        ],
        onError: (err) => {
          console.error(err);
        }
      } as SocialAuthServiceConfig,
    }
  ],
};
