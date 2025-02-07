import TextField from "@mui/material/TextField";
import Stack from "@mui/material/Stack";
import Link from "@mui/material/Link";
import InputLabel from "@mui/material/InputLabel";
import InputAdornment from "@mui/material/InputAdornment";
import FormControl from "@mui/material/FormControl";
import Visibility from "@mui/icons-material/Visibility";
import VisibilityOff from "@mui/icons-material/VisibilityOff";
import OutlinedInput from "@mui/material/OutlinedInput";
import IconButton from "@mui/material/IconButton";
import { useState } from "react";
import Typography from "@mui/material/Typography";
import FormHelperText from "@mui/material/FormHelperText";
import { CircularProgress, Paper } from "@mui/material";
import { LoadingButton } from "@mui/lab";
import AppConstants from "@/constants/constants";

const CreateMeetingForm = (props: any) => {
  const [showPassword, setShowPassword] = useState({
    showPassword: false,
  });

  const handleClickShowPassword = () => {
    setShowPassword({
      ...showPassword,
      showPassword: !showPassword.showPassword,
    });
  };

  const handleMouseDownPassword = (event: any) => {
    event.preventDefault();
  };

  return (
    <>
      <Stack direction="row" justifyContent="center">
        <Paper sx={{ padding: "1rem" }}>
          <form onSubmit={props.formik.handleSubmit}>
            <Stack spacing={AppConstants.layout.StandardSpacing}>
              <Typography variant="button" display="block" gutterBottom>
                Login
              </Typography>
              <TextField
                id="email"
                name="email"
                label="Email"
                variant="outlined"
                value={props.formik.values.email}
                onChange={props.formik.handleChange}
                error={
                  props.formik.touched.email &&
                  Boolean(props.formik.errors.email)
                }
                helperText={
                  props.formik.touched.email && props.formik.errors.email
                }
              />

              {true ? (
                <LoadingButton
                  loading={props?.loading}
                  type="submit"
                  variant="contained"
                >
                  Login
                </LoadingButton>
              ) : (
                <Stack alignItems="center" justifyContent="center">
                  <CircularProgress />
                </Stack>
              )}
              <Stack direction="row" justifyContent="center">
                <Link sx={{ fontSize: "12px" }} href="signup">
                  Doesn't have an Account ? Signup here
                </Link>
              </Stack>
            </Stack>
          </form>
        </Paper>
      </Stack>
    </>
  );
};

export default CreateMeetingForm;
