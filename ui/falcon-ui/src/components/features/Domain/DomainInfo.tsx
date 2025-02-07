import AppIcon from "@/components/ui-components/AppIcon";
import AppSimpleValue from "@/components/ui-components/AppSimpleValue";
import AppConstants from "@/constants/constants";
import { DomainWhois } from "@/models/Domain/ViewDomainInfoModel";
import DnsIcon from "@mui/icons-material/Dns";
import {
  Card,
  CardContent,
  CardHeader,
  Divider,
  Grid,
  Skeleton,
  Stack,
  Typography,
  useTheme,
} from "@mui/material";
import { useSelector } from "react-redux";

const DomainInfo = ({
  data,
  isLoading,
}: {
  data: DomainWhois | undefined;
  isLoading: boolean;
}) => {
  const theme = useTheme();
  return (
    <Grid container spacing={0.8}>
      <Grid item md={8} xs={12}>
        <Card sx={{ marginBottom: "0.5rem" }}>
          <CardContent>
            <Typography gutterBottom variant="subtitle2">
              Domain Details
            </Typography>
            <Divider sx={{ margin: "0.2rem" }} />
            <Grid container spacing={AppConstants.layout.StandardSpacing}>
              <Grid item md={6} xs={12}>
                <AppSimpleValue
                  isDataLoading={isLoading}
                  title={"Domain"}
                  value={data?.domain}
                />
              </Grid>
              <Grid item md={6} xs={12}>
                <AppSimpleValue
                  isDataLoading={isLoading}
                  title={"Registrar"}
                  value={data?.status}
                />
              </Grid>
            </Grid>
          </CardContent>
        </Card>
        <Card sx={{ marginBottom: "0.5rem" }}>
          <CardContent>
            <Typography gutterBottom variant="subtitle2">
              Registrar
            </Typography>
            <Divider sx={{ margin: "0.2rem" }} />
            <Grid container spacing={AppConstants.layout.StandardSpacing}>
              <Grid item md={3} xs={12}>
                <AppSimpleValue
                  isDataLoading={isLoading}
                  title={"Name"}
                  value={data?.registrar.name}
                />
              </Grid>
            </Grid>
          </CardContent>
        </Card>
        <Card>
          <CardContent>
            <Typography gutterBottom variant="subtitle2">
              Registrant
            </Typography>
            <Divider sx={{ margin: "0.2rem" }} />
            <Grid container spacing={AppConstants.layout.StandardSpacing}>
              <Grid item md={3} xs={12}>
                <AppSimpleValue
                  isDataLoading={isLoading}
                  title={"Organization"}
                  value={data?.registrant.organization}
                />
              </Grid>
              <Grid item md={3} xs={12}>
                <AppSimpleValue
                  isDataLoading={isLoading}
                  title={"Region"}
                  value={data?.registrant.region}
                />
              </Grid>
              <Grid item md={3} xs={12}>
                <AppSimpleValue
                  isDataLoading={isLoading}
                  title={"Country"}
                  value={data?.registrant.country}
                />
              </Grid>
            </Grid>
          </CardContent>
        </Card>
      </Grid>
      <Grid item md={4} sm={12}>
        <Card>
          <CardContent>
            <Typography gutterBottom variant="subtitle2">
              Nameservers
            </Typography>
            <Divider sx={{ margin: "0.2rem" }} />
            {isLoading
              ? Array.from({ length: 4 }).map((_, index) => (
                  <Stack
                    direction="row"
                    alignItems="center"
                    spacing={AppConstants.layout.StandardSpacing}
                    key={index}
                  >
                    <DnsIcon />
                    <Skeleton variant="text" width={150} />
                  </Stack>
                ))
              : data?.nameservers.map((s, index) => (
                  <Stack
                    margin={"0.5rem"}
                    direction="row"
                    alignItems="center"
                    spacing={AppConstants.layout.StandardSpacing}
                    key={index}
                  >
                    <AppIcon
                      Icon={DnsIcon}
                      color={theme.palette.primary.main}
                    />
                    <Typography variant="subtitle1" gutterBottom>
                      {s}
                    </Typography>
                  </Stack>
                ))}
          </CardContent>
        </Card>
      </Grid>
    </Grid>
  );
};

export default DomainInfo;
