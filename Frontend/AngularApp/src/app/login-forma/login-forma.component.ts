import {Component, ElementRef, Inject, OnInit, ViewChild} from '@angular/core';
import { CommonModule } from '@angular/common';
import {NavbarComponent} from "../navbar/navbar.component";
import {AuthLoginRequest} from "../helper/auth/authLoginRequest";
import {Router, RouterLink, RouterLinkActive} from "@angular/router";
import {MyAuthService} from "../helper/auth/MyAuthService";
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {GetAuth} from "../helper/auth/get-auth";
import {ToastrService} from "ngx-toastr";
import {AuthJwtDataService} from "../Auth/AuthJwtDataService";
import {AuthJwtLogin2faRequest, AuthJwtLoginRequest, AuthJwtLoginResponse} from "../Auth/Interfaces/AuthJwtInterfaces";
import {AuthJwtHelper} from "../Auth/AuthJwtHelper";
import {MatCardModule} from "@angular/material/card";
import {
  GoogleLoginProvider,
  GoogleSigninButtonModule,
  SocialAuthService,
} from "@abacritt/angularx-social-login";
import {GoogleAuthDto, GoogleDataService} from "../Auth/GoogleAuth/GoogleDataService";
import {SignalRService} from "../helper/signal-r/signal-r.service";
import {FooterComponent} from "../footer/footer.component";

@Component({
  selector: 'app-login-forma',
  standalone: true,
  imports: [CommonModule, NavbarComponent, FormsModule, RouterLink, RouterLinkActive, ReactiveFormsModule, MatCardModule, GoogleSigninButtonModule, FooterComponent],
  providers:[MyAuthService, GetAuth],
  templateUrl: './login-forma.component.html',
  styleUrl: './login-forma.component.css'
})
export class LoginFormaComponent implements OnInit{

  @ViewChild('2fakey', {static:false}) fakey:ElementRef;
  public loginRequest:AuthLoginRequest={
    email:"",
    lozinka:""
  };
  constructor(private router:Router,
              private myAuthService:MyAuthService,
              private getAuth:GetAuth,
              private formBuilder:FormBuilder,
              private toastr:ToastrService,
              private jwt:AuthJwtDataService,
              private jwtHelper:AuthJwtHelper,
              private googleAuthService:SocialAuthService,
              private googleDataService:GoogleDataService,
              private signalRService:SignalRService) {
    this.fakey=new ElementRef(null);
  }
  registerForm!:FormGroup
  submitted=false;
  focusInput(){
    this.fakey.nativeElement.focus();
  }
  loginGoogle=false;
  ngOnInit(): void {
    this.registerForm=this.formBuilder.group({
      email:['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      lozinka:['', [Validators.required]] //Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")] za sifru
    })

    this.googleAuthService.signOut();
    if(!this.jwtHelper.isLoggedIn() ) //&& this.ctr>=1
    {
      this.googleAuthService.authState.subscribe({next:(result)=>{
          let data:GoogleAuthDto={
            idToken:result.idToken,
            provider:result.provider
          }
          this.googleDataService.externalLogin(data).subscribe((x)=>{
            if(x.token){
              this.toastr.success("Sve uredu prošlo", "Dobar login");
              this.jwtHelper.storeJwtToken(x.token);
              this.jwtHelper.storeRefreshToken(x.refreshToken);
              var panel = this.jwtHelper.getPanel();
              this.router.navigateByUrl(panel);
            }
          })
        },
        error: (err)=>{
          console.log(JSON.parse(JSON.stringify(err.error)));
        }})
    }
    else{
    }

    this.fakey.nativeElement.focus();
  }

  _2fa=false;
  key="";

  signInJwt(){
    this.loginRequest.email=this.registerForm.get('email')?.value;
    this.loginRequest.lozinka= this.registerForm.get('lozinka')?.value;
    let signInData:AuthJwtLoginRequest={
      email:this.loginRequest.email,
      lozinka:this.loginRequest.lozinka,
      connectionID:this.signalRService.connectionId
    };

    this.jwt.login(signInData).subscribe({next:(response)=>{
        if(response.status!="2fa")
        {
          this.toastr.success("Sve uredu prošlo", "Dobar login");
          var panel = this.jwtHelper.getPanel();
          this.router.navigateByUrl(panel);
        }
        else{
          this._2fa=true;
          this.toastr.success("Dobar login, otvorite mail i unesite 2fa kod", "Info");
          this.focusInput();
        }
      }, error:(err)=>{
        this.toastr.error(JSON.parse(JSON.stringify(err.error)), "Greška");

        // this.toastr.error("Greška sa prija", "Error");
      }});
  }
  unesi2fajwt(){
    let obj:AuthJwtLogin2faRequest={
      email:this.loginRequest.email,
      key:this.key
    };

    this.jwt.login2fa(obj).subscribe({
      next:(obj)=>{
        if(obj.success){
          this.toastr.success("Sve uredu prošlo", "Dobar login");

          var panel = this.jwtHelper.getPanel();
          this.router.navigateByUrl(panel);
        }
      }
    })


  }

  async onEnter($event: any) {
    this.submitted = true;
    if (this.registerForm.invalid) {
      return;
    }
    await this.signInJwt();
  }

  logOut() {
    this.myAuthService.signOut();
    this.router.navigate(["/login"])
  }

  forgotPassword=false;
  emailForgot="";
    novaSifra() {
    this.jwt.forgotPassword(this.emailForgot).subscribe(x=>{
      this.toastr.success("Novi password je uspješno poslat na mail", "Uspjeh");
    },error => {
      this.toastr.error(JSON.parse(JSON.stringify(error.error)), "Greška")
    })
    this.forgotPassword=false;
  }
}
