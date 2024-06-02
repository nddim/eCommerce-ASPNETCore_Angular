import {KorisnickiRacun} from "./korisnickiRacun";

export interface AutentifikacijaToken {
  id: number
  vrijednost: string
  korisnickiRacunId: number
  korisnickiRacun: KorisnickiRacun
  vrijemeEvidentiranja: string
  ipAdresa: string
}
