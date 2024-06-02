import {Injectable} from "@angular/core";
// import * as signalR from "@microsoft/signalr";
import { HubConnectionBuilder } from "@microsoft/signalr";
import {MojConfig} from "../../../assets/moj-config";
import {DataServiceNavbar} from "../../navbar/data-service-navbar";
import {AuthJwtHelper} from "../../Auth/AuthJwtHelper";
import {SnackBar} from "../mat-snackbar/snack-bar";
@Injectable({providedIn:'root'})
export class SignalRService {
  public connectionId: string="";
  constructor(private dataService:DataServiceNavbar,
              private jwt: AuthJwtHelper,
              private snackBar: SnackBar) {
  }
  otvori_ws_konekciju(){

    let konekcija = new HubConnectionBuilder()
      .withUrl(MojConfig.adresa_local+'hub-putanja/')
      .build();

    konekcija.on("narudzba-posalji-notifikaciju", (p) => {
      this.snackBar.openSnackBarPlaviSignal("Vasa narudzba je uspjesno azrurirana, molimo provjerite stanje narudzbe.", "Uđi u narudzbe");

    });

    konekcija.start().then(()=>{
      this.connectionId=konekcija.connectionId ?? "";
      this.dodajKonekciju();
    });

  }
  public async dodajKonekciju() {
    try {
      const result = await this.dataService.addKonekciju(this.jwt.getId(), this.connectionId).toPromise();
      // console.log(this.jwt.getId(), this.connectionId);
      // console.log("Konekcija dodana", result);
    } catch (error) {
      // console.log(this.jwt.getId(), this.connectionId);
      console.error("Error while adding connection:", error);
    }
  }
}
