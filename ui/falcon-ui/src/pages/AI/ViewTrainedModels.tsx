import AppFileItem from "@/components/ui-components/AppFileItem";
import AppLazyLoader from "@/components/ui-components/AppLazyLoader";
import { useGetAllTrainedModelsQuery } from "@/services/AI/AIService";
import { Grid } from "@mui/material";
import FolderZipOutlinedIcon from "@mui/icons-material/FolderZipOutlined";
import { ListItemText, Stack, Typography } from "@mui/material";
import DateTimeUtilities from "@/utilities/DateTimeUtilities";

const ViewTrainedModels = () => {
  const { data: trainedModelsData } = useGetAllTrainedModelsQuery(null);

  return (
    <AppLazyLoader>
      <Grid container spacing={0.8}>
        <Grid item md={3} sm={12} xs={12}>
          {trainedModelsData?.data.map((x) => (
            <AppFileItem icon={<FolderZipOutlinedIcon />}>
              <ListItemText
                primary={x.name}
                secondary={
                  <Stack>
                    <Stack>
                      <Typography variant="caption">Size</Typography>
                      <Typography variant="body2">{x.size}</Typography>
                    </Stack>
                    <Stack>
                      <Typography variant="caption">Trained On</Typography>
                      <Typography variant="body2">
                        {DateTimeUtilities.toLocalDate(x.lastTrained)}
                      </Typography>
                    </Stack>
                  </Stack>
                }
              />
            </AppFileItem>
          ))}
        </Grid>
      </Grid>
    </AppLazyLoader>
  );
};

export default ViewTrainedModels;
