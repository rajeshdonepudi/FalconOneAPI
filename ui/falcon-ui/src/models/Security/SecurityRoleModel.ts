export interface SecurityRole {
  id: string;
  name: string | null;
  normalizedName: string | null;
  modifiedOn: string | null;
  createdOn: string;
  usersInRole: number;
}
