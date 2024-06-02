import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {ActivatedRoute, Router} from "@angular/router";
import {DataServiceAktivacija, RegisterActivateRequest} from "./DataServiceAktivacija";
import {error} from "@angular/compiler-cli/src/transformers/util";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-activate-registration',
  standalone: true,
  imports: [CommonModule],
  providers:[DataServiceAktivacija],
  templateUrl: './activate-registration.component.html',
  styleUrl: './activate-registration.component.css'
})
export class ActivateRegistrationComponent implements OnInit{
   activatedCode:string="";
  constructor(private route: ActivatedRoute, private dataService:DataServiceAktivacija,
              private router:Router,
              private toastr:ToastrService) {
    this.route.params.subscribe((params)=> {
      this.activatedCode = params['activationCode'];
    })
  }

  ngOnInit(): void {
    var kod:RegisterActivateRequest={activateCode:this.activatedCode};
    this.dataService.postActivationCode('auth/activate', kod).subscribe(x=>{
    this.router.navigate(['login']);
    },error=>{
      this.toastr.error("Greška sa aktivacijom!");
      this.router.navigate(['']);
      }
    )
  }


}
