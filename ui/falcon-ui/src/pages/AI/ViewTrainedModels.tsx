import AppFileItem from "@/components/ui-components/AppFileItem";
import { useGetAllTrainedModelsQuery } from "@/services/AI/AIService";
import FolderZipOutlinedIcon from "@mui/icons-material/FolderZipOutlined";
import { ListItemText, Stack, Typography } from "@mui/material";
import DateTimeUtilities from "@/utilities/DateTimeUtilities";
import Grid from '@mui/material/Grid2';
import AppConstants from "@/constants/constants";

const ViewTrainedModels = () => {
  const { data: trainedModelsData } = useGetAllTrainedModelsQuery(null);

  return (
    <>
      <Grid container spacing={AppConstants.layout.StandardSpacing}>
        <Grid size="auto">
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
    </>
  );
};

export default ViewTrainedModels;
