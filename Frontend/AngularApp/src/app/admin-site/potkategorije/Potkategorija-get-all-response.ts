export interface PotkategorijaGetAllResponse {
  id: number
  naziv: string
  kategorijaNaziv: string,
  kategorijaID:number
}

export interface PotkategorijaPaged{
  potkategorije:PotkategorijaGetAllResponse[],
  currentPage: number
  totalPages: number
  pageSize: number
  totalCount: number
}

export interface PotkategorijaPostRequest{
  naziv:string
  kategorijaID:number
}
