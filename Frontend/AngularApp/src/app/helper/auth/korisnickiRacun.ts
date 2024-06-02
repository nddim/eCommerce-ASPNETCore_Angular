export interface KorisnickiRacun {
  id: number;
  ime: string;
  prezime: string;
  email: string;
  datumKreiranja: string;
  datumModifikovanja: string | null;
  isKupac: boolean | null;
  isAdmin: boolean | null;
  is2FActive: boolean;
  adresa:string|null;
  brojTelefona:string|null;
  saljiNovosti:boolean;
}
