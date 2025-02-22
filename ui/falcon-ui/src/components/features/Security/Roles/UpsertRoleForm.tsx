import AppConstants from "@/constants/constants";
import { UpsertRoleModel } from "@/models/Security/Roles/UpsertRoleModel";
import { UpsertRoleValidationScheme } from "@/validation-schemes/Security/Roles/UpsertRoleValidationScheme";
import { useFormik } from "formik";
import { lazy, useImperativeHandle } from "react";
const Stack = lazy(() => import("@mui/material/Stack"));
const TextField = lazy(() => import("@mui/material/TextField"));
import Grid from "@mui/material/Grid2";

const UpsertRoleForm = (props: any) => {
  const formik = useFormik<UpsertRoleModel>({
    initialValues: {
      name: "",
      id: "",
    },
    validationSchema: UpsertRoleValidationScheme(props?.actionId),
    onSubmit: (values: UpsertRoleModel) => {
      props?.onSubmit(values);
    },
  });

  useImperativeHandle(props?.formikRef, () => {
    return {
      submitForm: formik.submitForm,
      resetForm: formik.resetForm,
      setValues: formik.setValues,
    };
  });

  return (
    <>
      <Stack
        direction="column"
        alignItems="center"
        spacing={AppConstants.layout.StandardSpacing}
      >
        <form
          autoComplete="off"
          onSubmit={formik.handleSubmit}
          style={{ width: "100%" }}
        >
          <Grid container spacing={1.5}>
            <Grid size={{ xs: 12, md: 12 }}>
              <TextField
                id="name"
                name="name"
                size="small"
                required={true}
                label={"Name"}
                variant="outlined"
                fullWidth
                value={formik.values?.name}
                onChange={formik.handleChange}
                error={formik.touched?.name && Boolean(formik.errors.name)}
                helperText={formik.touched?.name && formik.errors.name}
              />
            </Grid>
          </Grid>
        </form>
      </Stack>
    </>
  );
};

export default UpsertRoleForm;
