import AppIcon from "@/components/ui-components/AppIcon";
import AppLazyLoader from "@/components/ui-components/AppLazyLoader";
import AppLottieAnimation from "@/components/ui-components/AppLottieAnimation";
import { useTheme } from "@mui/material/styles";
import { AddOutlined, CallOutlined } from "@mui/icons-material";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import { Button, Card, Grid, Stack, TextField } from "@mui/material";
import { useState } from "react";
import NavUtilities from "@/utilities/NavUtilities";
import { useNavigate } from "react-router-dom";

enum MeetingType {
  None = 0,
  CreateMeeting,
  JoinMeeting,
}

const Meeting = () => {
  const [meetingType, setMeetingType] = useState<MeetingType>(MeetingType.None);
  const [meetingName, setMeetingName] = useState<string>("");
  const theme = useTheme();
  const navigate = useNavigate();
  return (
    <AppLazyLoader>
      <Card variant="outlined" sx={{ height: "85vh" }}>
        <Grid container sx={{ height: "100%" }}>
          <Grid
            item
            md={6}
            justifyContent={"center"}
            alignItems={"center"}
            xs={12}
          >
            <AppLottieAnimation
              height="100%"
              width="100%"
              lottieUrl="https://lottie.host/d48a6381-35c5-42e8-be78-22fc5d2ab887/hNaT8NGoY3.json"
            />
          </Grid>
          <Grid item md={6} xs={12}>
            <Stack
              gap={2}
              sx={{
                height: "100%",
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                padding: "1rem",
              }}
            >
              {meetingType === MeetingType.None && (
                <Stack gap={2}>
                  <Button
                    size="large"
                    startIcon={<CallOutlined />}
                    variant="contained"
                    onClick={() => setMeetingType(MeetingType.CreateMeeting)}
                  >
                    NEW MEETING
                  </Button>
                  <Button
                    size="large"
                    startIcon={<AddOutlined />}
                    variant="contained"
                    onClick={() => setMeetingType(MeetingType.JoinMeeting)}
                  >
                    JOIN MEETING
                  </Button>
                </Stack>
              )}
              {meetingType === MeetingType.CreateMeeting && (
                <Stack gap={2} sx={{ width: "100%", alignItems: "center" }}>
                  <Stack gap={1} direction={"row"} sx={{ width: "100%" }}>
                    <TextField
                      fullWidth
                      id="Create meeting"
                      label="Create meeting"
                      variant="outlined"
                      value={meetingName}
                      onChange={(
                        event: React.ChangeEvent<HTMLInputElement>
                      ) => {
                        setMeetingName(event.target.value);
                      }}
                    />
                    <Button
                      onClick={() => {
                        navigate(
                          NavUtilities.ToSecureArea(
                            `meetings/create-join?action=${
                              MeetingType.CreateMeeting
                            }&identifier=${window.crypto.randomUUID()}&name=${meetingName}`
                          )
                        );
                      }}
                      variant="contained"
                    >
                      CREATE
                    </Button>
                  </Stack>
                  <AppIcon
                    onClick={() => setMeetingType(MeetingType.None)}
                    Icon={ArrowBackIcon}
                    color={theme.palette.primary.main}
                  />
                </Stack>
              )}
              {meetingType === MeetingType.JoinMeeting && (
                <Stack gap={2} sx={{ width: "100%", alignItems: "center" }}>
                  <Stack gap={1} direction={"row"} sx={{ width: "100%" }}>
                    <TextField
                      fullWidth
                      id="Create meeting"
                      label="Meeting URL"
                      variant="outlined"
                    />
                    <Button variant="contained">JOIN</Button>
                  </Stack>
                  <AppIcon
                    onClick={() => setMeetingType(MeetingType.None)}
                    Icon={ArrowBackIcon}
                    color={theme.palette.primary.main}
                  />
                </Stack>
              )}
            </Stack>
          </Grid>
        </Grid>
      </Card>
    </AppLazyLoader>
  );
};

export default Meeting;
