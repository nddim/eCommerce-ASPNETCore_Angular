import {KategorijaResponse} from "./data-service-kategorija";

export interface KategorijaGetAllResponse {
  id: number
  naziv: string
  nazivGlavneKategorije: string
  glavnaKategorijaID:number
}
export interface KategorijaPaged{
  kategorije:KategorijaGetAllResponse[],
  currentPage: number
  totalPages: number
  pageSize: number
  totalCount: number
}
export interface KategorijaPostRequest{
  naziv: string
  glavnaKategorijaID: number
}

export interface KategorijaFind{
  naziv: string
}
export interface KategorijePaged {
  kategorije: KategorijaGetAllResponse[]
  currentPage: number
  totalPages: number
  pageSize: number
  totalCount: number
}
