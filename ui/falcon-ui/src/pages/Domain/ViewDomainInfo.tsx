import DomainInfo from "@/components/features/Domain/DomainInfo";
import AppPage from "@/components/ui-components/AppPage";
import { GetDomainInfo } from "@/models/Domain/ViewDomainInfoModel";
import { useLazyGetDomainInfoQuery } from "@/services/Domain/DomainService";
import DomainInfoValidationScheme from "@/validation-schemes/Domain/GetDomainInfoValidationScheme";
import { Button, Grid, Paper, Stack, TextField } from "@mui/material";
import { useFormik } from "formik";
import { useTranslation } from "react-i18next";

const ViewDomainInfo = (props: any) => {
  const { t: commonLocale } = useTranslation();
  const [getDomainInfo, response] = useLazyGetDomainInfoQuery();

  const handleSubmit = (values: GetDomainInfo) => {
    if (values.domainName) {
      getDomainInfo(values.domainName);
    }
  };

  const formik = useFormik<GetDomainInfo>({
    initialValues: {
      domainName: "",
    },
    validationSchema: DomainInfoValidationScheme(),
    onSubmit: (values: GetDomainInfo) => {
      handleSubmit(values);
    },
  });

  return (
    <AppPage
      title={"Domain Info"}
      content={
        <>
          <Paper variant="outlined" sx={{ padding: "0.8rem" }}>
            <form
              autoComplete="off"
              onSubmit={formik.handleSubmit}
              style={{ width: "100%" }}
            >
              <Grid
                container
                spacing={0.8} // Optional for spacing between items
              >
                <Grid item md={4} xs={12}>
                  <TextField
                    id="domainName"
                    name="domainName"
                    size="small"
                    fullWidth
                    required={true}
                    label={commonLocale("domain")}
                    variant="outlined"
                    value={formik.values?.domainName}
                    onChange={formik.handleChange}
                    error={
                      formik.touched?.domainName &&
                      Boolean(formik.errors.domainName)
                    }
                    helperText={
                      formik.touched?.domainName && formik.errors.domainName
                    }
                  />
                </Grid>
                <Grid item md={4} xs={12}>
                  <Button
                    onClick={formik.submitForm}
                    variant="outlined"
                    style={{
                      height: 40,
                    }}
                  >
                    Get Domain Info
                  </Button>
                </Grid>
              </Grid>
            </form>
          </Paper>
          <Paper variant="outlined" sx={{ padding: "0.8rem" }}>
            <DomainInfo
              isLoading={response.isFetching || response.isLoading}
              data={response.data?.data}
            />
          </Paper>
        </>
      }
    ></AppPage>
  );
};

export default ViewDomainInfo;
