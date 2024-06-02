import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {EditPostRequest} from "../kupac-postavke/edit-post-request";
import {MojConfig} from "../../../assets/moj-config";
import {UpdateLozinkeRequest} from "./UpdateLozinkeRequest";

@Injectable({providedIn:'root'})
export class UpdateLozinkeDataService{
  private readonly apiUrl=MojConfig.adresa_local;

  constructor(private http:HttpClient) {
  }

  updateLozinke(dataToSend:UpdateLozinkeRequest){
    let api=this.apiUrl+'auth/change-password';
    return this.http.post(api, dataToSend);
  }

}
