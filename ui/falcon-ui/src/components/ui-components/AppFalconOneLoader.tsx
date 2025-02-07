import { Box, Stack, Typography } from "@mui/material";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";

const AppFalconOneLoader = () => {
  const persistedTheme = useSelector((state: any) => state.theme.siteTheme);

  return (
    <Stack
      alignItems="center"
      justifyContent={"center"}
      sx={{ width: "100vw", height: "100vh" }}
    >
      <Box
        sx={{
          maxHeight: "200px",
          maxWidth: "220px",
          height: "150px",
          width: "150px",
        }}
      >
        <Stack alignItems={"center"}>
          <lottie-player
            src={
              "https://lottie.host/0dfd3d7e-da1b-494c-bd19-b857cd702336/qYSpvwtc0h.json"
            }
            background="transparent"
            speed="1"
            loop
            autoplay
          ></lottie-player>
          {/* <Typography sx={{ fontFamily: "Orbitron" }} variant="body1">
            Preparing for liftoff
          </Typography> */}
        </Stack>
      </Box>
    </Stack>
  );
};

export default AppFalconOneLoader;
