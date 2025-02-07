export interface UserLookupModel {
  id: string;
  email: string;
  fullName: string;
  resourceAlias: string;
}

export interface CallerInfoDto extends UserLookupModel {
  connectionId: string;
}
