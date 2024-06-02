import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {Router, RouterLink} from "@angular/router";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {MyAuthService} from "../../helper/auth/MyAuthService";
import {UpdateKorisnickiRacun} from "../kupac-postavke/update-korisnicki-racun";
import {EditPostRequest} from "../kupac-postavke/edit-post-request";
import {UpdateLozinkeRequest} from "./UpdateLozinkeRequest";
import {UpdateLozinkeDataService} from "./UpdateLozinkeDataService";
import {ToastrService} from "ngx-toastr";
import {NavbarComponent} from "../../navbar/navbar.component";
import {FooterComponent} from "../../footer/footer.component";

@Component({
  selector: 'app-reset-lozinke',
  standalone: true,
  imports: [CommonModule, RouterLink, ReactiveFormsModule, NavbarComponent, FooterComponent],
  providers:[MyAuthService, UpdateLozinkeDataService],
  templateUrl: './reset-lozinke.component.html',
  styleUrl: './reset-lozinke.component.css'
})
export class ResetLozinkeComponent {
  constructor(private myAuth: MyAuthService, private formBuilder: FormBuilder,
              private dataService: UpdateLozinkeDataService, private router:Router,
              private toastr:ToastrService) {
  }

  registerForm!: FormGroup
  submitted = false;

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      staraLozinka: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]], //Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")] za sifru
      novaLozinka1: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      novaLozinka2: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
    });


    //this.ucitajPodatkeOKorisniku();
  }



  private response: UpdateLozinkeRequest={
    staraLozinka:"",
    novaLozinka1:"",
    novaLozinka2:""
  };
  saljiNovosti: boolean=false;

  updateLozinku() {

    this.submitted = true;
    if (this.registerForm.invalid) {
      // this.toastr.error("Niste unijeli istu lozinku u potvrdi!");
      // this.resetForma()

      return;
    } else if (this.registerForm.get('novaLozinka1')?.value != this.registerForm.get('novaLozinka2')?.value) {
      this.toastr.error("Niste unijeli istu lozinku u potvrdi!");
      // this.resetForma()

      return;
    }
    this.dataService.updateLozinke({
      staraLozinka : this.registerForm.get('staraLozinka')?.value,
      novaLozinka1 : this.registerForm.get('novaLozinka1')?.value,
      novaLozinka2 : this.registerForm.get('novaLozinka2')?.value
    }).subscribe((x:any) => {

        //this.response = x;

        this.toastr.success("Uspješno promijenjena lozinka!", "Info");
        // this.myAuth.updateKupac(x as EditPostRequest);
        //alert("Uspjesno modifikovan račun");
        //this.router.navigate(["/kupac-panel"])
        //window.location.reload();

        //this.ucitajPodatkeOKorisniku();
      },
      error=>{
        this.toastr.error(JSON.parse(JSON.stringify(error.error)), "Greška");
      // this.toastr.error("Problem sa modifikovanjem lozinke", "Greška");
        this.response.staraLozinka="";
        this.response.novaLozinka2="";
        this.response.novaLozinka1="";
        // window.location.reload();

      }

    );
    this.resetForma()
  }

  onEnter($event: any) {
    this.updateLozinku();
  }

  resetForma(){
    this.registerForm.reset({
      staraLozinka: '',
      novaLozinka1: '',
      novaLozinka2: ''
    });
    this.registerForm.get('staraLozinka')?.clearValidators();
    this.registerForm.get('staraLozinka')?.updateValueAndValidity();
    this.registerForm.get('novaLozinka1')?.clearValidators();
    this.registerForm.get('novaLozinka1')?.updateValueAndValidity();
    this.registerForm.get('novaLozinka2')?.clearValidators();
    this.registerForm.get('novaLozinka2')?.updateValueAndValidity();
  }
}
