import MenuItem from "@models/Common/MenuItem";

export interface GroupedMenuItem {
  groupId: number;
  groupName: string;
  icon: JSX.Element;
  items: MenuItem[];
}
