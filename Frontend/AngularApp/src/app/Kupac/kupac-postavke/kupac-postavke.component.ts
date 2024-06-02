import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MyAuthService} from "../../helper/auth/MyAuthService";
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {RegistracijaDataService} from "../../register-forma/Registracija-data-service";
import {Router} from "@angular/router";
import {AuthGetDetail, UpdateKorisnickiRacun} from "./update-korisnicki-racun";
import {EditPostRequest} from "./edit-post-request";
import {ToastrService} from "ngx-toastr";
import {NavbarComponent} from "../../navbar/navbar.component";
import {FooterComponent} from "../../footer/footer.component";

@Component({
  selector: 'app-kupac-postavke',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, NavbarComponent, FooterComponent],
  providers:[MyAuthService, UpdateKorisnickiRacun],
  templateUrl: './kupac-postavke.component.html',
  styleUrl: './kupac-postavke.component.css'
})
export class KupacPostavkeComponent implements OnInit {
  constructor(private myAuth: MyAuthService, private formBuilder: FormBuilder,
              private dataService: UpdateKorisnickiRacun,
              private toastr:ToastrService) {
  }

  registerForm!: FormGroup;
  submitted = false;

  ngOnInit() {
    // this.registerForm = this.formBuilder.group({
    //   ime: ['', Validators.required, Validators.minLength(3), Validators.maxLength(40)],
    //   prezime: ['', Validators.required, Validators.minLength(3), Validators.maxLength(50)],
    //   // email: ['', Validators.required, Validators.minLength(3), Validators.maxLength(50), Validators.email],
    //   broj: ['', Validators.maxLength(20)], //Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")] za sifru
    //   adresa: ['', Validators.maxLength(150)], //Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")] za sifru
    //   saljiNovosti: ['', ] //Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")] za sifru
    // });
    this.getInfo();




    //this.saljiNovosti=this.myAuth.getAuthorizationToken()!.korisnickiRacun.saljiNovosti;

    //this.ucitajPodatkeOKorisniku();
  }

  user:AuthGetDetail={
    ime:"",
    adresa:"",
    email:"",
    prezime:"",
    brojTelefona:"",
    saljiNovosti:false
  };
  getInfo(){
    this.dataService.getAccountInfo().subscribe(x=>{
      this.user=x;

    })
  }




//   private response: EditPostRequest={
//     ime:"",
//     email:"",
//     prezime:"",
//     adresa:"",
//     brojTelefona:"",
//     saljiNovosti:false
// };
  saljiNovosti: boolean=false;

  updateUser() {
    //const controls = this.registerForm.controls;

    // this.submitted = true;
    // if (this.registerForm.invalid) {
    //   return;
    // }
    if(!this.user.ime || !this.user.prezime
    )
    {
      this.toastr.error("Nedozvoljene vrijednosti!");
      return;
    }
    this.dataService.updateAccount(this.user).subscribe((x:any) => {
        //this.response = x;

      //this.myAuth.updateKupac(x as EditPostRequest);
      this.toastr.success("Uspješno modifikovan račun", "Obavijest");
        //this.ucitajPodatkeOKorisniku();
      }
    );


    //   this.dataService.registerAccount({
    //     ime:this.registerForm.get('ime')?.value,
    //     prezime:this.registerForm.get('prezime')?.value,
    //     email:this.registerForm.get('email')?.value,
    //     lozinka:this.registerForm.get('lozinka')?.value,
    //   }).subscribe(x=>{
    //       this.response=x;
    //       alert("Otvorite mail za aktivaciju računa.");
    //
    //       if(this.response.uredu){
    //         this.router.navigate(["/login"]);
    //       }
    //     },
    //     error=>alert("Greska sa registracijom, ponovite registraciju!")
    //   )
    // }


  }

  onEnter($event: any) {
    this.updateUser();
  }
}
