import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {NavbarComponent} from "../navbar/navbar.component";
import {Router, RouterLink, RouterLinkActive, RouterOutlet} from "@angular/router";
import {MyAuthService} from "../helper/auth/MyAuthService";
import {FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {RegisterRacun} from "./RegisterRacun";
import {RegisterPostResponse, RegistracijaDataService} from "./Registracija-data-service";
import {error} from "@angular/compiler-cli/src/transformers/util";
import {ToastrService} from "ngx-toastr";
import {AuthjwtService} from "../Auth/Services/authjwt.service";
import {AuthJwtDataService} from "../Auth/AuthJwtDataService";
import {AuthJwtRegisterRequest} from "../Auth/Interfaces/AuthJwtInterfaces";
import {FooterComponent} from "../footer/footer.component";

@Component({
  selector: 'app-register-forma',
  standalone: true,
  imports: [CommonModule, NavbarComponent, RouterOutlet, FormsModule, ReactiveFormsModule, RouterLink, RouterLinkActive, FooterComponent],
  providers:[MyAuthService, RegistracijaDataService],
  templateUrl: './register-forma.component.html',
  styleUrl: './register-forma.component.css'
})
export class RegisterFormaComponent implements  OnInit {
  constructor(private formBuilder:FormBuilder, private dataService:RegistracijaDataService,
              private router:Router, private toastr:ToastrService, private jwt:AuthJwtDataService) {
  }

  ngOnInit(): void {
    this.registerForm=this.formBuilder.group({
      ime:['', [Validators.required, Validators.minLength(3), Validators.maxLength(40)]],
      prezime:['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      email:['', [Validators.required, Validators.minLength(3), Validators.maxLength(50), Validators.email]],
      lozinka:['', [Validators.required]], //Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")] za sifru
      potvrdiLozinku:['', [Validators.required]] //Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")] za sifru
    })
  }


  public racun: RegisterRacun = {
    lozinka:"",
    ime:"",
    email:"",
    prezime:"",
    potvrdiLozinku:""
  };

  registerForm!:FormGroup
  submitted=false;


  private _ime="";
  private _prezime="";
  private _email="";
  private _lozinka="";
  private _potvrdiLozinku="";
  private response:any;
  registerUser() {
    this.submitted=true;
    if(this.registerForm.invalid){
      return;
    }
    if(this._lozinka!=this._potvrdiLozinku){
      this.toastr.error("Nisu iste lozinke!");
    }
  this.dataService.registerAccount({
    ime:this.registerForm.get('ime')?.value,
    prezime:this.registerForm.get('prezime')?.value,
    email:this.registerForm.get('email')?.value,
    lozinka:this.registerForm.get('lozinka')?.value,
    potvrdiLozinku:this.registerForm.get('potvrdiLozinku')?.value,
  }).subscribe(x=>{
    this.response=x;
      alert("Otvorite mail za aktivaciju računa.");

    if(this.response.uredu){
      this.router.navigate(["/login"]);
    }
  },
    error=>alert("Greska sa registracijom, ponovite registraciju!")
    )
  }

  registerUserJwt(){
    this.submitted=true;
    if(this.registerForm.invalid){
      return;
    }
    if(this._lozinka!=this._potvrdiLozinku){
      this.toastr.error("Nisu iste lozinke!", "Greška");
    }
    let data:AuthJwtRegisterRequest={
      ime:this.registerForm.get('ime')?.value,
      prezime:this.registerForm.get('prezime')?.value,
      email:this.registerForm.get('email')?.value,
      lozinka:this.registerForm.get('lozinka')?.value,
      lozinkaPotvrdi:this.registerForm.get('potvrdiLozinku')?.value
    };
    this.jwt.register(data).subscribe({next:(response)=>{
      if(response.success){
        this.toastr.success(response.message, response.status);
        this.router.navigateByUrl("/login");
      }
      else{
        this.toastr.error(response.message, response.status);
        this.router.navigateByUrl("");

      }
      }, error:(err)=>{
        this.toastr.error(JSON.parse(JSON.stringify(err.error)), "Greška");
        this.router.navigateByUrl("/login");

      }});
  }
}
