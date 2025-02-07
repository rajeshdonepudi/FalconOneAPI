import AppLazyLoader from "@/components/ui-components/AppLazyLoader";
import {
  useGetSecurityGroupInfoQuery,
  useGetSecurityGroupPermissionsQuery,
} from "@/services/Security/SecurityGroupService";
import NavUtilities from "@/utilities/NavUtilities";
import {
  Button,
  Card,
  CardContent,
  Grid,
  Skeleton,
  Stack,
  Typography,
} from "@mui/material";
import { useNavigate, useSearchParams } from "react-router-dom";
import ArrowBackOutlinedIcon from "@mui/icons-material/ArrowBackOutlined";
import DateTimeUtilities from "@/utilities/DateTimeUtilities";
import ViewSecurityGroupUsers from "./ViewSecurityGroupUsers";
import AppAccordion from "@/components/ui-components/AppAccordion";
import AppConstants from "@/constants/constants";

const ViewSecurityGroupDetails = () => {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const { data: securityGroupInfo, isLoading: isSecurityGroupInfoLoading } =
    useGetSecurityGroupInfoQuery(String(searchParams.get("groupId")));
  const { data: userPermissions } = useGetSecurityGroupPermissionsQuery(
    String(searchParams.get("groupId"))
  );
  return (
    <>
      <AppLazyLoader>
        <Stack gap={1.5}>
          <Grid container>
            <Grid item md={12}>
              <Stack
                direction={"row"}
                flexWrap={"wrap"}
                justifyContent={"space-between"}
                alignItems="center"
                sx={{ marginBottom: "0.5rem" }}
              >
                <Typography variant="h6">Security Group Details</Typography>
                {isSecurityGroupInfoLoading ? (
                  <Skeleton height={60} width={100} />
                ) : (
                  <Button
                    onClick={() =>
                      navigate(
                        NavUtilities.ToSecureArea("security/security-groups")
                      )
                    }
                    startIcon={<ArrowBackOutlinedIcon />}
                  >
                    Back to Security Groups
                  </Button>
                )}
              </Stack>
            </Grid>
            <Grid item md={12}>
              <Card variant="outlined">
                <CardContent>
                  <Grid container spacing={AppConstants.layout.StandardSpacing}>
                    <Grid item md={2} xs={12} sm={12}>
                      <Stack>
                        {isSecurityGroupInfoLoading ? (
                          <Skeleton height={"20px"} width={"100px"} />
                        ) : (
                          <Typography variant="caption">Name</Typography>
                        )}
                        {isSecurityGroupInfoLoading ? (
                          <Skeleton height={"30px"} width={"100px"} />
                        ) : (
                          <Typography variant="body2">
                            {securityGroupInfo?.data.name}
                          </Typography>
                        )}
                      </Stack>
                    </Grid>
                    <Grid item md={2} xs={12} sm={12}>
                      <Stack>
                        {isSecurityGroupInfoLoading ? (
                          <Skeleton height={"20px"} width={"100px"} />
                        ) : (
                          <Typography variant="caption">Created On</Typography>
                        )}
                        {isSecurityGroupInfoLoading ? (
                          <Skeleton height={"30px"} width={"100px"} />
                        ) : (
                          <Typography variant="body2">
                            {DateTimeUtilities.toLocalDate(
                              securityGroupInfo?.data.createdOn
                            )}
                            <br></br>(
                            {DateTimeUtilities.toRelativeTime(
                              securityGroupInfo?.data.createdOn
                            )}
                            )
                          </Typography>
                        )}
                      </Stack>
                    </Grid>

                    <Grid item md={2} xs={12} sm={12}>
                      <Stack>
                        {isSecurityGroupInfoLoading ? (
                          <Skeleton height={"20px"} width={"100px"} />
                        ) : (
                          <Typography variant="caption">
                            Last Modified
                          </Typography>
                        )}
                        {isSecurityGroupInfoLoading ? (
                          <Skeleton height={"30px"} width={"100px"} />
                        ) : (
                          <Typography variant="body2">
                            {securityGroupInfo?.data.modifiedOn}
                          </Typography>
                        )}
                      </Stack>
                    </Grid>

                    <Grid item md={2} xs={12} sm={12}>
                      <Stack>
                        {isSecurityGroupInfoLoading ? (
                          <Skeleton height={"20px"} width={"100px"} />
                        ) : (
                          <Typography variant="caption">Users</Typography>
                        )}
                        {isSecurityGroupInfoLoading ? (
                          <Skeleton height={"30px"} width={"100px"} />
                        ) : (
                          <Typography variant="body2">
                            {securityGroupInfo?.data.noOfUsers}
                          </Typography>
                        )}
                      </Stack>
                    </Grid>
                    <Grid item md={2} xs={12} sm={12}>
                      <Stack>
                        {isSecurityGroupInfoLoading ? (
                          <Skeleton height={"20px"} width={"100px"} />
                        ) : (
                          <Typography variant="caption">Permissions</Typography>
                        )}
                        {isSecurityGroupInfoLoading ? (
                          <Skeleton height={"30px"} width={"100px"} />
                        ) : (
                          <Typography variant="body2">
                            {securityGroupInfo?.data.noOfPermissions}
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
              <Stack
                direction={"row"}
                flexWrap={"wrap"}
                justifyContent={"space-between"}
                alignItems="center"
                sx={{ marginBottom: "0.5rem" }}
              >
                <Typography variant="h6">Associated Permissions</Typography>
              </Stack>
            </Grid>
            <Grid item md={12}>
              <Card variant="outlined">
                <CardContent>
                  {userPermissions?.data.map((g) => {
                    return (
                      <Stack marginBottom={2}>
                        <AppAccordion
                          title={g.name}
                          content={
                            <Grid
                              container
                              spacing={AppConstants.layout.StandardSpacing}
                            >
                              {g.permissions.map((p) => {
                                return (
                                  <Grid md={4} item>
                                    <Typography variant="body2">{p}</Typography>
                                  </Grid>
                                );
                              })}
                            </Grid>
                          }
                        />
                      </Stack>
                    );
                  })}
                </CardContent>
              </Card>
            </Grid>
          </Grid>
          <Grid container>
            <Grid item md={12}>
              <ViewSecurityGroupUsers />
            </Grid>
          </Grid>
        </Stack>
      </AppLazyLoader>
    </>
  );
};

export default ViewSecurityGroupDetails;
