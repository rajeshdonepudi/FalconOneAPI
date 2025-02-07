import { ChangeEvent, lazy, useMemo, useRef, useState } from "react";

import EditOutlinedIcon from "@mui/icons-material/EditOutlined";
import DeleteOutlineOutlinedIcon from "@mui/icons-material/DeleteOutlineOutlined";

const Grid = lazy(() => import("@mui/material/Grid"));
const Stack = lazy(() => import("@mui/material/Stack"));

import Paper from "@mui/material/Paper";
import { useSelector } from "react-redux";
import {
  Avatar,
  IconButton,
  Skeleton,
  Tooltip,
  Typography,
} from "@mui/material";
import AppLazyLoader from "@/components/ui-components/AppLazyLoader";
import AppModal from "@/components/ui-components/AppModal";
import AppImageCropper from "@/components/ui-components/AppImageCropper";
import {
  useGetUserProfileInfoQuery,
  useUpdateProfilePictureMutation,
} from "@/services/User/UserService";
import { AppVisuallyHiddenInput } from "@/components/ui-components/AppVisualllyHiddenInput";
import ImageUtilities from "@/utilities/ImageUtilities";
import AppPage from "@/components/ui-components/AppPage";
import AppConstants from "@/constants/constants";

const UserProfile = () => {
  const [uploadProfilePicture] = useUpdateProfilePictureMutation();
  const [selectedImageBase64, setSelectedImageBase64] = useState<string>("");
  const [modalState, setModalState] = useState({
    visible: false,
  });
  const imagePickerRef = useRef<any>(null);
  const loggedInUser = useSelector((state: any) => state.auth);
  const { data: userInfo, isLoading: isUserInfoLoading } =
    useGetUserProfileInfoQuery(loggedInUser.id);

  const userDetails = useMemo(() => {
    return userInfo?.data;
  }, [userInfo?.data]);

  const photoCropperRef = useRef<any>(null);

  const onFileChange = (event: ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    if (file) {
      const reader = new FileReader();
      reader.readAsArrayBuffer(file);
      reader.onload = async () => {
        const byteArray = new Uint8Array(reader.result as ArrayBuffer);
        setSelectedImageBase64(
          `data:image/png;base64, ${ImageUtilities.convertArrayBufferToBase64(
            byteArray
          )}`
        );
        setModalState(() => {
          return {
            visible: true,
          };
        });
      };
    }
  };

  const onCancelUpdateProfilePicture = () => {
    setModalState(() => {
      return {
        visible: false,
      };
    });
    setSelectedImageBase64("");
  };

  const onProfilePictureConfirmed = async () => {
    const croppedImage = await photoCropperRef?.current?.getCroppedImage();
    const fetchData = async (imageUrl: string) => {
      try {
        const response = await fetch(imageUrl);
        const blob = await response.blob();
        const reader = new FileReader();
        reader.readAsDataURL(blob);
        reader.onloadend = () => {
          uploadProfilePicture({
            userId: loggedInUser.id,
            image: String(reader?.result).split(",")[1],
          })
            .unwrap()
            .then(() => {
              onCancelUpdateProfilePicture();
            });
        };
      } catch (error) {
        console.error("Error fetching image:", error);
      }
    };
    if (croppedImage) {
      await fetchData(croppedImage);
    }
  };

  return (
    <AppPage
      title="Profile"
      content={
        <AppLazyLoader>
          <AppVisuallyHiddenInput
            type="file"
            ref={imagePickerRef}
            onChange={onFileChange}
          />
          <Grid container spacing={0.8}>
            <Grid item md={12} xs={12}>
              <Paper sx={{ padding: "0.8rem" }} variant="outlined">
                <Stack
                  direction={"row"}
                  justifyContent={"center"}
                  alignItems={"center"}
                >
                  <Stack
                    direction={"column"}
                    justifyContent={"center"}
                    alignItems={"center"}
                  >
                    {isUserInfoLoading ? (
                      <Skeleton variant="circular" width={150} height={150} />
                    ) : (
                      <Avatar
                        alt={`${userInfo?.data.firstName} ${userInfo?.data.lastName}`}
                        src={`data:image/gif;base64,${userInfo?.data.avatar}`}
                        sx={{ width: 150, height: 150 }}
                      />
                    )}
                    {isUserInfoLoading ? (
                      <Skeleton variant="rectangular" width={210} height={60} />
                    ) : (
                      <Typography variant="h6">{`${userDetails?.fullName}`}</Typography>
                    )}

                    <Stack direction={"row"}>
                      <Tooltip title="Update profile picture">
                        <IconButton
                          onClick={() => imagePickerRef?.current?.click()}
                        >
                          <EditOutlinedIcon />
                        </IconButton>
                      </Tooltip>

                      <Tooltip title="Remove profile picture">
                        <IconButton>
                          <DeleteOutlineOutlinedIcon />
                        </IconButton>
                      </Tooltip>
                    </Stack>
                  </Stack>
                </Stack>
              </Paper>
            </Grid>
            <Grid item md={12} xs={12}>
              <Paper sx={{ padding: "0.8rem" }} variant="outlined">
                <Grid container spacing={AppConstants.layout.StandardSpacing}>
                  <Grid item md={3} xs={12}>
                    {isUserInfoLoading ? (
                      <Stack spacing={AppConstants.layout.StandardSpacing}>
                        <Skeleton variant="text" width={60} height={20} />
                        <Skeleton variant="text" width={120} height={24} />
                      </Stack>
                    ) : (
                      <Stack>
                        <Typography variant="caption">First Name</Typography>
                        <Typography variant="body2">
                          {userDetails?.firstName}
                        </Typography>
                      </Stack>
                    )}
                  </Grid>

                  <Grid item md={3} xs={12}>
                    {isUserInfoLoading ? (
                      <Stack spacing={AppConstants.layout.StandardSpacing}>
                        <Skeleton variant="text" width={60} height={20} />
                        <Skeleton variant="text" width={120} height={24} />
                      </Stack>
                    ) : (
                      <Stack>
                        <Typography variant="caption">Last Name</Typography>
                        <Typography variant="body2">
                          {userDetails?.lastName}
                        </Typography>
                      </Stack>
                    )}
                  </Grid>

                  <Grid item xs={12}>
                    {isUserInfoLoading ? (
                      <Stack spacing={AppConstants.layout.StandardSpacing}>
                        <Skeleton variant="text" width={40} height={20} />
                        <Skeleton variant="text" width={100} height={24} />
                      </Stack>
                    ) : (
                      <Stack>
                        <Typography variant="caption">Email</Typography>
                        <Typography variant="body2">
                          {userDetails?.email}
                        </Typography>
                      </Stack>
                    )}
                  </Grid>

                  <Grid item md={3} xs={12}>
                    {isUserInfoLoading ? (
                      <Stack spacing={AppConstants.layout.StandardSpacing}>
                        <Skeleton variant="text" width={40} height={20} />
                        <Skeleton variant="text" width={100} height={24} />
                      </Stack>
                    ) : (
                      <Stack>
                        <Typography variant="caption">Phone</Typography>
                        <Typography variant="body2">
                          {userDetails?.phone}
                        </Typography>
                      </Stack>
                    )}
                  </Grid>
                </Grid>
              </Paper>
            </Grid>
          </Grid>
          <AppModal
            modalTitle={"Update profile picture"}
            show={modalState.visible}
            okButtonText={"Update"}
            handleOk={onProfilePictureConfirmed}
            handleClose={onCancelUpdateProfilePicture}
          >
            <Stack>
              <AppImageCropper
                ref={photoCropperRef}
                image={selectedImageBase64}
              />
            </Stack>
          </AppModal>
        </AppLazyLoader>
      }
    />
  );
};

export default UserProfile;
