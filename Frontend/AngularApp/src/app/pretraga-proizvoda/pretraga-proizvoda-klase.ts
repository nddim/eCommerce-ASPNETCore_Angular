export interface BrendGetAll{
  brendId:number,
  brendNaziv:string,
  brojProizvoda:number,
  isChecked:boolean
}

export interface BrendChecks{
  brendId:number,
  checked:boolean
}

export interface PotkategorijeSidebar{
  potkategorijaID:number,
  potkategorijaNaziv:string
}

export interface SortiranjaResponseGetAll{
sortiranja:SortiranjaResponseList[]
}

export interface SortiranjaResponseList{
  id:number,
  naziv:string
}

export interface ProizvodiSearchResults {
  id: number
  naziv: string
  pocetnaCijena: number
  slikaUrl: string
  relevance: number
  popust:number
}

export interface BrojProizvodaStranica{
  id:number,
  tableSize:number
}
