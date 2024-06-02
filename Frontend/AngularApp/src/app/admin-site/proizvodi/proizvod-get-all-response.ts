export interface ProizvodGetAll{
  proizvodi:ProizvodGetAllResponse[],
  currentPage:number,
  totalPages:number,
  pageSize:number,
  totalCount:number
}
export interface ProizvodGetAllResponse {
  id: number
  naziv: string
  pocetnaKolicina: number
  pocetnaCijena: number
  opis: string
  brojKlikova: number
  potkategorijaNaziv: string
  potkategorijaID: number
  brendNaziv: string
  brendID: number
  slikaUrl :string
  popust: number
  slikeGalerija:string[]
}

export interface ProizvodGetAllResponseList{
  proizvodi:ProizvodGetAllPretragaObject[],
  min:number,
  max:number,
  currentPage:number,
  totalPages:number,
  pageSize:number,
  totalCount:number
}

export interface ProizvodGetAllPretragaObject {
  id: number
  naziv: string
  pocetnaKolicina: number
  pocetnaCijena: number
  opis: string
  brojKlikova: number
  potkategorijaNaziv: string
  potkategorijaID: number
  brendNaziv: string
  brendID: number
  slikaUrl :string
  popust:number
}
export interface ProizvodPostRequest{
  naziv: string
  pocetnaKolicina: number
  pocetnaCijena: number
  opis: string
  brojKlikova: number
  potkategorijaID: number
  brendID: number
  popust: number

}
