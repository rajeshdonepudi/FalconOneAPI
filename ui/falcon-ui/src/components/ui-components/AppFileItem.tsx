import { Card, ListItem, ListItemIcon } from "@mui/material";
import FolderZipOutlinedIcon from "@mui/icons-material/FolderZipOutlined";
const AppFileItem = (props: any) => {
  return (
    <Card variant="outlined">
      <ListItem>
        <ListItemIcon>{props?.icon}</ListItemIcon>
        {props.children}
      </ListItem>
    </Card>
  );
};

export default AppFileItem;
