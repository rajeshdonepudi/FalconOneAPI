import AppLazyLoader from "@/components/ui-components/AppLazyLoader";
import {
  Avatar,
  Button,
  Card,
  CardContent,
  Chip,
  Divider,
  Grid,
  Skeleton,
  Stack,
  Typography,
} from "@mui/material";
import { useNavigate, useSearchParams } from "react-router-dom";
import {
  useGetUserDetailsQuery,
  useGetUserPermissionsQuery,
  useGetUserRolesQuery,
  useRemoveUserPermissionMutation,
} from "@/services/User/UserService";
import DisplayUtilities from "@/utilities/DisplayUtilities";
import ArrowBackOutlinedIcon from "@mui/icons-material/ArrowBackOutlined";
import NavUtilities from "@/utilities/NavUtilities";
import AppLottieAnimation from "@/components/ui-components/AppLottieAnimation";
import AppAccordion from "@/components/ui-components/AppAccordion";
import { toast } from "react-toastify";
import AppPage from "@/components/ui-components/AppPage";
import AppConstants from "@/constants/constants";

const ViewUserDetails = () => {
  let [searchParams, setSearchParams] = useSearchParams();
  const resourceId = String(searchParams.get("resourceId"));
  const navigate = useNavigate();
  const { data: userDetails, isLoading: isUserDetailsLoading } =
    useGetUserDetailsQuery(String(searchParams.get("resourceId")));
  const { data: userRoles, isLoading: isUserRolesLoading } =
    useGetUserRolesQuery(resourceId);
  const { data: userPermissions, isLoading: isPermissionsLoading } =
    useGetUserPermissionsQuery(resourceId);

  const [removeUserPermission] = useRemoveUserPermissionMutation();

  const onUserPermissionDelete = (permission: string) => {
    removeUserPermission({
      permission: permission,
      resourceId: resourceId,
    })
      .unwrap()
      .then(() => {
        toast("Permission removed successfully.");
      });
  };

  return (
    <AppPage
      title="User Details"
      rightHeaderActions={
        <Stack
          direction={"row"}
          flexWrap={"wrap"}
          justifyContent={"space-between"}
          alignItems="center"
        >
          {isUserDetailsLoading ? (
            <Skeleton height={60} width={100} />
          ) : (
            <Button
              onClick={() => navigate(NavUtilities.ToSecureArea("users"))}
              startIcon={<ArrowBackOutlinedIcon />}
            >
              Back to Users
            </Button>
          )}
        </Stack>
      }
      content={
        <AppLazyLoader>
          <Grid container>
            <Grid item md={12}>
              <Card variant="outlined">
                <CardContent>
                  <Grid container spacing={AppConstants.layout.StandardSpacing}>
                    <Grid item md={12} xs={12} sm={12}>
                      <Stack
                        direction={"column"}
                        justifyContent={"center"}
                        alignItems={"center"}
                      >
                        {isUserDetailsLoading ? (
                          <Skeleton
                            variant="circular"
                            height={"150px"}
                            width={"150px"}
                          />
                        ) : (
                          <Avatar
                            alt={`${userDetails?.data.firstName} ${userDetails?.data.lastName}`}
                            src={`data:image/gif;base64,${userDetails?.data.avatar}`}
                            sx={{ width: 150, height: 150 }}
                          />
                        )}

                        {isUserDetailsLoading ? (
                          <Skeleton height={"60px"} width={"100px"} />
                        ) : (
                          <Typography variant="h6">{`${userDetails?.data.fullName}`}</Typography>
                        )}
                      </Stack>
                    </Grid>
                    <Grid item md={2} xs={12} sm={12}>
                      <Stack>
                        {isUserDetailsLoading ? (
                          <Skeleton height={"20px"} width={"100px"} />
                        ) : (
                          <Typography variant="caption">First Name</Typography>
                        )}
                        {isUserDetailsLoading ? (
                          <Skeleton height={"30px"} width={"100px"} />
                        ) : (
                          <Typography variant="body2">
                            {userDetails?.data.firstName}
                          </Typography>
                        )}
                      </Stack>
                    </Grid>
                    <Grid item md={2} xs={12} sm={12}>
                      <Stack>
                        {isUserDetailsLoading ? (
                          <Skeleton height={"20px"} width={"100px"} />
                        ) : (
                          <Typography variant="caption">Last Name</Typography>
                        )}
                        {isUserDetailsLoading ? (
                          <Skeleton height={"30px"} width={"100px"} />
                        ) : (
                          <Typography variant="body2">
                            {userDetails?.data.lastName}
                          </Typography>
                        )}
                      </Stack>
                    </Grid>
                    <Grid item md={2} xs={12} sm={12}>
                      <Stack>
                        {isUserDetailsLoading ? (
                          <Skeleton height={"20px"} width={"100px"} />
                        ) : (
                          <Typography variant="caption">Email</Typography>
                        )}
                        {isUserDetailsLoading ? (
                          <Skeleton height={"30px"} width={"100px"} />
                        ) : (
                          <Typography variant="body2">
                            {userDetails?.data.email}
                          </Typography>
                        )}
                      </Stack>
                    </Grid>
                    <Grid item md={2} xs={12} sm={12}>
                      <Stack>
                        {isUserDetailsLoading ? (
                          <Skeleton height={"20px"} width={"100px"} />
                        ) : (
                          <Typography variant="caption">Full Name</Typography>
                        )}
                        {isUserDetailsLoading ? (
                          <Skeleton height={"30px"} width={"100px"} />
                        ) : (
                          <Typography variant="body2">
                            {userDetails?.data.fullName}
                          </Typography>
                        )}
                      </Stack>
                    </Grid>
                    <Grid item md={2} xs={12} sm={12}>
                      <Stack>
                        {isUserDetailsLoading ? (
                          <Skeleton height={"20px"} width={"100px"} />
                        ) : (
                          <Typography variant="caption">Locked</Typography>
                        )}
                        {isUserDetailsLoading ? (
                          <Skeleton height={"30px"} width={"100px"} />
                        ) : (
                          <Typography variant="body2">
                            {DisplayUtilities.formatBoolean(
                              Boolean(userDetails?.data.isLocked)
                            )}
                          </Typography>
                        )}
                      </Stack>
                    </Grid>
                    <Grid item md={2} xs={12} sm={12}>
                      <Stack>
                        {isUserDetailsLoading ? (
                          <Skeleton height={"20px"} width={"100px"} />
                        ) : (
                          <Typography variant="caption">
                            Lockout Enabled
                          </Typography>
                        )}
                        {isUserDetailsLoading ? (
                          <Skeleton height={"30px"} width={"100px"} />
                        ) : (
                          <Typography variant="body2">
                            {DisplayUtilities.formatBoolean(
                              Boolean(userDetails?.data.lockoutEnabled)
                            )}
                          </Typography>
                        )}
                      </Stack>
                    </Grid>
                    <Grid item md={2} xs={12} sm={12}>
                      <Stack>
                        {isUserDetailsLoading ? (
                          <Skeleton height={"20px"} width={"100px"} />
                        ) : (
                          <Typography variant="caption">
                            Email Confirmed
                          </Typography>
                        )}
                        {isUserDetailsLoading ? (
                          <Skeleton height={"30px"} width={"100px"} />
                        ) : (
                          <Typography variant="body2">
                            {DisplayUtilities.formatBoolean(
                              Boolean(userDetails?.data.emailConfirmed)
                            )}
                          </Typography>
                        )}
                      </Stack>
                    </Grid>
                    <Grid item md={2} xs={12} sm={12}>
                      <Stack>
                        {isUserDetailsLoading ? (
                          <Skeleton height={"20px"} width={"100px"} />
                        ) : (
                          <Typography variant="caption">Phone</Typography>
                        )}
                        {isUserDetailsLoading ? (
                          <Skeleton height={"30px"} width={"100px"} />
                        ) : (
                          <Typography variant="body2">
                            {DisplayUtilities.formatPhone(
                              String(userDetails?.data.phone)
                            )}
                          </Typography>
                        )}
                      </Stack>
                    </Grid>
                    <Grid item md={2} xs={12} sm={12}>
                      <Stack>
                        {isUserDetailsLoading ? (
                          <Skeleton height={"20px"} width={"100px"} />
                        ) : (
                          <Typography variant="caption">
                            Phone Confirmed
                          </Typography>
                        )}
                        {isUserDetailsLoading ? (
                          <Skeleton height={"30px"} width={"100px"} />
                        ) : (
                          <Typography variant="body2">
                            {DisplayUtilities.formatBoolean(
                              Boolean(userDetails?.data.phoneNumberConfirmed)
                            )}
                          </Typography>
                        )}
                      </Stack>
                    </Grid>
                    <Grid item md={2} xs={12} sm={12}>
                      <Stack>
                        {isUserDetailsLoading ? (
                          <Skeleton height={"20px"} width={"100px"} />
                        ) : (
                          <Typography variant="caption">
                            Two-factor Enabled
                          </Typography>
                        )}
                        {isUserDetailsLoading ? (
                          <Skeleton height={"30px"} width={"100px"} />
                        ) : (
                          <Typography variant="body2">
                            {DisplayUtilities.formatBoolean(
                              Boolean(userDetails?.data.twoFactorEnabled)
                            )}
                          </Typography>
                        )}
                      </Stack>
                    </Grid>
                    <Grid item md={2} xs={12} sm={12}>
                      <Stack>
                        {isUserDetailsLoading ? (
                          <Skeleton height={"20px"} width={"100px"} />
                        ) : (
                          <Typography variant="caption">
                            Lockout Probability (AI Powered)
                          </Typography>
                        )}
                        {isUserDetailsLoading ? (
                          <Skeleton height={"30px"} width={"100px"} />
                        ) : (
                          <Typography variant="body2">
                            {userDetails?.data.lockoutProbability}
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
              >
                <Typography variant="h6" gutterBottom>
                  Roles
                </Typography>
              </Stack>
            </Grid>
            <Grid item md={12}>
              {isUserRolesLoading ? (
                <Skeleton height={"250px"} />
              ) : (
                <Card variant="outlined">
                  <CardContent>
                    <Grid
                      container
                      spacing={AppConstants.layout.StandardSpacing}
                    >
                      {userRoles?.data.length === 0 && (
                        <Stack sx={{ width: "100%" }} alignItems={"center"}>
                          <AppLottieAnimation lottieUrl="https://lottie.host/ada81a9c-e0c5-4a65-a852-a37d9193ab78/qb4MP1dgQD.json" />
                          <Typography variant="h6">
                            No roles associated with the user.
                          </Typography>
                        </Stack>
                      )}
                      {userRoles?.data.map((r) => (
                        <Grid md={2} item>
                          {
                            <Stack>
                              <Typography variant="caption">Role</Typography>
                              <Typography variant="body2">{r}</Typography>
                            </Stack>
                          }
                        </Grid>
                      ))}
                    </Grid>
                  </CardContent>
                </Card>
              )}
            </Grid>
          </Grid>
          <Grid container>
            <Grid item md={12}>
              <Stack
                direction={"row"}
                flexWrap={"wrap"}
                justifyContent={"space-between"}
                alignItems="center"
              >
                <Typography variant="h6" gutterBottom>
                  Permissions
                </Typography>
              </Stack>
            </Grid>
            <Grid item md={12}>
              {isPermissionsLoading ? (
                <Skeleton height={"300px"} />
              ) : (
                <Card variant="outlined">
                  <CardContent>
                    {userPermissions?.data.length === 0 && (
                      <Stack sx={{ width: "100%" }} alignItems={"center"}>
                        <AppLottieAnimation lottieUrl="https://lottie.host/93429471-a4ab-44d8-ae79-066b00a57eec/QCqhcgFB9h.json" />
                        <Typography variant="h6" gutterBottom>
                          No permissions associated with the user.
                        </Typography>
                      </Stack>
                    )}
                    {userPermissions?.data.map((g) => {
                      return (
                        <Stack marginBottom={2}>
                          <AppAccordion
                            id={g.name}
                            showCustomTitle={true}
                            renderTitle={<Divider>{g.name}</Divider>}
                            content={
                              <Grid
                                container
                                spacing={AppConstants.layout.StandardSpacing}
                              >
                                {g.permissions.map((p) => {
                                  return (
                                    <Grid md={4} item>
                                      <Stack
                                        gap={2}
                                        direction={"row"}
                                        alignItems={"center"}
                                      >
                                        <Chip
                                          label={p}
                                          variant="outlined"
                                          onDelete={() =>
                                            onUserPermissionDelete(p)
                                          }
                                        />
                                      </Stack>
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
              )}
            </Grid>
          </Grid>
        </AppLazyLoader>
      }
    ></AppPage>
  );
};

export default ViewUserDetails;
