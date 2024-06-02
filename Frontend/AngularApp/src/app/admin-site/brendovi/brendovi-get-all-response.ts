export interface BrendoviGetAllResponse{
  id:number
  naziv:string
}
export interface BrendoviPostRequest{
  naziv:string
}
export interface BrendSearch{
  naziv:string
}
export interface BrendoviGetPaged{
  brendovi:BrendoviGetAllResponse[],
  currentPage: number
  totalPages: number
  pageSize: number
  totalCount: number
}
