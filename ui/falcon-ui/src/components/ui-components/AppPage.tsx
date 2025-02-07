import { FC, lazy, ReactNode } from "react";
import AppLazyLoader from "@ui-components/AppLazyLoader";
import { useSelector } from "react-redux";
import AppConstants from "@/constants/constants";
const Grid = lazy(() => import("@mui/material/Grid"));
const Stack = lazy(() => import("@mui/material/Stack"));
const Typography = lazy(() => import("@mui/material/Typography"));

interface AppPageProps {
  title: string;
  content: ReactNode;
  rightHeaderActions?: ReactNode;
}

const AppPage: FC<AppPageProps> = ({ title, content, rightHeaderActions }) => {
  const themeStore = useSelector((state: any) => state.theme.siteTheme);

  return (
    <>
      <AppLazyLoader>
        <Grid container spacing={AppConstants.layout.StandardSpacing}>
          <Grid item md={12} xs={12} sm={12}>
            <Stack
              direction={"row"}
              flexWrap={"wrap"}
              justifyContent={"space-between"}
              alignItems="center"
            >
              <Typography
                style={{
                  borderLeft: `5px solid ${themeStore?.primaryColor}`,
                  paddingLeft: "12px",
                }}
                variant="h6"
              >
                {title}
              </Typography>
              {rightHeaderActions}
            </Stack>
          </Grid>
          <Grid item md={12} xs={12} sm={12}>
            {content}
          </Grid>
        </Grid>
      </AppLazyLoader>
    </>
  );
};

export default AppPage;
