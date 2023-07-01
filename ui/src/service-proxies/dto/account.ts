export interface ILoginDto {
  email: string;
  password: string;
}

export interface IRegisterDto {
  email: string;
  username: string;
  password: string;
}

export interface IUserInfo {
  id: string;
  email: string;
  username: string;
  role: RoleEnum;
}

export enum RoleEnum {
  Admin = 0,
  User = 1,
}
