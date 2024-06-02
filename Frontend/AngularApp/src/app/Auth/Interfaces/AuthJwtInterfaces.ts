export interface AuthJwtLoginRequest{
  email:string,
  lozinka:string,
  connectionID:string
}
export interface AuthJwtLogin2faRequest{
  email:string,
  key:string
}
export interface AuthJwtLoginResponse{
  token:string,
  expiration:Date,
  status :string,
  message :string,
  success:boolean,
  refreshToken:string
}

export interface AuthJwtRegisterResponse{
  status :string,
  message :string,
  success:boolean
}
export interface AuthJwtRegisterRequest{
  ime:string,
  prezime:string,
  email:string,
  lozinka:string,
  lozinkaPotvrdi:string
}
