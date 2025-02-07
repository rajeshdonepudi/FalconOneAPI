import AppLazyLoader from "@/components/ui-components/AppLazyLoader";
import { useGetTenantDetailsQuery } from "@/services/Tenant/TenantService";
import NavUtilities from "@/utilities/NavUtilities";
import {
  Avatar,
  Button,
  Card,
  CardContent,
  Grid,
  IconButton,
  Skeleton,
  Stack,
  Tooltip,
  Typography,
} from "@mui/material";
import { useNavigate, useSearchParams } from "react-router-dom";
import ArrowBackOutlinedIcon from "@mui/icons-material/ArrowBackOutlined";
import DateTimeUtilities from "@/utilities/DateTimeUtilities";
import EditOutlinedIcon from "@mui/icons-material/EditOutlined";
import ViewTenantUsers from "./ViewTenantUsers";
import AppPage from "@/components/ui-components/AppPage";
import AppConstants from "@/constants/constants";
const ViewTenantDetails = () => {
  let [searchParams, setSearchParams] = useSearchParams();
  const navigate = useNavigate();

  const { data: tenantDetails, isLoading: isTenantDetailsLoading } =
    useGetTenantDetailsQuery(String(searchParams.get("accountAlias")));

  return (
    <AppPage
      title="Tenant Details"
      rightHeaderActions={
        <Stack
          direction={"row"}
          flexWrap={"wrap"}
          justifyContent={"space-between"}
          alignItems="center"
        >
          {isTenantDetailsLoading ? (
            <Skeleton height={60} width={100} />
          ) : (
            <Button
              onClick={() =>
                navigate(NavUtilities.ToSecureArea("manage-tenants"))
              }
              startIcon={<ArrowBackOutlinedIcon />}
            >
              Back to Tenants
            </Button>
          )}
        </Stack>
      }
      content={
        <AppLazyLoader>
          <Stack gap={2}>
            <Grid container>
              <Grid item md={12}>
                <Card variant="outlined">
                  <CardContent>
                    <Grid
                      container
                      spacing={AppConstants.layout.StandardSpacing}
                    >
                      <Grid item md={12} xs={12} sm={12}>
                        <Stack alignItems={"end"}>
                          <Tooltip title="Edit user">
                            <IconButton onClick={() => {}} aria-label="Example">
                              <EditOutlinedIcon />
                            </IconButton>
                          </Tooltip>
                        </Stack>
                      </Grid>
                      <Grid item md={12} xs={12} sm={12}>
                        <Stack
                          direction={"column"}
                          justifyContent={"center"}
                          alignItems={"center"}
                        >
                          {isTenantDetailsLoading ? (
                            <Skeleton
                              variant="circular"
                              height={"150px"}
                              width={"150px"}
                            />
                          ) : (
                            <Avatar
                              alt={`${tenantDetails?.data.host}`}
                              src={`data:image/gif;base64,${tenantDetails?.data.profilePicture}`}
                              sx={{ width: 150, height: 150 }}
                            />
                          )}

                          {isTenantDetailsLoading ? (
                            <Skeleton height={"60px"} width={"100px"} />
                          ) : (
                            <Typography variant="h6">{`${tenantDetails?.data.name}`}</Typography>
                          )}
                        </Stack>
                      </Grid>
                      <Grid item md={2} xs={12} sm={12}>
                        <Stack>
                          {isTenantDetailsLoading ? (
                            <Skeleton height={"20px"} width={"100px"} />
                          ) : (
                            <Typography variant="caption">Name</Typography>
                          )}
                          {isTenantDetailsLoading ? (
                            <Skeleton height={"30px"} width={"100px"} />
                          ) : (
                            <Typography variant="body2">
                              {tenantDetails?.data.name}
                            </Typography>
                          )}
                        </Stack>
                      </Grid>
                      <Grid item md={2} xs={12} sm={12}>
                        <Stack>
                          {isTenantDetailsLoading ? (
                            <Skeleton height={"20px"} width={"100px"} />
                          ) : (
                            <Typography variant="caption">Host</Typography>
                          )}
                          {isTenantDetailsLoading ? (
                            <Skeleton height={"30px"} width={"100px"} />
                          ) : (
                            <Typography variant="body2">
                              {tenantDetails?.data.host}
                            </Typography>
                          )}
                        </Stack>
                      </Grid>
                      <Grid item md={2} xs={12} sm={12}>
                        <Stack>
                          {isTenantDetailsLoading ? (
                            <Skeleton height={"20px"} width={"100px"} />
                          ) : (
                            <Typography variant="caption">
                              Account Alias
                            </Typography>
                          )}
                          {isTenantDetailsLoading ? (
                            <Skeleton height={"30px"} width={"100px"} />
                          ) : (
                            <Typography variant="body2">
                              {tenantDetails?.data.accountAlias}
                            </Typography>
                          )}
                        </Stack>
                      </Grid>
                      <Grid item md={2} xs={12} sm={12}>
                        <Stack>
                          {isTenantDetailsLoading ? (
                            <Skeleton height={"20px"} width={"100px"} />
                          ) : (
                            <Typography variant="caption">
                              Created On
                            </Typography>
                          )}
                          {isTenantDetailsLoading ? (
                            <Skeleton height={"30px"} width={"100px"} />
                          ) : (
                            <Typography variant="body2">
                              {DateTimeUtilities.toLocalDate(
                                tenantDetails?.data.createdOn
                              )}{" "}
                              <br />(
                              {DateTimeUtilities.toRelativeTime(
                                tenantDetails?.data.createdOn
                              )}
                              )
                            </Typography>
                          )}
                        </Stack>
                      </Grid>
                      <Grid item md={2} xs={12} sm={12}>
                        <Stack>
                          {isTenantDetailsLoading ? (
                            <Skeleton height={"20px"} width={"100px"} />
                          ) : (
                            <Typography variant="caption">
                              Last Modified
                            </Typography>
                          )}
                          {isTenantDetailsLoading ? (
                            <Skeleton height={"30px"} width={"100px"} />
                          ) : (
                            <Typography variant="body2">
                              {tenantDetails?.data.modifiedOn &&
                                DateTimeUtilities.toLocalDate(
                                  Boolean(tenantDetails?.data.modifiedOn)
                                )}
                              {tenantDetails?.data.modifiedOn &&
                                DateTimeUtilities.toRelativeTime(
                                  tenantDetails?.data.modifiedOn
                                )}
                            </Typography>
                          )}
                        </Stack>
                      </Grid>
                    </Grid>
                  </CardContent>
                </Card>
              </Grid>
            </Grid>
            <Grid container>
              <Grid item md={12}>
                <ViewTenantUsers />
              </Grid>
            </Grid>
          </Stack>
        </AppLazyLoader>
      }
    />
  );
};

export default ViewTenantDetails;
