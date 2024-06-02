import { Injectable } from '@angular/core';
import {MojConfig} from "../../../assets/moj-config";
import {AuthJwtLoginRequest} from "../Interfaces/AuthJwtInterfaces";
import {AuthJwtDataService} from "../AuthJwtDataService";

@Injectable({
  providedIn: 'root'
})
export class AuthjwtService {
  apiUrl=MojConfig.adresa_local;
  constructor(private auth:AuthJwtDataService) { }



}
